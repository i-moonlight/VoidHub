using AutoMapper;
using ForumApi.Data.Models;
using ForumApi.Data.Repository.Extensions;
using ForumApi.Data.Repository.Interfaces;
using ForumApi.DTO.DBan;
using ForumApi.DTO.Page;
using ForumApi.Exceptions;
using ForumApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ForumApi.Services
{
    public class BanService : IBanService
    {
        private readonly IRepositoryManager _rep;
        private readonly IMapper _mapper;

        public BanService(
            IRepositoryManager rep,
            IMapper mapper)
        {
            _rep = rep;
            _mapper = mapper;
        }

        public async Task<List<BanResponse>> GetBans(Page page)
        {
            return await _rep.Ban.Value
                .FindAll()
                .OrderByDescending(b => b.CreatedAt)
                .TakePage(page)
                .Select(b => new BanResponse
                {
                    Id = b.Id,
                    CreatedAt = b.CreatedAt,
                    Moderator = b.Moderator,
                    Account = b.Account,
                    UpdatedBy = b.UpdatedBy,
                    Reason = b.Reason,
                    ExpiresAt = b.ExpiresAt,
                }).ToListAsync();
        }

        public async Task<Ban> Create(int moderId, BanDto ban)
        {
            var moder = await _rep.Account.Value
                .FindById(moderId).FirstOrDefaultAsync() ?? throw new NotFoundException("Moderator not found");

            var user = await _rep.Account.Value
                .FindById(ban.AccountId).FirstOrDefaultAsync() ?? throw new NotFoundException("User not found");

            if(moder.Id == user.Id)
                throw new BadRequestException("You cannot ban yourself");

            if(moder.Role == Role.Moder && user.Role != Role.User)
                throw new ForbiddenException("You cannot perform this action");

            var banEntity = _mapper.Map<Ban>(ban);
            banEntity.ModeratorId = moderId;
            banEntity.UpdatedById = moderId;

            _rep.Ban.Value.Create(banEntity);

            await _rep.Save();

            return banEntity;
        }
        
        public async Task<Ban> Update(int moderId, int banId, BanDto ban)
        {
            var moder = await _rep.Account.Value
                .FindById(moderId)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("Moderator not found");

            var banEntity = await _rep.Ban.Value
                .FindByCondition(b => b.Id == banId, true)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("Ban not found");

            if(moder.Role == Role.Moder && banEntity.Account.Role != Role.User)
                throw new ForbiddenException("You cannot perform this action");

            _mapper.Map(ban, banEntity);
            banEntity.UpdatedById = moderId;
            banEntity.UpdatedAt = DateTime.UtcNow;

            await _rep.Save();

            return banEntity;
        }

        public async Task Delete(int moderId, int accountId)
        {
            var moder = await _rep.Account.Value
                .FindById(moderId)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("Moderator not found");

            var activeBans = await _rep.Ban.Value
                .FindByCondition(b => b.AccountId == accountId && b.IsActive == true, true)
                .ToListAsync();

            if(!activeBans.Any()) 
                throw new NotFoundException("No active bans");

            if(moder.Role == Role.Moder && activeBans[0].Account.Role != Role.User)
                throw new ForbiddenException("You cannot perform this action");

            _rep.Ban.Value.DeleteMany(activeBans);
            foreach(var ban in activeBans)
                ban.ModeratorId = moderId;

            await _rep.Save();
        }
    }
}
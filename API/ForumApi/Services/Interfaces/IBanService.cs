using ForumApi.Data.Models;
using ForumApi.DTO.DBan;
using ForumApi.DTO.Page;

namespace ForumApi.Services.Interfaces
{
    public interface IBanService
    {
        Task<List<BanResponse>> GetBans(Page page);
        Task<Ban> Create(int moderId, BanDto ban);
        Task<Ban> Update(int moderId, int banId, BanDto ban);
        Task Delete(int moderId, int banId);
    }
}
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using ForumApi.Data.Models;
using ForumApi.Data.Repository.Extensions;
using ForumApi.Data.Repository.Interfaces;
using ForumApi.DTO.DSearch;
using ForumApi.DTO.Page;
using ForumApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForumApi.Services
{
    public class SearchService : ISearchService
    {
        private readonly IRepositoryManager _rep;



        public SearchService(IRepositoryManager rep)
        {
            _rep = rep;
        }

        public async Task<List<Topic>> SearchTopics([FromQuery] string query, SearchParams search, Page page)
        {
            var predicate = PredicateBuilder.Create<Topic>(t => t.DeletedAt == null);

            var predicators = new Dictionary<string, Expression<Func<Topic, bool>>>
            {
                [SearchParamNames.WordTitle] = t => t.SearchVector.Matches(EF.Functions.ToTsQuery("english", query)),
                [SearchParamNames.WordContent] = t => t.Posts.First().SearchVector.Matches(EF.Functions.ToTsQuery("english", query)),
                [SearchParamNames.PartialTitle] = t => EF.Functions.Like(t.Title, $"%{query.ToLower()}%"),
                [SearchParamNames.PartialContent] = t => EF.Functions.Like(t.Posts.First().Content ?? "", $"%{query.ToLower()}%"),
            };
            //first need to be AND, then OR
            var first = true;

            if(search.WordMatch)
            {
                predicate = predicate.AndOrFirst(predicators[SearchParamNames.WordTitle], ref first);

                if(search.WithPostContent)
                    predicate = predicate.OR(predicators[SearchParamNames.WordContent]);
            }

            if(search.PartialMatch)
            {
                predicate = predicate.AndOrFirst(predicators[SearchParamNames.PartialTitle], ref first);

                if(search.WithPostContent)
                    predicate = predicate.OR(predicators[SearchParamNames.PartialContent]);
            }
            
            var q = _rep.Topic.Value.FindByCondition(predicate);

            //priority of sorting:
            if(search.WordMatch)
            {
                q = q.OrderByDescending(t => t.SearchVector.Rank( 
                    EF.Functions.ToTsQuery("english", query)
                ));
            }

            //apply sort
            if(string.IsNullOrEmpty(search.Sort) && !search.WordMatch)
            {
                q = q.OrderByDescending(t => t.CreatedAt);
            }
            else
            {
                //TODO: replace with normal))
                q = q.OrderBy($"CreatedAt {search.Sort}");
            }

            return await q.TakePage(page).ToListAsync();
        }
    }
}
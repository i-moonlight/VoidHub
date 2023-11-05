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

        public async Task<SearchResponse> SearchTopics([FromQuery] string query, SearchParams search, Page page)
        {
            query = query.Trim();

            var predicate = PredicateBuilder.Create<Topic>(t => t.DeletedAt == null);

            // configure tsquery search
            var forTsQuery = query.Split(' ')
                .Select(w => $"{w}:*")
                .Aggregate((a, b) => $"{a} | {b}");

            var predicators = new Dictionary<string, Expression<Func<Topic, bool>>>
            {
                [SearchParamNames.WordTitle] = t => t.SearchVector.Matches(EF.Functions.ToTsQuery("english", forTsQuery)),
                [SearchParamNames.WordContent] = t => t.Posts.First().SearchVector.Matches(EF.Functions.ToTsQuery("english", forTsQuery)),
                [SearchParamNames.PartialTitle] = t => EF.Functions.Like(t.Title, $"%{query.ToLower()}%"),
                [SearchParamNames.PartialContent] = t => EF.Functions.Like(t.Posts.First().Content ?? "", $"%{query.ToLower()}%"),
            };

            predicate = predicate.AND(predicators[SearchParamNames.WordTitle]);

            if(search.WithPostContent)
                predicate = predicate.OR(predicators[SearchParamNames.WordContent]);
            
            // do search
            var q = _rep.Topic.Value.FindByCondition(predicate);


            q = q.OrderByDescending(t => t.SearchVector.Rank( 
                EF.Functions.ToTsQuery("english", forTsQuery)
            ));

            //apply sort
            if(string.IsNullOrEmpty(search.Sort))
            {
                q = ((IOrderedQueryable<Topic>)q).ThenBy(t => t.CreatedAt);
            }
            else
            {
                //TODO: replace with normal))
                q = ((IOrderedQueryable<Topic>)q).ThenBy($"CreatedAt {search.Sort}");
            }

            var searchRes = new SearchResponse
            {
                SearchCount = q.Count(),
                Topics = await q.TakePage(page).ToListAsync()
            };
            
            return searchRes;
        }
    }
}
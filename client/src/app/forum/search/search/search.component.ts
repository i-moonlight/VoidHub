import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SearchParams } from '../../models/search-params.model';
import { Page } from 'src/shared/page.model';
import { SearchService } from '../../services/search.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent {

  searchResult: any;

  searchParams : SearchParams = new SearchParams("", "false");
  searchPage: Page = new Page(1);
  query = '';

  currentPage = 1;
  errorMessages = [];

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private searchService: SearchService
    ) {
    this.activatedRoute
      .queryParams.subscribe(params => {
        let newSearchParams = new SearchParams(
          params["sort"],
          params["withPostContent"]
        );

        let newSearchPage = new Page(
          params["pageNumber"],
          params["pageSize"]
        );

        let newQuery = params["query"] ;

        let isParamsChanged = !this.searchParams.Equals(newSearchParams);
        let isPageChanged = !this.searchPage.Equals(newSearchPage);
        let isQueryChanged = !(this.query == newQuery)

        console.log('new', newQuery, newSearchParams, newSearchPage);
        console.log('old', this.query, this.searchParams, this.searchPage);
        if(isParamsChanged || isPageChanged || isQueryChanged) {
          this.query = newQuery ?? "";
          this.searchParams = newSearchParams;

          // go to 1 page if search changed
          if(!isPageChanged && this.searchPage.pageNumber != 1) {
            this.router.navigate([], {
              relativeTo: this.activatedRoute,
              queryParams: {
                ...{query: this.query},
                ...new Page(1, this.searchPage.pageSize),
                ...this.searchParams
              },
              queryParamsHandling: 'merge'
            });
          }
          else {
            this.searchPage = newSearchPage
            this.search();
          }
        }
      });
  }

  search() {
    console.log(this.query, this.searchParams, this.searchPage);
    this.searchService.searchTopics(this.query, this.searchParams, this.searchPage)
    .subscribe({
      next: data => {
        console.log(data);
        this.errorMessages = [];
        this.searchResult = null;
        this.searchResult = data;
      },
      error: errs => {
        this.errorMessages = errs;
      }
    })
  }

  changePage(newPage: number) {
    this.router.navigate([], {
      relativeTo: this.activatedRoute,
      queryParams: {
        ...{query: this.query},
        ...new Page(newPage, this.searchPage.pageSize),
        ...this.searchParams
      },
      queryParamsHandling: 'merge'
    });
  }
}

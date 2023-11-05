import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable()
export class SearchService {

  baseURL = "http://localhost:5000/api/v1/";

  constructor(private http: HttpClient) {}

  searchTopics(query, params, page) {
    return this.http.get(this.baseURL + 'topics/search', {
      params: {
        ...{query: query},
        ...params,
        ...page
      }
    })
  }

}

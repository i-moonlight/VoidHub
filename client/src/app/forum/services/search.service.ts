import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment as env } from "src/environments/environment";

@Injectable()
export class SearchService {

  baseURL = env.baseAPIUrl + "/v1/";

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

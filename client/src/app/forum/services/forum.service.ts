import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Page } from "src/shared/page.model";
import { environment as env } from "src/environments/environment";


@Injectable()
export class ForumService {

  private baseUrl = env.baseAPIUrl +  '/v1/forums';

  constructor(private http: HttpClient) {}

  getForum(forumId) {
    return this.http.get(`${this.baseUrl}/${forumId}`);
  }

  getForumTopics(forumId, page: Page) {
    return this.http.get(`${this.baseUrl}/${forumId}/topics`, {
      headers: {
        'X-Limit-Param' : '2'
      },
      params: {...page}
    });
  }

  createForum(data) {
    return this.http.post(this.baseUrl, data);
  }

  updateForum(forumId, data) {
    return this.http.put(`${this.baseUrl}/${forumId}`, data);
  }

  deleteForum(forumId) {
    return this.http.delete(`${this.baseUrl}/${forumId}`);
  }
}

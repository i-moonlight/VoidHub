import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Page } from "src/shared/page.model";

@Injectable()
export class ForumService {

  private baseUrl = 'http://localhost:5000/api/v1/forums';

  constructor(private http: HttpClient) {}

  createForum(data) {
    return this.http.post(this.baseUrl, data);
  }

  getForum(forumId) {
    return this.http.get(`${this.baseUrl}/${forumId}`);
  }

  getForumTopics(forumId, page: Page) {
    return this.http.get(`${this.baseUrl}/${forumId}/topics`, {params: {...page}});
  }
}

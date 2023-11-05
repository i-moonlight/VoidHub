import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Page } from "src/shared/page.model";

@Injectable()
export class TopicService {

  private baseURL = 'http://localhost:5000/api/v1/topics';

  constructor(
    private http: HttpClient,
  ) {}

  getTopic(topicId) {
    return this.http.get(`${this.baseURL}/${topicId}`);
  }

  getPostsPage(topicId, page: Page) {
    return this.http.get(`${this.baseURL}/${topicId}/posts`, {
      headers: {
        'X-Limit-Param' : '2'
      },
      params: {...page}
    });
  }

  createTopic(topic) {
    return this.http.post(this.baseURL, topic);
  }

  updateTopic(topicId, data) {
    return this.http.put(this.baseURL + '/' + topicId, data);
  }

  deleteTopic(topicId) {
    return this.http.delete(this.baseURL + '/' + topicId);
  }
}

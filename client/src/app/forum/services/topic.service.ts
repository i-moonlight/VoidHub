import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Offset } from "src/shared/offset.model";
import { Page } from "src/shared/page.model";

@Injectable()
export class TopicService {

  private baseURL = 'http://localhost:5000/api/v1/topics';

  constructor(
    private http: HttpClient,
  ) {}

  getTopic(topicId, offset: Offset) {
    return this.http.get(`${this.baseURL}/${topicId}`, {
      params: {...offset}
    });
  }

  getTopics(offset: Offset, time: Date) {
    return this.http.get(this.baseURL, {
      params: {
        ...offset,
        time: time.toISOString()
      }
    })
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

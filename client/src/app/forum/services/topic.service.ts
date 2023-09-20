import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable()
export class TopicService {

  private baseURL = 'http://localhost:5000/api/v1/topics';

  constructor(
    private http: HttpClient,
  ) {}

  createTopic(topic) {
    return this.http.post(this.baseURL, topic);
  }
}

import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable()
export class ForumService {

  private baseUrl = 'http://localhost:5000/api/v1/forums';

  constructor(private http: HttpClient) {}

  createForum(data) {
    return this.http.post(this.baseUrl, data);
  }

}

import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Page } from "src/shared/page.model";


@Injectable()
export class PostService {

  private baseURL = 'http://localhost:5000/api/v1/posts';

  constructor(private http: HttpClient) {}

  createPost(data) {
    return this.http.post(this.baseURL, data);
  }

  updatePost(postId, data) {
    return this.http.put(`${this.baseURL}/${postId}`, data);
  }
}

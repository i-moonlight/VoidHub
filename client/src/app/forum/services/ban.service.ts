import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";


@Injectable()
export class BanService {

  baseUrl = 'http://localhost:5000/api/v1/bans';

  constructor(private http: HttpClient) {}

  public banUser(data) {
    return this.http.post(this.baseUrl, data);
  }

  public unbanUser(accountId) {
    return this.http.delete(`${this.baseUrl}?accountId=${accountId}`);
  }
}

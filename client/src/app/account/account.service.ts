import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { environment as env } from 'src/environments/environment';

@Injectable()
export class AccountService {

  private baseURL = env.baseAPIUrl + '/v1/accounts'

  constructor(private client: HttpClient) {}

  getAccount(id: number) {
    return this.client.get(this.baseURL + '/' + id);
  }

  updAccount(data: any, currentAvatarPath: string) {
    return this.client.patch(this.baseURL, data, {
      params: {
        currentPath: currentAvatarPath
      }
    });
  }

  updAvatar(data: FormData) {
    return this.client.patch(`${this.baseURL}/avatar`, data, {
      reportProgress: true,
      responseType: 'json',
      observe: 'events'
    });
  }
}

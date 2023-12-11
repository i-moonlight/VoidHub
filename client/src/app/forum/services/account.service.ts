import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Roles } from 'src/shared/roles.enum';
import { environment as env } from 'src/environments/environment';

@Injectable()
export class AccountService {

  private baseURL = env.baseAPIUrl + '/v1/accounts';

  constructor(private http: HttpClient) {}

  updateRole(id, role: Roles) {
    return this.http.patch(`${this.baseURL}/${id}`, {
      role: role
    });
  }

  updateUsername(id, data) {
    return this.http.patch(`${this.baseURL}/${id}/rename`, data);
  }

  defaultAvatar(id) {
    return this.http.patch(`${this.baseURL}/${id}/avatar-default`, null);
  }
}

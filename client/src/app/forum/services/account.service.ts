import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Roles } from 'src/shared/roles.enum';

@Injectable()
export class AccountService {

  constructor(private http: HttpClient) {}

  updateRole(id, role: Roles) {
    return this.http.patch(`http://localhost:5000/api/v1/accounts/${id}`, {
      role: role
    });
  }

}

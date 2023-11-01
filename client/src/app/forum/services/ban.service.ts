import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";


@Injectable()
export class BanService {

  constructor(private http: HttpClient) {}

}

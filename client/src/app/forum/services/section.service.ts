import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Section } from "../models/section.model";
import { environment as env } from "src/environments/environment";

@Injectable()
export class SectionService {

  baseUrl = env.baseAPIUrl + '/v1/sections';

  constructor(private http: HttpClient) {}

  getSections() {
    return this.http.get<Section[]>(this.baseUrl);
  }

  createSection(section) {
    return this.http.post(this.baseUrl, section);
  }

  updateSection(sectionId, data) {
    return this.http.put(`${this.baseUrl}/${sectionId}`, data);
  }
}

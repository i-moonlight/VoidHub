import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Section } from "../models/section.model";

@Injectable()
export class SectionService {

  baseUrl = 'http://localhost:5000/api/v1/sections';

  constructor(private http: HttpClient) {}

  getSections() {
    return this.http.get<Section[]>(this.baseUrl);
  }

  createSection(section) {
    return this.http.post(this.baseUrl, section);
  }
}

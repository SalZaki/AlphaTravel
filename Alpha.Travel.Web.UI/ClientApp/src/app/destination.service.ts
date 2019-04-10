import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DestinationService {

  baseUrl: string = 'https://localhost:44351/api/v1/destinations';

  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get(this.baseUrl);
  }
}

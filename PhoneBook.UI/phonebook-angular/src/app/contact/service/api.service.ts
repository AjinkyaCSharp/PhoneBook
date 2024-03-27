import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Contact } from '../model/contact.model';
import { HttpHeaders } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class ApiService {

  apiUrl = 'https://localhost:7232/api/phonebook';
  apiKeyHeader=new HttpHeaders().set('x-api-key','a4135f84-831a-4e3a-8311-29e7aa8d20f3');
  authHeader:HttpHeaders;

  constructor(private http: HttpClient) { 
  var user=JSON.parse(localStorage.getItem('user'));
  this.authHeader=new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${user.stsTokenManager.accessToken as string}`
  });

  }

  getContact(offset,numRecords): Observable<Contact[]> {
    return this.http.get<Contact[]>(`${this.apiUrl}/contact?offset  =${offset}&numRecords=${numRecords}`,{headers:this.authHeader});
  }

  getConactById(id: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/contact/${id}`);
  }

  addContact(post: Contact): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/contact`, post,{headers:this.authHeader});
  }

  deleteContact(id: string): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/contact/${id}`,{headers:this.authHeader});
  }
}
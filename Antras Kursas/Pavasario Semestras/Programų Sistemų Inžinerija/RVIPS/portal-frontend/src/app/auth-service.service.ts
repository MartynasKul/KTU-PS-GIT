import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { ApiService } from './api-service.service';
import { Role } from './models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private token: string | null;

  constructor(private http: HttpClient, private router: Router, apiService: ApiService) {
    this.token = localStorage.getItem('token');
  }

  validateToken() {
    this.resetToken();
      var token = this.getToken();
      if (token) {
        const decodedToken = JSON.parse(atob(token.split('.')[1]));
        const expirationDate = new Date(decodedToken.exp * 1000);
        const now = new Date();
        const timeUntilExpiration = expirationDate.getTime() - now.getTime();

        if (timeUntilExpiration < 5 * 60 * 1000) {
          if (timeUntilExpiration < 0) {
            localStorage.clear();
            this.router.navigate(['/login']);
          } else {
            this.renewToken().subscribe();
          }
        }
      }
  }

  getToken() {
    this.resetToken()
    return this.token;
  }

  resetToken() {
    this.token = localStorage.getItem('token');
  }

  isAuthenticated() {
    // check if the token exists and is valid
    return !!this.getToken();
  }

  renewToken() {
    var options = {
        headers: new HttpHeaders({
           'Accept':'text/plain'
        }),
        'responseType': 'text' as 'json'
     }

    // make a request to the renew token endpoint and save the new token to local storage
    return this.http.get<string>('api/renew-token', options)
      .pipe(
        tap(response => {
          this.token = response;
          localStorage.setItem('token', this.token);
        })
      );
  }
}
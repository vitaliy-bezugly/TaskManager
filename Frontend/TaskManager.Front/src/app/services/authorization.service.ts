import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';
import { AUTH_API_URL } from 'src/app-injection-token';
import { Token } from 'src/models/Token';

export const ACCES_TOKEN_KEY = 'task_manager_acces_token'

@Injectable({
  providedIn: 'root'
})

export class AuthorizationService {
  constructor(private http : HttpClient, @Inject(AUTH_API_URL) private apiUrl : string, 
      private jwtHelper : JwtHelperService, private router : Router) { }
  
  login(email : string, password : string): Observable<Token> {
    var someValue = this.http.post<Token>(this.apiUrl + 'Account/Login', {email, password});

    someValue.subscribe(res => {
      localStorage.setItem(ACCES_TOKEN_KEY, res.acces_token)
    })

    return someValue;
  }

  logout(): void {
    localStorage.removeItem(ACCES_TOKEN_KEY)
    this.router.navigate([''])
  } 

  isAuthenticated(): boolean {
    let token = localStorage.getItem(ACCES_TOKEN_KEY)
    if(token !== null && this.jwtHelper.isTokenExpired(token) === false) {
      return true
    }

    return false
  }
}

import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';
import { AUTH_API_URL } from 'src/app-injection-token';
import { RegisterViewModel } from 'src/models/registreViewModel';
import { Token } from 'src/models/token';


export const ACCES_TOKEN_KEY = 'task_manager_acces_token'

@Injectable({
  providedIn: 'root'
})

export class AuthorizationService {
  constructor(private http : HttpClient, @Inject(AUTH_API_URL) private apiUrl : string, 
      private jwtHelper : JwtHelperService, private router : Router) { }
  
  login(email : string, password : string): Observable<Token> {
    var request = this.http.post<Token>(this.apiUrl + 'Account/Login', {email, password});

    request.subscribe(data => {
      localStorage.setItem(ACCES_TOKEN_KEY, data.access_token)
    }, (e: HttpErrorResponse) => {
      console.log(e)
    })

    return request
  }

  register(registerViewModel : RegisterViewModel) {
    var request = this.http.post<Token>(this.apiUrl + 'Account/Register', registerViewModel);

    request.subscribe(data => {
      localStorage.setItem(ACCES_TOKEN_KEY, data.access_token)
    }, (e: HttpErrorResponse) => {
      console.log(e)
    })

    return request
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

  getUsername() : string {
    let token = localStorage.getItem(ACCES_TOKEN_KEY);

    if(token != null) {
      let decodedJWT = this.jwtHelper.decodeToken(token);
      return decodedJWT.given_name
    }

    return ""
  }
}

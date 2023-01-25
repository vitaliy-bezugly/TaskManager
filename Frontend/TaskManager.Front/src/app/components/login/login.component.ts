import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ACCES_TOKEN_KEY, AuthorizationService } from 'src/app/services/authorization.service';
import { LoginViewModel } from 'src/models/loginModel';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public loginViewModel : LoginViewModel
  public alertMessage : string
  public successLogin : boolean

  constructor(private authService : AuthorizationService) { 
    this.loginViewModel = new LoginViewModel()
    this.alertMessage = ""
    this.successLogin = false
  }

  ngOnInit(): void {
  }

  login() : void {
    let response = this.authService.login(this.loginViewModel.email, this.loginViewModel.password)

    this.showCircle()
    response.subscribe(data => {
      this.alertMessage = 'You have been successfully logged in!'
      this.successLogin = true
      this.hideCircle()
    }, (error : HttpErrorResponse) => {
      if(error.status === 401) {
        this.alertMessage = 'Incorrect email or password'
      }
      else {
        this.alertMessage = 'Something goes wrong'
        
      }

      let alertError = document.getElementById('alertError')
        alertError?.removeAttribute('hidden')
      this.hideCircle()
    })
  }

  showCircle() {
    let circle = document.getElementById('load-circle-login')
    circle?.removeAttribute('hidden')
  }
  hideCircle() : void {
    let circle = document.getElementById('load-circle-login')
    circle?.setAttribute('hidden', '')
  }
}

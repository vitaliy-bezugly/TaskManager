import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ACCES_TOKEN_KEY, AuthorizationService } from 'src/app/services/authorization.service';
import { RegisterViewModel } from 'src/models/registreViewModel';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  public alertMessage : string
  public successRegister : boolean
  public registerViewModel : RegisterViewModel
  public matchPassword : boolean

  constructor(private authService : AuthorizationService) { 
    this.registerViewModel = new RegisterViewModel()
    this.alertMessage = ""
    this.successRegister = false
    this.matchPassword = true
  }

  ngOnInit(): void {
  }

  register() : void {
    let response = this.authService.register(this.registerViewModel)

    this.showCircle()
    response.subscribe(data => {
      localStorage.setItem(ACCES_TOKEN_KEY, data.access_token)

      this.alertMessage = 'You have been successfully created account!'
      this.successRegister = true
      this.hideCircle()
    }, (error : HttpErrorResponse) => {
      console.log(error)
      this.alertMessage = ''

      if(error.error.errors["Password"])
      {
        for(let item of error.error.errors.Password) {
          this.alertMessage += item + '\n'
        }
      }
      else if(error.error.errors["Email"])
      {
        for(let item of error.error.errors.Email) {
          this.alertMessage += item + '\n'
        }
      }
      else if(error.error.errors["Username"]) {
        for(let item of error.error.errors.Username) {
          this.alertMessage += item + '\n'
        }
      }
      else {
        for(let item of error.error.errors) {
          this.alertMessage += item + '\n'
        }
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

  textChanged(newValue : any) : void {
    let password = document.getElementById('password') as HTMLInputElement
    let confirmPassword = document.getElementById('confirmPassword') as HTMLInputElement

    if(password.value != confirmPassword.value) {
      this.matchPassword = false
    } else {
      this.matchPassword = true
    }
  }
}

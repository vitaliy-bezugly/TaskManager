import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AuthorizationService } from 'src/app/services/authorization.service';
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
      this.alertMessage = 'You have been successfully created account!'
      this.successRegister = true
      this.hideCircle()
    }, (error : HttpErrorResponse) => {
      if(error.status === 401) {
        this.alertMessage = 'Incorrect data'
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

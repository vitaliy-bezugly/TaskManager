import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthorizationService } from 'src/app/services/authorization.service';
import { ChangePasswordRequest } from 'src/models/changePasswordRequest';
import { ChangeUsernameRequest } from 'src/models/changeUsernameRequest';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {
  public usernameRequest : ChangeUsernameRequest = new ChangeUsernameRequest()
  public passwordRequest : ChangePasswordRequest = new ChangePasswordRequest()
  public matchPassword : boolean = true

  constructor(private authService : AuthorizationService, private router: Router) { }

  ngOnInit(): void {
    if(this.authService.isAuthenticated() !== true) {
      this.router.navigateByUrl('')
    }
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

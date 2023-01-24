import { Component, OnInit } from '@angular/core';
import { AuthorizationService } from 'src/app/services/authorization.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {
  constructor(private authorizationService : AuthorizationService) { }

  ngOnInit(): void {
  }

  getUsername() : string {
    return this.authorizationService.getUsername()
  }

  isAuthorized() : boolean {
    return this.authorizationService.isAuthenticated()
  }

  logout() : void {
    this.authorizationService.logout()
  }
}

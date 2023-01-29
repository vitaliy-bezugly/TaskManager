import { Component, ElementRef, OnInit } from '@angular/core';
import { AuthorizationService } from 'src/app/services/authorization.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  constructor(private el: ElementRef, private authService : AuthorizationService) { }

  ngOnInit(): void {
    console.log('ngOnInit()')
  }

  public hideOrShowSidebar() : any {
    let myTag = this.el.nativeElement.querySelector("div"); // you can select html element by getelementsByClassName also, please use as per your requirement.
    if(myTag.classList.contains('close') === false)
    {
        myTag.classList.add('close'); 
    }
    else {
      myTag.classList.remove('close'); 
    }
  }

  public isAuthenticated() : boolean {
    return this.authService.isAuthenticated()
  }
}

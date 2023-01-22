import { Component, ElementRef, OnInit } from '@angular/core';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  isAuthenticated = true;

  constructor(private el: ElementRef) { }
  isAuthorized : boolean = false;
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
}

import { Component, ElementRef, OnInit } from '@angular/core';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  isAuthenticated = true;

  constructor(private el: ElementRef) { }
  
  ngOnInit(): void {
    console.log('ngOnInit()')
  }

  public hideOrShowSidebar() : any {
    console.log('hideOrShowSidebar')

    let myTag = this.el.nativeElement.querySelector("div"); // you can select html element by getelementsByClassName also, please use as per your requirement.
    if(myTag.classList.contains('close') === false)
    {
        myTag.classList.add('close'); 
    }
    else {
      myTag.classList.remove('close'); 
    }
    /*
    const body = document.querySelector('body');
    let sidebar = body!.querySelector('nav');
    let toggle = body!.querySelector(".toggle");

    console.log(body)
    console.log(sidebar)
    console.log(toggle)

    console.log('before')

    toggle!.addEventListener("click" , () =>{
      console.log('addEventListener')

      sidebar!.classList.toggle("close");
    }) */
  }
}

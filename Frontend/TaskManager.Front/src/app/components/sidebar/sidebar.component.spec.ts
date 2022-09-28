import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SidebarComponent } from './sidebar.component';

console.log('test')

describe('SidebarComponent', () => {
  let component: SidebarComponent;
  let fixture: ComponentFixture<SidebarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SidebarComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SidebarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

const body = document.querySelector('body'),
  sidebar = body?.querySelector('nav'),
  toggle = body?.querySelector(".toggle"),
  searchBtn = body?.querySelector(".search-box");

console.log(body);

toggle?.addEventListener("click" , () =>{
sidebar?.classList.toggle("close");
})

searchBtn?.addEventListener("click" , () =>{
sidebar?.classList.remove("close");
})
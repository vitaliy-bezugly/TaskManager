import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RandomArticlesComponent } from './random-articles.component';

describe('RandomArticlesComponent', () => {
  let component: RandomArticlesComponent;
  let fixture: ComponentFixture<RandomArticlesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RandomArticlesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RandomArticlesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

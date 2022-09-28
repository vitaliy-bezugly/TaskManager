import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { RandomArticlesComponent } from './components/random-articles/random-articles.component';
import { TasksComponent } from './components/tasks/tasks.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'Home', redirectTo: ''},
  {path: 'Tasks', component: TasksComponent},
  {path: 'Articles', component: RandomArticlesComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }

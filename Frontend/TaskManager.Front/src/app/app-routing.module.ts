import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { RandomArticlesComponent } from './components/random-articles/random-articles.component';
import { RegisterComponent } from './components/register/register.component';
import { SettingsComponent } from './components/settings/settings.component';
import { TasksComponent } from './components/tasks/tasks.component';
import { ContactsComponent } from './components/contacts/contacts.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'Home', redirectTo: ''},
  {path: 'Articles', component: RandomArticlesComponent},
  {path: 'Tasks', component: TasksComponent},
  {path: 'Register', component: RegisterComponent},
  {path: 'Login', component: LoginComponent},
  {path: 'Settings', component: SettingsComponent},
  {path: 'Contacts', component: ContactsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }

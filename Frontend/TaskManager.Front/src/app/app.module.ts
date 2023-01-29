import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { JwtModule } from "@auth0/angular-jwt";
import { ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { NavigationComponent } from './components/navigation/navigation.component';
import { FooterComponent } from './components/footer/footer.component';
import { RandomArticlesComponent } from './components/random-articles/random-articles.component';
import { TasksComponent } from './components/tasks/tasks.component';
import { HomeComponent } from './components/home/home.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';

import { environment } from 'src/environments/environment';
import { ACCES_TOKEN_KEY } from './services/authorization.service';
import { AUTH_API_URL, STORE_API_URL } from 'src/app-injection-token';
import { FilterPipe } from './Pipes/filter.pipe';
import { SettingsComponent } from './components/settings/settings.component';

export function tokenGetter() {
  return localStorage.getItem(ACCES_TOKEN_KEY)
}

@NgModule({
  declarations: [
    AppComponent,
    NavigationComponent,
    FooterComponent,
    RandomArticlesComponent,
    TasksComponent,
    HomeComponent,
    SidebarComponent,
    RegisterComponent,
    LoginComponent,
    FilterPipe,
    SettingsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter,
        allowedDomains: environment.tokenWhitelistedDomains
      }
    }),
    ReactiveFormsModule
  ],
  providers: [
    DatePipe,
    { provide: AUTH_API_URL, useValue: environment.authApi },
    { provide: STORE_API_URL, useValue: environment.storeApi }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

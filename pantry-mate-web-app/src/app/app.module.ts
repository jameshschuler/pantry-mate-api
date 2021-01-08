import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FooterComponent } from './components/footer/footer.component';
import { SignupComponent } from './components/signup/signup.component';
import { AccountService } from './services/account.service';
import { BaseUrlInterceptor } from './services/baseUrlInterceptor';
import { LoginComponent } from './components/login/login.component';
import { LandingComponent } from './components/landing/landing.component';
import { AlertComponent } from './components/alert/alert.component';
import { JwtInterceptor } from './helpers/jwt.interceptor';
import { ErrorInterceptor } from './helpers/error.interceptor';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AppNavigationComponent } from './components/app-navigation/app-navigation.component';
import { ProfileComponent } from './components/profile/profile.component';
import { ItemsComponent } from './components/items/items.component';


@NgModule( {
    declarations: [
        AlertComponent,
        AppComponent,
        DashboardComponent,
        NavbarComponent,
        LoginComponent,
        FooterComponent,
        SignupComponent,
        LandingComponent,
        DashboardComponent,
        AppNavigationComponent,
        ProfileComponent,
        ItemsComponent
    ],
    imports: [
        // Angular
        BrowserModule,
        FormsModule,
        HttpClientModule,
        AppRoutingModule,
        BrowserAnimationsModule,
        ReactiveFormsModule
    ],
    exports: [
    ],
    providers: [
        {
            provide: HTTP_INTERCEPTORS,
            useClass: BaseUrlInterceptor,
            multi: true
        },
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
        AccountService
    ],
    bootstrap: [AppComponent]
} )
export class AppModule { }

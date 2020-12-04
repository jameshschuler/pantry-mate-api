import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { SigninComponent } from './components/signin/signin.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FooterComponent } from './components/footer/footer.component';
import { SignupComponent } from './components/signup/signup.component';
import { AccountService } from './services/account.service';
import { environment } from 'src/environments/environment';
import { BaseUrlInterceptor } from './services/baseUrlInterceptor';


@NgModule( {
    declarations: [
        AppComponent,
        NavbarComponent,
        SigninComponent,
        FooterComponent,
        SignupComponent
    ],
    imports: [
        // Angular
        BrowserModule,
        HttpClientModule,
        AppRoutingModule,
        BrowserAnimationsModule,
        FormsModule,
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
        AccountService
    ],
    bootstrap: [AppComponent]
} )
export class AppModule { }

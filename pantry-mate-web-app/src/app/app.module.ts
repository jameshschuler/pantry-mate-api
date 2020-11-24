import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

import { AppComponent } from './app.component';
import { SigninComponent } from './components/signin/signin.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FooterComponent } from './components/footer/footer.component';
import { SignupComponent } from './components/signup/signup.component';
import { AccountService } from './services/account.service';
import { HealthCheckService } from './services/healthcheck.service';
import { environment } from 'src/environments/environment';


@NgModule( {
    declarations: [
        AppComponent,
        NavbarComponent,
        SigninComponent,
        FooterComponent,
        SignupComponent
    ],
    imports: [
        // Material Modules
        MatToolbarModule,
        MatIconModule,
        MatCardModule,
        MatListModule,
        MatInputModule,
        MatButtonModule,
        MatFormFieldModule,

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
        { provide: '', useValue: environment.baseApiUrl },
        HealthCheckService,
        AccountService
    ],
    bootstrap: [AppComponent]
} )
export class AppModule { }

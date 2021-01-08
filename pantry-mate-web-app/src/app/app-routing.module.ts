import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ItemsComponent } from './components/items/items.component';
import { LandingComponent } from './components/landing/landing.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { AuthGuard } from './helpers/auth.guard';
import { LoggedInAuthGuard } from './helpers/loggedIn.guard';

const routes: Routes = [
    { path: 'login', component: LoginComponent, canActivate: [LoggedInAuthGuard] },
    { path: 'signup', component: SignupComponent, canActivate: [LoggedInAuthGuard] },
    { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
    { path: 'items', component: ItemsComponent, canActivate: [AuthGuard] },
    { path: '', component: LandingComponent, canActivate: [LoggedInAuthGuard] },
    { path: '**', redirectTo: '' } // TODO: add notfound component
];

@NgModule( {
    imports: [RouterModule.forRoot( routes )],
    exports: [RouterModule]
} )
export class AppRoutingModule { }

import { Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";
import { AccountService } from '../services/account.service';

@Injectable( { providedIn: 'root' } )
export class LoggedInAuthGuard implements CanActivate {

    constructor ( private accountService: AccountService, private router: Router ) { }

    canActivate (): boolean {
        const user = this.accountService.userValue;
        if ( user ) {
            this.router.navigate( ['/dashboard'] )
            return false
        } else {
            return true
        }
    }
}
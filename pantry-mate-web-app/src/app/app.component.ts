import { Component, OnInit } from '@angular/core';
import { User } from './models/user';
import { AccountService } from './services/account.service';

@Component( {
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
} )
export class AppComponent implements OnInit {
    public title = 'pantry-mate';
    public user: User | null = null;

    constructor ( private accountService: AccountService ) {
        this.accountService.user.subscribe( x => this.user = x );
    }

    ngOnInit (): void {

    }
}

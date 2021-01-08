import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';

@Component( {
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.scss']
} )
export class DashboardComponent implements OnInit {
    public username?: string;

    constructor ( private accountService: AccountService ) {
        this.username = accountService.userValue?.username;
    }

    ngOnInit (): void {
    }

}

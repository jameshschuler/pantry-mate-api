import { Component, Input, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { AccountService } from 'src/app/services/account.service';

@Component( {
    selector: 'navbar',
    templateUrl: './navbar.component.html',
    styleUrls: ['./navbar.component.scss'],
} )
export class NavbarComponent implements OnInit {
    @Input() user: User | null = null;

    constructor ( private accountService: AccountService ) { }

    ngOnInit (): void {
    }

    logout () {
        this.accountService.logout();
    }
}

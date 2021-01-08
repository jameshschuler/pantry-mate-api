import { Component, Input, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { AccountService } from 'src/app/services/account.service';

@Component( {
    selector: 'app-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.scss']
} )
export class ProfileComponent implements OnInit {
    @Input() user: User | null = null;

    public fullName = 'Tony Smith';

    constructor ( private accountService: AccountService ) { }

    ngOnInit (): void {
    }

    logout () {
        this.accountService.logout();
    }

}

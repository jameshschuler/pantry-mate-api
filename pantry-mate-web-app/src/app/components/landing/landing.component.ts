import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';

@Component( {
    selector: 'app-landing',
    templateUrl: './landing.component.html',
    styleUrls: ['./landing.component.scss']
} )
export class LandingComponent implements OnInit {

    constructor ( private accountService: AccountService ) { }

    ngOnInit (): void {
        console.log( this.accountService.userValue );
    }

}

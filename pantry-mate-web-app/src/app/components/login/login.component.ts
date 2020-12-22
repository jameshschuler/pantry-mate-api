import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AppError } from 'src/app/models/error';
import { UserFormModel } from 'src/app/models/user';
import { AccountService } from 'src/app/services/account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { AlertService } from 'src/app/services/alert.service';

@Component( {
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
} )
export class LoginComponent implements OnInit {

    public user = new UserFormModel();
    public loading = false;
    public error: AppError | null = null;
    public returnUrl: string;

    constructor ( private accountService: AccountService, private alertService: AlertService, private route: ActivatedRoute, private router: Router ) {
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    }

    ngOnInit (): void {
    }

    onSubmit ( loginForm: NgForm ) {
        this.accountService.login( loginForm.value.username, loginForm.value.password )
            .pipe( first() )
            .subscribe(
                data => {
                    this.router.navigate( [this.returnUrl] );
                },
                error => {
                    this.alertService.error( error );
                    this.loading = false;
                } );
    }

}

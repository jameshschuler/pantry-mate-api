import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { AppError } from 'src/app/models/error';
import { UserFormModel } from 'src/app/models/user';
import { AccountService } from 'src/app/services/account.service';
import { AlertService } from 'src/app/services/alert.service';

@Component( {
    selector: 'app-signup',
    templateUrl: './signup.component.html',
    styleUrls: ['./signup.component.scss']
} )
export class SignupComponent implements OnInit {
    public user = new UserFormModel();
    public loading = false;
    public error: AppError | null = null;

    constructor ( private accountService: AccountService, private alertService: AlertService, private route: ActivatedRoute, private router: Router ) {
    }

    ngOnInit (): void {
    }

    save ( signupForm: NgForm ) {
        this.alertService.clear();

        this.loading = true;

        this.accountService.register( signupForm.value.username, signupForm.value.password )
            .pipe( first() )
            .subscribe(
                data => {
                    this.alertService.success( 'Registration successful', { keepAfterRouteChange: true } );
                    this.router.navigate( ['/login'], { relativeTo: this.route } );
                },
                error => {
                    this.alertService.error( error, { autoClose: false } );
                    this.loading = false;
                } );
    }
}
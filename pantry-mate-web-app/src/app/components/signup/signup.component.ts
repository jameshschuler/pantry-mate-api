import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { AppError } from 'src/app/models/error';
import { User } from 'src/app/models/user';
import { AccountService } from 'src/app/services/account.service';

@Component( {
    selector: 'app-signup',
    templateUrl: './signup.component.html',
    styleUrls: ['./signup.component.scss']
} )
export class SignupComponent implements OnInit {
    public user = new User();
    public loading = false;
    public error: AppError | null = null;

    constructor ( private accountService: AccountService ) {
    }

    ngOnInit (): void {
    }

    save ( signupForm: NgForm ) {
        console.log( signupForm.form );
        console.log( JSON.stringify( signupForm.value ) );
        this.loading = true;
        this.accountService.register( signupForm.value.username, signupForm.value.password ).subscribe( response => {
            this.loading = false;
            signupForm.resetForm();

            // TODO: redirect to login?
        }, err => {
            this.error = err.error;
            this.loading = false;
        } );
    }
}
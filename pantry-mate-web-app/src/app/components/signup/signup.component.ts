import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { AccountService } from 'src/app/services/account.service';

@Component( {
    selector: 'app-signup',
    templateUrl: './signup.component.html',
    styleUrls: ['./signup.component.scss']
} )
export class SignupComponent implements OnInit {

    public signupForm: FormGroup;
    public usernameFormControl = new FormControl( '', [Validators.required] );
    public passwordFormControl = new FormControl( '', [Validators.required] );

    constructor ( private accountService: AccountService, private formBuilder: FormBuilder ) {
        this.signupForm = this.formBuilder.group( {
            username: ['', Validators.required],
            password: ['', Validators.required],
        } );
    }

    ngOnInit (): void {
    }

    onSubmit ( signupData: any ) {
        // TODO: prevent calling service if there's an error

        // TODO: call service method
        this.accountService.register( signupData.username, signupData.password ).subscribe(
            ( response: any ) => {
                console.log( response );
            },
            ( error: any ) => {
                console.log( 'error', error );
            } );
    }
}
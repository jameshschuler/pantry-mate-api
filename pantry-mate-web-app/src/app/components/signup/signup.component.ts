import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
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

    public matcher = new MyErrorStateMatcher();

    constructor ( private accountService: AccountService, private formBuilder: FormBuilder ) {
        this.signupForm = this.formBuilder.group( {
            username: '',
            password: ''
        } );
    }

    ngOnInit (): void {
    }

    onSubmit ( signupData: any ) {
        console.log( signupData );
        // TODO: call service method
        this.accountService.register( signupData.username, signupData.password ).subscribe(
            ( response: any ) => {
                console.log( response );
            },
            ( error: any ) => {
                console.log( error );
            } );
    }
}

/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher implements ErrorStateMatcher {
    isErrorState ( control: FormControl | null, form: FormGroupDirective | NgForm | null ): boolean {
        const isSubmitted = form && form.submitted;
        return !!( control && control.invalid && ( control.dirty || control.touched || isSubmitted ) );
    }
}
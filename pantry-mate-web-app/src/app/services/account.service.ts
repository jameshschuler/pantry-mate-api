import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../models/user';
import { Router } from '@angular/router';

@Injectable( {
    providedIn: 'root'
} )
export class AccountService {

    private userSubject: BehaviorSubject<User | null>;
    public user: Observable<User | null>;

    constructor ( private router: Router, private http: HttpClient ) {
        const storedUser = localStorage.getItem( 'user' );
        let parsedUser;
        if ( storedUser ) {
            parsedUser = JSON.parse( storedUser )
        }

        this.userSubject = new BehaviorSubject<User | null>( parsedUser );
        this.user = this.userSubject.asObservable();
    }

    public get userValue (): User | null {
        return this.userSubject.value;
    }

    login ( username: string, password: string ): Observable<any> {
        return this.http.post( 'account/authenticate', {
            username,
            password
        }, {
            headers: {
                'Access-Control-Allow-Origin': '*'
            }
        } ).pipe( map( ( user: any ) => {
            localStorage.setItem( 'user', JSON.stringify( user ) );
            this.userSubject!.next( user );
            return user;
        } ) );
    }

    logout () {
        localStorage.removeItem( 'user' );
        this.userSubject!.next( null );
        this.router.navigate( ['/login'] );
    }

    register ( username: string, password: string ): Observable<any> {
        return this.http.post( 'account/register', {
            username,
            password
        }, {
            headers: {
                'Access-Control-Allow-Origin': '*'
            }
        } );
    }
}
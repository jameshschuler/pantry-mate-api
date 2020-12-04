import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable( {
    providedIn: 'root'
} )
export class AccountService {
    constructor ( private http: HttpClient ) { }

    register ( username: string, password: string ): Observable<any> {
        return this.http.post( 'account/register', {
            username,
            password
        } );
    }
}
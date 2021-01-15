import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";
import { Item } from "../models/item";

@Injectable( {
    providedIn: 'root'
} )
export class ItemService {
    constructor ( private http: HttpClient ) {

    }

    getItems (): Observable<Item[]> {
        return this.http.get<Item[]>( 'item', {
            headers: {
                'Access-Control-Allow-Origin': '*'
            }
        } ).pipe(
            catchError( this.handleError )
        );
    }

    handleError ( err: any ) {
        return throwError( err.message || err );
    }
}
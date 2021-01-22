import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";
import { Item, NewItemRequest } from "../models/item";

@Injectable( {
    providedIn: 'root'
} )
export class ItemService {
    constructor ( private http: HttpClient ) {

    }

    createItem ( item: NewItemRequest ): Observable<NewItemRequest> {
        return this.http.post<NewItemRequest>( 'item', item, {
            headers: {
                'Access-Control-Allow-Origin': '*'
            }
        } ).pipe(
            catchError( this.handleError )
        );
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

    handleError ( error: any ) {
        return throwError( error.message || error );
    }
}
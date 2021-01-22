import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";
import { Brand } from "../models/brand";
import { Item } from "../models/item";

@Injectable( {
    providedIn: 'root'
} )
export class BrandService {
    constructor ( private http: HttpClient ) {

    }

    getBrands (): Observable<Brand[]> {
        return this.http.get<Brand[]>( 'brand', {
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
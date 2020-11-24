import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HealthCheck } from '../models/healthcheck';

@Injectable( {
    providedIn: 'root'
} )
export class HealthCheckService {
    private url = 'https://pantrymateapi.azurewebsites.net/health';

    constructor ( private http: HttpClient ) { }

    getAPIHealth (): Observable<HealthCheck> {
        return this.http.get<HealthCheck>( this.url );
    }
}
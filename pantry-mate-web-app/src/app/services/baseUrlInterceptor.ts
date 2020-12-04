import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable()
export class BaseUrlInterceptor implements HttpInterceptor {
    intercept ( request: HttpRequest<any>, next: HttpHandler ): Observable<HttpEvent<any>> {
        const apiReq = request.clone( { url: `https://pantrymateapi.azurewebsites.net/api/v1/${request.url}` } );
        return next.handle( apiReq );
    }
}
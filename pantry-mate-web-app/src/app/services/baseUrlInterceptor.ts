import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable()
export class BaseUrlInterceptor implements HttpInterceptor {
    intercept ( request: HttpRequest<any>, next: HttpHandler ): Observable<HttpEvent<any>> {
        const apiReq = request.clone( { url: `${environment.apiUrl}/${request.url}` } );
        return next.handle( apiReq );
    }
}
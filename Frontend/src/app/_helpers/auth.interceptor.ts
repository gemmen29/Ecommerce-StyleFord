import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';
import { AuthenticationService } from '../_services/authentication.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    constructor(private authenticationService: AuthenticationService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // add header with basic auth credentials if user is logged in and request is to the api url
        // const user = this.authenticationService.userValue;
        // const isLoggedIn = user && user.authdata;
        const Token = localStorage.getItem("token");
        const isApiUrl = request.url.startsWith(environment.apiUrl);
        if (Token && isApiUrl) {
            request = request.clone({
                setHeaders: { 
                    Authorization: `Bearer ${Token}`                    
                }
            });
        }
        
        return next.handle(request);
    }
}
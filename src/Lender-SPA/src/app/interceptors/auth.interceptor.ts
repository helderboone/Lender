import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let token = localStorage.getItem('token');
    let image = localStorage.getItem('image');
    let authReq;
    if (token) {
      if (image != null) {
        authReq = req.clone({
          headers: req.headers 
            .set('Authorization', `Bearer ${token}`)
        });
      } else {
        authReq = req.clone({
          headers: req.headers
            .set('Content-Type', 'application/json')
            .set('Authorization', `Bearer ${token}`)
        });
      }
    } else {
      authReq = req.clone({
        headers: req.headers
          .set('Content-Type', 'application/json')
      });
    }

    return next.handle(authReq);
  }


  constructor() { }
}

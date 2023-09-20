import { Injectable } from "@angular/core";
import { AuthService } from "./auth.service";
import { HttpErrorResponse, HttpHandler, HttpRequest } from "@angular/common/http";
import { catchError, switchMap, throwError } from "rxjs";

@Injectable()
export class AuthInterceptor {
  constructor(private authService: AuthService) {}

  intercept (
    req: HttpRequest<any>,
    next: HttpHandler
  ) {
    const accessToken = localStorage.getItem('access-token');
    if(!accessToken)
      return next.handle(req);

    let modifiedReq = req.clone({
      headers: req.headers.append('Authorization', 'Bearer ' + accessToken)
    });

    return next.handle(modifiedReq).pipe(
      catchError((err) => {
        // If it's Unauthorized(401) try to refresh tokens and resend request
        if(err instanceof HttpErrorResponse && err.status === 401) {
          localStorage.removeItem('access-token');
          if(!localStorage.getItem('refresh-token')) {
            this.authService.logout();
            return throwError(err);
          }

          return this.authService.refreshAndAuth().pipe(
            switchMap(() => {
              const accessToken = localStorage.getItem('access-token');

              if (!accessToken) {
                this.authService.logout();
                return next.handle(req);
              }

              modifiedReq = req.clone({
                headers: req.headers.append('Authorization', 'Bearer ' + accessToken),
              });

              return next.handle(modifiedReq);
            }),
            catchError((err) => {
              this.authService.logout();
              return throwError(err);
            })
          );
        } else {
          return throwError(err);
        }
      })
    );
  }
}

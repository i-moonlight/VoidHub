import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Observable, catchError, throwError } from "rxjs";

export class HttpExceptionInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((err) => {
        console.log(err);
        let errors = []
        if (err instanceof HttpErrorResponse) {
          switch (err.status) {
            case 400:
              if (err.error.errors) {

                Object.keys(err.error.errors).forEach(key => {
                  errors.push(err.error.errors[key][0]);
                });

                return throwError(errors);
              }

              if (err.error) {
                errors.push(err.error)
                return throwError(errors);
              }
              break;
            case 401:
              errors.push(err.statusText)
              break;
            case 403:
              errors.push("Access denied")
              break;
            case 404:
              errors.push(err.statusText)
              break;
            case 500:
              errors.push("Internal server error");
              break;
            default:
              errors.push("Something went wrong");
              break;
          }
        }

        return throwError(errors);
      })
    );
  }

}

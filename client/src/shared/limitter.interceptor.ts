import { HttpHandler, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, finalize, tap, throwError } from "rxjs";

@Injectable()
export class LimitterInterceptor {

  private defaultLimit: number = 1;

  private activeReq = new BehaviorSubject<number>(0);
  public activeReq$ = this.activeReq.asObservable();

  intercept (
    req: HttpRequest<any>,
    next: HttpHandler
  ) {
    let limitParam = req.headers.get('X-Limit-Param') ?? this.defaultLimit;
    if(this.activeReq.value >= +limitParam)
      throw new Error('Too much requests')

    const reqMod = req.clone({ headers: req.headers.delete('X-Limit-Param') });

    return next.handle(reqMod)
    .pipe(finalize(
      () => {
        this.activeReq.next(this.activeReq.value - 1)
      },
    ));
  }
}

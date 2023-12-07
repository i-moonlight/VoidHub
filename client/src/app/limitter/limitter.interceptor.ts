import { HttpHandler, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, finalize, tap, throwError } from "rxjs";
import { LimitterService } from "./limitter.service";

@Injectable()
export class LimitterInterceptor {

  constructor(private limitter: LimitterService){}

  intercept (
    req: HttpRequest<any>,
    next: HttpHandler
  ) {
    let isSkip = req.headers.get('X-Limit-Skip') ?? false;
    if(isSkip)
    {
      const reqMod = req.clone({ headers: req.headers.delete('X-Limit-Skip') });
      return next.handle(reqMod);
    }

    let limitParam = req.headers.get('X-Limit-Param') ?? this.limitter.defaultLimit;
    if(this.limitter.isOutOfLimit(this.limitter.defaultLimit + +limitParam)) {
      throw new Error('Too much requests')
    }

    this.limitter.plus();
    const reqMod = req.clone({ headers: req.headers.delete('X-Limit-Param') });

    return next.handle(reqMod)
    .pipe(finalize(
      () => {
        this.limitter.minus();
      },
    ));
  }
}

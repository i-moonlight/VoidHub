import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

@Injectable()
export class LimitterService {

  private _defaultLimit = 1;
  public get defaultLimit() : number {
    return this._defaultLimit;
  };

  private activeReq = new BehaviorSubject<number>(0);
  public activeReq$ = this.activeReq.asObservable();

  plus() {
    this.activeReq.next(this.activeReq.value + 1);
  }

  minus() {
    this.activeReq.next(this.activeReq.value - 1);
  }

  isOutOfLimit(limit: number) {
    return this.activeReq.value >= limit;
  }
}

import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { LimitterInterceptor } from '../limitter.interceptor';
import { ReplaySubject, takeUntil } from 'rxjs';
import { LimitterService } from '../limitter.service';

@Component({
  selector: 'app-limit-loader',
  templateUrl: './limit-loader.component.html',
  styleUrls: ['./limit-loader.component.css']
})
export class LimitLoaderComponent implements OnDestroy, OnInit {

  @Input()
  limit = -1;
  activeReqs = 0;

  @Input()
  containerClasses: string = null;

  @Input()
  ignore:boolean = false;

  private readonly destroy$ = new ReplaySubject<boolean>(1);

  constructor(private limitter: LimitterService) {
  }

  ngOnInit(): void {
    this.limitter.activeReq$
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: activeReqs => {
          this.activeReqs = activeReqs
        }
      });
  }

  ngOnDestroy(): void {
    this.destroy$.next(true);
    this.destroy$.complete();
  }
}

import { Component, ElementRef, HostListener, OnDestroy, OnInit, Renderer2, ViewChild } from '@angular/core';
import { Offset } from 'src/shared/offset.model';
import { TopicService } from '../services/topic.service';
import Editor from 'ckeditor5/build/ckeditor';
import { LimitterService } from 'src/app/limitter/limitter.service';
import { ReplaySubject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-recent',
  templateUrl: './recent.component.html',
  styleUrls: ['./recent.component.css']
})
export class RecentComponent implements OnInit, OnDestroy {

  editor = Editor as {create: any}

  private _firstLoadTime: Date = new Date();
  offset = new Offset(0, 5);

  topics = [];

  @ViewChild('topicsContainer', {static: false})
  topicsContainer:ElementRef;

  lastDataLoaded = false;
  threshold = 400;

  private destroy$ = new ReplaySubject<boolean>(1);
  private activeReq = 0;

  constructor(
    private topicService: TopicService,
    private limitter: LimitterService) {
      this.limitter.activeReq$
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: activeReqs => {
          this.activeReq = activeReqs
        }
      });
  }

  @HostListener('window:scroll', ['$event'])
  onWindowScroll() {
    if(!this.topicsContainer)
      return;

    const pos = document.documentElement.scrollTop + document.documentElement.clientHeight;
    const max = this.topicsContainer.nativeElement.offsetHeight + this.topicsContainer.nativeElement.offsetTop - this.threshold;
    if(pos > max) {
      if(this.activeReq == 0 && this.lastDataLoaded)
        this.loadNew();
    }
  }

  ngOnInit(): void {
    this.loadNew();
  }

  loadNew() {
    // TODO: remove old after some cap
    this.offset.offsetNumber = this.topics.length;
    this.topicService.getTopics(this.offset, this._firstLoadTime)
      .subscribe({
        next: (topics:any[]) => {
          this.topics.push(...topics);
          this.lastDataLoaded = topics.length > 0;
        }
      })
  }

  ngOnDestroy(): void {
    this.destroy$.next(true);
    this.destroy$.complete();
  }
}

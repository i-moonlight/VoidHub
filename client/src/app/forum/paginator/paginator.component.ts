import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-paginator',
  templateUrl: './paginator.component.html',
  styleUrls: ['./paginator.component.css']
})
export class PaginatorComponent {

  private _currentPage: number;
  @Input() set currentPage(value: number) {
    this._currentPage = +value;

    if(this._currentPage < this.min)
      this._currentPage = this.min;

    this.updatePages();
  };

  get currentPage(): number {
    return this._currentPage
  };

  private _max: number = 0;
  @Input() set max(value: number) {
    value = +value;

    if(value < this.min)
      return;

    this._max = value % 1 > 0 ? Math.floor(value + 1) : value;
  };

  get max(): number {
    return this._max
  };

  min: number = 1;
  @Input()
  range: number = 3;

  pages: number[] = [];

  @Output()
  changePage = new EventEmitter<number>();

  constructor() {}

  changePageClick(page: number){
    this.changePage.emit(page);
  }

  updatePages() {
    this.pages = [];
    let currentPape = +this.currentPage;

    let min = currentPape - this.range >= this.min ? currentPape - this.range : this.min;
    let max = currentPape + this.range <= this.max ? currentPape + this.range : this.max;

    if(min == max)
      return;

    for(let i = min; i <= max; i++) {
      this.pages.push(i);
    }
  }
}

import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-paginator',
  templateUrl: './paginator.component.html',
  styleUrls: ['./paginator.component.css']
})
export class PaginatorComponent {

  private _currentPage: number;
  @Input() set currentPage(value: number) {
    this._currentPage = value;
    this.updatePages();
  };

  get currentPage(): number {
    return this._currentPage
  };

  @Input() max: number = 0;
  min: number = 1;
  @Input()
  range: number = 3;

  pages: number[] = [];

  @Output()
  changePage = new EventEmitter<number>();

  changePageClick(page: number){
    this.changePage.emit(page);
  }

  updatePages() {
    this.pages = [];
    let currentPape = +this.currentPage;

    let min = currentPape - this.range >= this.min ? currentPape - this.range : this.min;
    let max = currentPape + this.range <= this.max ? currentPape + this.range : this.max;

    for(let i = min; i <= max; i++) {
      this.pages.push(i);
    }
  }
}

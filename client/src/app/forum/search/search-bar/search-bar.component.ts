import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.css'],
})
export class SearchBarComponent {

  @Input()
  searchQuery = '';

  @Input()
  withContent:boolean = false;

  @Input()
  enableParams = false;

  @Output()
  onForceSearch = new EventEmitter();

  constructor(
    private router: Router,
    private route: ActivatedRoute
    ) {}

  onSubmit(form: NgForm) {
    let params = this.route.snapshot.queryParamMap
    if( params.get('query') == form.value.search &&
      params.get('withPostContent') == form.value.withContent+'') {
      this.onForceSearch.emit();
      return;
    }

    this.router.navigate(["/forum/search"],
    {
      queryParams: {
        query: form.value.search,
        withPostContent: form.value.withContent ?? this.withContent
      },
      queryParamsHandling: 'merge'
    });
  }
}

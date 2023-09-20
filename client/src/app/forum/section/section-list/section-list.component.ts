import { Component, OnDestroy } from '@angular/core';
import { Section } from '../../models/section.model';
import { ReplaySubject, takeUntil } from 'rxjs';
import { AuthService } from 'src/app/auth/auth.service';
import { User } from 'src/shared/models/user.model';
import { SectionService } from '../../services/section.service';

@Component({
  selector: 'app-section-list',
  templateUrl: './section-list.component.html',
  styleUrls: ['./section-list.component.css']
})
export class SectionListComponent implements OnDestroy {

  sections = [];
  /*[
    {id: 1, title: 'Section 1', orderPosition: 1, forums: [
      {id: 1, title: 'Forum 1', isClosed: false},
      {id: 2, title: 'Forum 2', isClosed: true},
      {id: 3, title: 'Forum 3', isClosed: false},
      {id: 4, title: 'Forum 4', isClosed: true},
    ]},
    {id: 1, title: 'Section 2', orderPosition: 1, forums: [
      {id: 1, title: 'Forum 1', isClosed: false},
      {id: 2, title: 'Forum 2', isClosed: true},
      {id: 3, title: 'Forum 3', isClosed: false},
      {id: 4, title: 'Forum 4', isClosed: true},
      {id: 5, title: 'Forum 5', isClosed: false},
      {id: 6, title: 'Forum 6', isClosed: true},
      {id: 7, title: 'Forum 7', isClosed: false},
      {id: 8, title: 'Forum 8', isClosed: true},
      {id: 9, title: 'Forum 9', isClosed: false},
    ]},
      {id: 1, title: 'Section 3', orderPosition: 1, forums: [
        {id: 1, title: 'Forum 1', isClosed: false},
        {id: 2, title: 'Forum 2', isClosed: true},
        {id: 3, title: 'Forum 3', isClosed: false},
        {id: 4, title: 'Forum 4', isClosed: true},
        {id: 5, title: 'Forum 5', isClosed: false},
        {id: 6, title: 'Forum 6', isClosed: true},
        {id: 7, title: 'Forum 7', isClosed: false},
        {id: 8, title: 'Forum 8', isClosed: true},
        {id: 9, title: 'Forum 9', isClosed: false},
    ]},
  ];*/

  private destroy$ = new ReplaySubject<boolean>(1);
  user: User = null;

  constructor(
    private authService: AuthService,
    private sectionService: SectionService
    )
  {
    authService.user$.pipe(takeUntil(this.destroy$))
      .subscribe(user => {this.user = user; });

    sectionService.getSections()
    .subscribe({
      next: (sections: Section[]) => {
        this.sections = sections;
        console.log(this.sections)
      },
      error: (err) => {
        console.log(err);
      }
    })
  }

  ngOnDestroy(): void {
    this.destroy$.next(true);
    this.destroy$.complete();
  }
}

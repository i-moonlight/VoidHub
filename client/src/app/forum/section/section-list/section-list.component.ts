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
        console.log(sections);
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

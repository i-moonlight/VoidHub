import { AuthService } from './../auth/auth.service';
import { Component, OnDestroy } from '@angular/core';
import { ReplaySubject, takeUntil } from 'rxjs';
import { User } from 'src/shared/models/user.model';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnDestroy {

  private readonly destroy$ = new ReplaySubject<boolean>(1);

  user: User = null;

  constructor(private authService: AuthService) {
    authService.user$
    .pipe(takeUntil(this.destroy$))
    .subscribe({
      next: data => {
        this.user = data;
      }
    })
  }

  logout() {
    this.authService.logout();
  }

  ngOnDestroy(): void {
    this.destroy$.next(true);
    this.destroy$.complete();
  }
}

import { Component, OnDestroy, OnInit } from '@angular/core';
import { AccountService } from '../account.service';
import { AuthService } from 'src/app/auth/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ReplaySubject, takeUntil } from 'rxjs';
import { User } from 'src/shared/models/user.model';
import { Roles } from 'src/shared/roles.enum';
import { AdminService } from 'src/app/forum/admin/services/admin.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnDestroy {
  resourceUrl = environment.resourceURL;

  user: User = null;
  profile: any = null;
  userId = null;
  roles = Roles;

  private destroy$ = new ReplaySubject<boolean>(1);

  constructor(
    private accountService: AccountService,
    private route: ActivatedRoute,
    private adminService: AdminService,
    router: Router,
    authService: AuthService,
  ) {

    authService.user$
      .pipe(takeUntil(this.destroy$))
      .subscribe(user => {
        this.user = user;
      });

    adminService.cancelClicked
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        _ => {
          router.navigate(['../'], { relativeTo: this.route });
        }
      )

    this.handleIdChange(this.route.snapshot.params['id']);

    route.params.subscribe(async params => {
      this.handleIdChange(params['id']);
    });

  }

  async handleIdChange(newId: number) {
    if(this.userId == newId)
      return;

    this.userId = newId;

    this.accountService.getAccount(this.userId)
      .subscribe((user: any) => {
        this.adminService.user = user;
        this.profile = user;
      });
  };

  ngOnDestroy(): void {
    this.destroy$.next(true);
    this.destroy$.complete();
    this.adminService.user = null;
  }
}

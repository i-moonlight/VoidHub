import { Component, OnDestroy } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ReplaySubject, takeUntil } from 'rxjs';
import { AuthService } from 'src/app/auth/auth.service';
import { User } from 'src/shared/models/user.model';
import { NgFormExtension } from 'src/shared/ng-form.extension';
import { AccountService } from '../account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnDestroy {

  errorMessages: string[] = [];

  user: User;
  private destroy$ = new ReplaySubject<boolean>(1);

  constructor(
    private authService: AuthService,
    private accountService: AccountService,
    private toastr: ToastrService) {
    authService.user$
      .pipe(takeUntil(this.destroy$))
      .subscribe(user => this.user = user);
  }

  onProfileSubmit(form: NgForm) {
    this.errorMessages = [];
    let data = form.value;

    if(data.oldPassword && !data.newPassword || !data.oldPassword && data.newPassword) {
      this.errorMessages.push("Both old and new password must be filled");
      return;
    }

    if(form.invalid || data.newPassword && data.newPassword.length < 8) {
      NgFormExtension.markAllAsTouched(form);
      return;
    }

    this.accountService.updAccount(data).subscribe({
      next: (user: User) => {
        this.toastr.success('Account updated successfully');
        this.authService.setUser(user);
      },
      error: errs => {
        this.errorMessages = errs;
      }
    })
  }

  ngOnDestroy(): void {
    this.destroy$.next(true);
    this.destroy$.complete();
  }
}

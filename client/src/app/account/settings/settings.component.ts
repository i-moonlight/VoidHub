import { Component, OnDestroy } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ReplaySubject, takeUntil } from 'rxjs';
import { AuthService } from 'src/app/auth/auth.service';
import { User } from 'src/shared/models/user.model';
import { NgFormExtension } from 'src/shared/ng-form.extension';
import { AccountService } from '../account.service';
import { ToastrService } from 'ngx-toastr';
import { environment as env } from 'src/environments/environment';
import { HttpEvent } from '@angular/common/http';
import { LimitterService } from 'src/app/limitter/limitter.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnDestroy {
  errorMessages: string[] = [];

  avatarUrl: string = null;
  urlDynamicParam: string = Date.now().toString();
  fileToUpload: File | null = null;
  avatarProgress: number = 0;
  activeReq: number = 0;

  user: User;
  private destroy$ = new ReplaySubject<boolean>(1);

  constructor(
    private authService: AuthService,
    private accountService: AccountService,
    private toastr: ToastrService,
    private limitterService: LimitterService
  ) {
    authService.user$
      .pipe(takeUntil(this.destroy$))
      .subscribe(user => {
        this.user = user
        if(user)
          this.urlDynamicParam = Date.now().toString();
          this.avatarUrl = `${env.resourceURL}/${user.avatarPath}`;
      });

    limitterService.activeReq$
      .pipe(takeUntil(this.destroy$))
      .subscribe(activeReq => this.activeReq = activeReq);
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

    this.accountService.updAccount(data, this.user.avatarPath).subscribe({
      next: (user: User) => {
        this.toastr.success('Account updated successfully');
        this.authService.updateUser(user);
      },
      error: errs => {
        this.errorMessages = errs;
      }
    })
  }

  handleFileInput(target: any) {
    this.fileToUpload = target.files[0];
  }

  onAvatarSubmit(form: NgForm) {
    this.errorMessages = [];
    let data = form.value;

    if(form.invalid) {
      NgFormExtension.markAllAsTouched(form);
      return;
    }

    if(!this.fileToUpload) {
      this.errorMessages.push("File is required");
      return;
    }

    if(this.fileToUpload.size > 524288) {
      this.errorMessages.push("File size must be less than 512 KB");
      return;
    }

    let formData = new FormData();
    formData.append('img', this.fileToUpload);
    this.accountService.updAvatar(formData).subscribe({

      next: (event: HttpEvent<User>) => {
        if(event.type == 1) {
          this.avatarProgress = Math.round(100 * event.loaded / event.total);
        }
        if(event.type == 4) {
          if(event.body) {
            this.authService.updateUser(event.body);
          } else {
            this.urlDynamicParam = Date.now().toString();
          }
          this.toastr.success('Avatar updated successfully');
          this.fileToUpload = null;
          form.reset();
        }
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

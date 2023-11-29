import { Component, EventEmitter, Input, OnDestroy, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { NgFormExtension } from 'src/shared/ng-form.extension';
import { BanService } from '../../services/ban.service';
import { AdminService } from '../admin.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-ban-menu',
  templateUrl: './ban-menu.component.html',
  styleUrls: ['./ban-menu.component.css']
})
export class BanMenuComponent {

  @Input()
  userId: string;

  private _currentDate: Date = new Date();

  public get currentDate():Date {
    return this._currentDate;
  }

  public set currentDate(value:Date) {
    this._currentDate = null;
    setTimeout(_ => {this._currentDate = value;})
  }

  @Output()
  onBan = new EventEmitter<void>();

  errorMessages = [];

  constructor(
    private adminService: AdminService,
    private banService: BanService,
    private toastr: ToastrService) {}

  onBanSubmit(form: NgForm) {
    this.errorMessages = [];

    if(form.invalid)
    {
      NgFormExtension.markAllAsTouched(form);
      return;
    }
    //get ban time
    let banTime = new Date(form.value.expiresAt).getTime() - new Date().getTime();

    if(banTime <= 0)
    {
      this.errorMessages.push("Ban time must be greater than current time");
      return;
    }

    //set expiresAt as UTC
    let data = form.value;
    let currentUtc = new Date().toISOString();

    data.expiresAt = new Date(new Date(currentUtc).getTime() + new Date(banTime).getTime()).toISOString();

    this.banService.banUser(data)
      .subscribe({
        next: _ => {
          form.reset();
          this.currentDate = new Date();
          this.toastr.success("User banned")
        },
        error: errs => {
          this.errorMessages = errs;
        }
      });
  }

  onUnbanSubmit(form: NgForm) {
    this.errorMessages = [];

    if(form.controls['accountId'].invalid)
    {
      return;
    }

    this.banService.unbanUser(form.value.accountId)
      .subscribe({
        next: _ => {
          form.reset();
          this.currentDate = new Date();
          this.toastr.success("User unbanned")
        },
        error: errs => {
          this.errorMessages = errs;
        }
      });
  }

  onBanClick() {
    this.onBan.emit();
  }

  onCancelClick() {
    this.adminService.cancelClicked.next();
  }
}

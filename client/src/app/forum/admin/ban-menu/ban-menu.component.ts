import { Component, EventEmitter, Input, OnDestroy, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ReplaySubject, takeUntil } from 'rxjs';
import { NgFormExtension } from 'src/shared/ng-form.extension';
import { BanService } from '../../services/ban.service';
import { AdminService } from '../admin.service';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-ban-menu',
  templateUrl: './ban-menu.component.html',
  styleUrls: ['./ban-menu.component.css']
})
export class BanMenuComponent {

  @Input()
  userId: string;

  currentDate: Date = new Date();

  @Output()
  onBan = new EventEmitter<void>();

  errorMessages = [];

  constructor(
    private adminService: AdminService,
    private banService: BanService) {}

  onSubmit(form: NgForm) {
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
    let currentUtc = new Date().toUTCString();
    data.expiresAt = new Date(new Date(currentUtc).getTime() + new Date(banTime).getTime()).toISOString();

    console.log(data);
  }

  onBanClick() {
    this.onBan.emit();
  }

  onCancelClick() {
    console.log('cancel click')
    this.adminService.cancelClicked.next();
  }
}

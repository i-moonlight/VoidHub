import { Component, Input, OnInit } from '@angular/core';
import { Roles } from 'src/shared/roles.enum';
import { AdminService } from '../services/admin.service';
import { NgForm } from '@angular/forms';
import { NgFormExtension } from 'src/shared/ng-form.extension';
import { AccountService } from '../../services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-role-menu',
  templateUrl: './role-menu.component.html',
  styleUrls: ['./role-menu.component.css']
})
export class RoleMenuComponent implements OnInit {

  @Input()
  userId;

  userIdBlocked;

  roles = Object.keys(Roles).map(key => Roles[key]);

  errorMessages = [];

  constructor(
    private adminService: AdminService,
    private accountService: AccountService,
    private toastr: ToastrService) {}

  ngOnInit(): void {
    this.userIdBlocked = this.adminService.userIdBlocked;
    this.userId = this.adminService.user.id + '';
  }

  onSubmit(form: NgForm) {
    this.errorMessages = [];

    if(this.roles.indexOf(form.value.roleName) === -1)
      form.controls['roleName'].setErrors({'incorrect': true});

    if(form.invalid) {
      NgFormExtension.markAllAsTouched(form);
      return;
    }

    this.accountService.updateRole(form.value.accountId, form.value.roleName).subscribe({
      next: () => {
        this.toastr.success('Role updated successfully');
      },
      error: errs => {
        this.errorMessages = errs;
      }
    })
  }

  onCancelClick() {
    this.adminService.cancelClicked.next();
  }
}

import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { NgFor } from '@angular/common';
import { NgFormExtension } from 'src/shared/ng-form.extension';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  errorMessages: string[] = [];

  constructor(
    private  authService: AuthService,
    private router: Router) {}

  onSubmit(form: NgForm) {
    this.errorMessages = [];

    if(form.invalid) {
      NgFormExtension.markAllAsTouched(form);
      return;
    }

    this.authService.login(form.value).subscribe({
      next: (data) => {
        this.router.navigate(['/']);
      },
      error: (err) => {
        this.errorMessages = err;
        console.log(err);
      }
    })
  }
}

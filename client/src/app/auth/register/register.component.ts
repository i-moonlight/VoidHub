import { AuthService } from './../auth.service';
import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { NgFormExtension } from 'src/shared/ng-form.extension';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  errorMessages: string[] = [];

  constructor(
    private authService: AuthService,
    private router: Router) {}


  onSubmit(form: NgForm) {
    this.errorMessages = [];
    let data = form.value;

    if(form.invalid || data.password != data.confirmPassword) {
      NgFormExtension.markAllAsTouched(form);
      return;
    }

    this.authService.register(data)
      .subscribe({
        next: data => {
          this.router.navigate(['/']);
        },
        error: err => {
          this.errorMessages = err
        }
      })
  }

}

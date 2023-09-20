import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';

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
      let controls = form.controls;
      for(let i = 0; i < Object.keys(controls).length; i++) {
        let control = controls[Object.keys(controls)[i]];
        control.markAsTouched();
      }

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

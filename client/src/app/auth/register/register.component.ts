import { AuthService } from './../auth.service';
import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

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

    console.log(form);
    if(form.invalid) {
      let controls = form.controls;
      for(let i = 0; i < Object.keys(controls).length; i++) {
        let control = controls[Object.keys(controls)[i]];
        control.markAsTouched();
      }

      return;
    }

    this.authService.register(form.value)
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

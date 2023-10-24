import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { SectionService } from '../../services/section.service';
import { Router } from '@angular/router';
import { NgFormExtension } from 'src/shared/ng-form.extension';

@Component({
  selector: 'app-new-section',
  templateUrl: './new-section.component.html',
  styleUrls: ['./new-section.component.css']
})
export class NewSectionComponent {

  errorMessages: string[] = [];

  constructor(
    private sectionService: SectionService,
    private router: Router) { }

  onSubmit(form: NgForm) {
    this.errorMessages = [];

    if(form.invalid) {
      NgFormExtension.markAllAsTouched(form);

      return;
    }

    this.sectionService.createSection(form.value).subscribe({
      next: _ => {
        this.router.navigate(['/forum/sections']);
      },
      error: errs => {
        this.errorMessages = errs;
      }
    });
  }
}

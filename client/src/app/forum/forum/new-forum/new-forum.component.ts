import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ForumService } from '../../services/forum.service';
import { NgFormExtension } from 'src/shared/ng-form.extension';

@Component({
  selector: 'app-new-forum',
  templateUrl: './new-forum.component.html',
  styleUrls: ['./new-forum.component.css']
})
export class NewForumComponent {

  sectionId: number;
  errorMessages: string[] = [];

  constructor(
    private forumService: ForumService,
    private router: Router,
    private route: ActivatedRoute) {

    route.params.subscribe(params => {
      this.sectionId = params['id'];
    })
  }

  onSubmit(form: NgForm) {
    this.errorMessages = [];

    if(form.invalid) {
      NgFormExtension.markAllAsTouched(form);

      return;
    }

    this.forumService.createForum(form.value).subscribe({
      next: _ => {
        this.router.navigate(['/forum'], { relativeTo: this.route });
      },
      error: errs => {
        this.errorMessages = errs;
      }
    });
  }
}

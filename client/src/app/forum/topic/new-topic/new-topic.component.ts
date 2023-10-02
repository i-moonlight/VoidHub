import { Component, EventEmitter, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgFormExtension } from 'src/shared/ng-form.extension';
import { TopicService } from '../../services/topic.service';
import { quillToolbarModules } from 'src/shared/quill/qiull-toolbar.modules';

@Component({
  selector: 'app-new-topic',
  templateUrl: './new-topic.component.html',
  styleUrls: ['./new-topic.component.css']
})
export class NewTopicComponent {

  forumId;
  errorMessages: string[] = [];
  content = '';

  modules = {toolbar: quillToolbarModules};

  @Output()
  close = new EventEmitter();

  @Output()
  created = new EventEmitter();

  constructor(
    route: ActivatedRoute,
    private router: Router,
    private topicService: TopicService) {
    route.params.subscribe(params => {
      this.forumId = params['id']
    })
  }

  onSubmit(form: NgForm) {
    this.errorMessages = [];

    if(form.invalid) {
      NgFormExtension.markAllAsTouched(form);
      return;
    }

    this.topicService.createTopic(form.value).subscribe({
      next: _ => {
        this.created.emit();
      },
      error: errs => {
        this.errorMessages = errs
      }
    });
  }

  onCancel() {
    this.close.emit();
  }
}

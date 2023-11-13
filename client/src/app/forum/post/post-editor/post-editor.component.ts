import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { NgFormExtension } from 'src/shared/ng-form.extension';
import Editor from 'ckeditor5/build/ckeditor';

@Component({
  selector: 'app-post-editor',
  templateUrl: './post-editor.component.html',
  styleUrls: ['./post-editor.component.css']
})
export class NewPostComponent {
  editor = Editor as {
    create: any;
  };

  @Output()
  onCreate = new EventEmitter<any>();

  @Output()
  onCancel = new EventEmitter<any>();

  @Input()
  content = '';

  @Input()
  submitPlaceholder = 'Create';

  @Input()
  cancelPlaceholder = 'Cancel';

  @Input()
  ancestorId: number | null = null;
  @Input()
  topicId: number;
  @Input()
  postId: number;

  onSubmit(form: NgForm) {
    if(form.invalid) {
      NgFormExtension.markAllAsTouched(form);
      return;
    }

    this.onCreate.emit(form.value);
  }

  onCancelClick() {
    this.content = '';
    this.onCancel.emit();
  }
}

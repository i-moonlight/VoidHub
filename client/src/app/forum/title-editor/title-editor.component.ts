import { Component, ContentChildren, EventEmitter, Input, Output, QueryList, ViewChild } from '@angular/core';
import { NgForm, NgModel } from '@angular/forms';
import { NgFormExtension } from 'src/shared/ng-form.extension';

@Component({
  selector: 'app-title-editor',
  templateUrl: './title-editor.component.html',
  styleUrls: ['./title-editor.component.css']
})
export class TitleEditorComponent {
  @ContentChildren(NgModel, {descendants: true})
  public models: QueryList<NgModel>;
  @ViewChild(NgForm)
  public form: NgForm;

  @Input()
  id: number;
  @Input()
  title: string = '';

  @Output()
  onSave = new EventEmitter<any>();
  @Output()
  onCancel = new EventEmitter();

  @Input()
  svgClasses: string = 'stroke-base-content';

  private _editMode:boolean = false;
  set editMode(value: boolean) {
    this._editMode = value;

    if(value) {
      //create delay before loading all inputs
      setTimeout(() => {
        let ngContentModels = this.models.toArray();
        ngContentModels.forEach((model) => {
          this.form.addControl(model);
        });
      });
    }
  }

  get editMode (): boolean {
    return this._editMode
  }

  constructor() {}

  async onSubmit(form: NgForm) {
    if(form.invalid) {
      NgFormExtension.markAllAsTouched(form);
      return;
    }

    this.onSave.emit(form.value);
    console.log(form.value)
    this.editMode = false;
  }

  setEditMode(value: boolean) {
    this.editMode = value;
  }

  cancelClick() {
    this.editMode = false;
    this.onCancel.emit();
  }
}

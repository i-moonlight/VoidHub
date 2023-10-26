import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-delete',
  templateUrl: './delete.component.html',
  styleUrls: ['./delete.component.css']
})
export class DeleteComponent {
  @Input()
  confirmContent: string = 'Are you sure?';

  @Output()
  onConfirm = new EventEmitter();

  @Input()
  height: string = '30px';

  @Input()
  width: string = '20px';

  constructor() {}

  confirmClick() {
    this.onConfirm.emit();
  }
}

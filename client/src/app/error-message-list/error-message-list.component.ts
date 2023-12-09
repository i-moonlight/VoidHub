
import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-error-message-list',
  templateUrl: './error-message-list.component.html',
  styleUrls: ['./error-message-list.component.css'],
  standalone: true,
  imports: [
    CommonModule
  ]
})
export class ErrorMessageListComponent {
  @Input("messages")
  errorMessages: string[] = [];
}

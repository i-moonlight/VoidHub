import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-closed-icon',
  templateUrl: './closed-icon.component.html',
  styleUrls: ['./closed-icon.component.css'],
  standalone: true,
})
export class ClosedIconComponent {
  @Input()
  height: string = '20px';

  @Input()
  width: string = '20px';
}

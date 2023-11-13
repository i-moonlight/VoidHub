import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-pinned-icon',
  templateUrl: './pinned-icon.component.html',
  styleUrls: ['./pinned-icon.component.css'],
  standalone: true,
})
export class PinnedIconComponent {

  @Input()
  height: string = '20px';

  @Input()
  width: string = '20px';

}

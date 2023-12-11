import { Component, Input } from '@angular/core';
import { PinnedIconComponent } from '../../shared/pinned-icon/pinned-icon.component';
import { ClosedIconComponent } from '../../shared/closed-icon/closed-icon.component';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { environment } from 'src/environments/environment';
import { TruncatePipe } from 'src/shared/truncate.pipe';

@Component({
  selector: 'app-topic-element',
  templateUrl: './topic-element.component.html',
  styleUrls: ['./topic-element.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    PinnedIconComponent,
    ClosedIconComponent,
    RouterModule,
    TruncatePipe
  ]
})
export class TopicElementComponent {
  @Input()
  topic;

  resourceUrl = environment.resourceURL;
}

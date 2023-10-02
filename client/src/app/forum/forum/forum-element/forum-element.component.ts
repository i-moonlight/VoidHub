import { Component, Input } from '@angular/core';
import { Forum } from '../../models/forum.model';

@Component({
  selector: 'app-forum',
  templateUrl: './forum-element.component.html',
  styleUrls: ['./forum-element.component.css']
})
export class ForumComponent {
  @Input()
  forum: Forum;
}

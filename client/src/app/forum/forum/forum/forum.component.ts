import { Component, Input } from '@angular/core';
import { Forum } from '../../models/forum.model';

@Component({
  selector: 'app-forum',
  templateUrl: './forum.component.html',
  styleUrls: ['./forum.component.css']
})
export class ForumComponent {
  @Input()
  forum: Forum;
}

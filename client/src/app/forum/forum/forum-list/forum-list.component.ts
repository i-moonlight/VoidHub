import { Component, Input } from '@angular/core';
import { Forum } from '../../models/forum.model';

@Component({
  selector: 'app-forum-list',
  templateUrl: './forum-list.component.html',
  styleUrls: ['./forum-list.component.css']
})
export class ForumListComponent {
  @Input()
  forums: Forum[] = [];
}

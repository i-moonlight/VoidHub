import { Component, Input } from '@angular/core';
import { Forum } from '../../models/forum.model';
import { environment as env } from 'src/environments/environment';


@Component({
  selector: 'app-forum-element',
  templateUrl: './forum-element.component.html',
  styleUrls: ['./forum-element.component.css']
})
export class ForumElementComponent {
  @Input()
  forum: Forum;

  resourceUrl = env.resourceURL;
}

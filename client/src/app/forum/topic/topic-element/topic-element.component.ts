import { Component, Input } from '@angular/core';
import { Topic } from '../../models/topic.model';

@Component({
  selector: 'app-topic-element',
  templateUrl: './topic-element.component.html',
  styleUrls: ['./topic-element.component.css']
})
export class TopicElementComponent {
  @Input()
  topic: Topic;
}

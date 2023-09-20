import { Component } from '@angular/core';
import { Topic } from '../../models/topic.model';

@Component({
  selector: 'app-topic',
  templateUrl: './topic.component.html',
  styleUrls: ['./topic.component.css']
})
export class TopicComponent {
  topic: Topic;
}

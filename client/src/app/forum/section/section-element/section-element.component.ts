import { Component, Input } from '@angular/core';
import { Section } from '../../models/section.model';

@Component({
  selector: 'app-section',
  templateUrl: './section-element.component.html',
  styleUrls: ['./section-element.component.css']
})
export class SectionElementComponent {

  @Input()
  section: Section;
}

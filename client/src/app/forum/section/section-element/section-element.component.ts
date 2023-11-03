import { Component, Input } from '@angular/core';
import { Section } from '../../models/section.model';
import { SectionService } from '../../services/section.service';
import { User } from 'src/shared/models/user.model';
import { Roles } from 'src/shared/roles.enum';


@Component({
  selector: 'app-section',
  templateUrl: './section-element.component.html',
  styleUrls: ['./section-element.component.css']
})
export class SectionElementComponent {

  @Input()
  section: Section;

  @Input()
  user: User;

  roles = Roles;

  editMode = false;

  constructor(private sectionService: SectionService) { }

  onEdit(data) {
    this.sectionService.updateSection(this.section.id, data)
    .subscribe({
      next: (section:any) => {
        this.section.title = section.title;
      }
    })
  }
}

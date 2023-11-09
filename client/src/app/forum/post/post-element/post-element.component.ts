import { Component, EventEmitter, Input, Output } from '@angular/core';
import { User } from 'src/shared/models/user.model';
import { PostService } from '../../services/post.service';
import { Roles } from 'src/shared/roles.enum';
import Editor from 'ckeditor5/build/ckeditor';

@Component({
  selector: 'app-post',
  templateUrl: './post-element.component.html',
  styleUrls: ['./post-element.component.css']
})
export class PostElementComponent {
  editor = Editor as {
    create: any;
  }

  @Input()
  post;

  @Input()
  user: User;

  @Input()
  enableDeliting: boolean = true;

  roles = Roles;

  @Output()
  onDelete = new EventEmitter();

  editMode = false;

  constructor(private postService: PostService) {}

  onPostEdit(data) {
    this.postService.updatePost(this.post.id, data).subscribe({
      next: (post: any) => {
        this.editMode = false;
        this.post.content = post.content;
      }
    })
  }

  setEditMode(value: boolean) {
    this.editMode = value;
  }

  deleteClick() {
    this.postService.deletePost(this.post.id).subscribe({
      next: _ => this.handleDelete(),
    });
  }

  onAdminDelete() {
    this.postService.deletePostByAdmin(this.post.id).subscribe({
      next: _ => this.handleDelete(),
    });
  }

  handleDelete() {
    this.onDelete.emit();
  }
}

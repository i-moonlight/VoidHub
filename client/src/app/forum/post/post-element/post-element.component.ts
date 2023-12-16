import { Component, EventEmitter, Input, Output } from '@angular/core';
import { User } from 'src/shared/models/user.model';
import { PostService } from '../../services/post.service';
import { Roles } from 'src/shared/roles.enum';
import Editor from 'ckeditor5/build/ckeditor';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-post',
  templateUrl: './post-element.component.html',
  styleUrls: ['./post-element.component.css']
})
export class PostElementComponent {
  editor = Editor as {create: any}
  resourceUrl = environment.resourceURL;

  @Input()
  post;

  @Input()
  user: User;

  @Input()
  enableDeliting: boolean = true;

  @Input()
  enableComments: boolean = true;

  @Input()
  isTopicClosed: boolean = false;

  @Input()
  depth = 1;

  roles = Roles;

  @Output()
  onDelete = new EventEmitter<number>();

  editMode = false;
  commentsMode = false;

  constructor(private postService: PostService) {

  }

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

  onAdminDelete() {
    this.postService.deletePost(this.post.id).subscribe({
      next: _ => this.handleDelete(),
    });
  }

  handleDelete() {
    this.onDelete.emit(this.post.id);
  }

  // comments
  toggleCommentsMode() {
    this.commentsMode = !this.commentsMode;
  }

  updateCommentsCounter(count) {
    this.post.commentsCount = count;
  }
}

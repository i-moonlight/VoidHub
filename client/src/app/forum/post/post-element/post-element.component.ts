import { Component, Input } from '@angular/core';
import { User } from 'src/shared/models/user.model';
import { PostService } from '../../services/post.service';

@Component({
  selector: 'app-post',
  templateUrl: './post-element.component.html',
  styleUrls: ['./post-element.component.css']
})
export class PostElementComponent {
  @Input()
  post;

  @Input()
  user: User;

  editMode = false;

  constructor(private postService: PostService) {}

  onPostEdit(data) {
    this.postService.updatePost(this.post.id, data).subscribe({
      next: (post: any) => {
        console.log(post);
        this.editMode = false;
        this.post.content = post.content;
      }
    })
  }

  setEditMode(value: boolean) {
    this.editMode = value;
  }
}

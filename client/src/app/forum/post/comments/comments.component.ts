import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { User } from 'src/shared/models/user.model';
import { PostService } from '../../services/post.service';
import { Offset } from 'src/shared/offset.model';
import { environment as env } from 'src/environments/environment';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.css']
})
export class CommentsComponent implements OnInit {

  @Input()
  user: User;

  @Input()
  topicId;

  @Input()
  postId;

  @Input()
  commentsCount = 0;

  @Input()
  depth = 1;
  depthLimit = env.commenthDepthLimit;

  @Output()
  onCommentsCounterUpdated = new EventEmitter<number>();

  @Output()
  onClose = new EventEmitter();

  posts:any = [];
  postOnPage = 5;

  editorContent = '';

  constructor(private postService: PostService) {}

  ngOnInit(): void {
    this.loadNexPosts();
  }

  loadNexPosts() {
    console.log('count ', this.commentsCount)
    console.log('count p', this.posts.length)
    if(this.posts.length >= this.commentsCount) {
      return;
    }

    let limit = this.commentsCount - this.posts.length;
    limit = limit > this.postOnPage ? this.postOnPage : limit;
    let offset = new Offset(this.posts.length, limit);

    this.postService.getComments(this.postId, offset).subscribe({
      next: (posts: any) => {
        this.posts.push(...posts);
      }
    });
  }

  onPostCreate(data) {
    this.postService.createPost(data).subscribe({
      next: _ => {
        this.onCommentsCounterUpdated.emit(this.commentsCount + 1);
        this.commentsCount += 1;
        this.loadNexPosts();

        //clear the editor
        this.editorContent = new Date().toString();
        setTimeout(() => this.editorContent = '', 0)
      }
    });
  }

  onPostDelete(postId: any) {
    this.posts.splice(this.posts.findIndex(p => p.id == postId), 1);
    this.onCommentsCounterUpdated.emit(this.commentsCount - 1);
    this.commentsCount -= 1;
  }

  onCloseClicked() {
    this.onClose.emit();
  }
}

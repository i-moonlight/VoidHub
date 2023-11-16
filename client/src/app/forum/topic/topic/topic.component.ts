import { Topic } from './../../models/topic.model';
import { TopicService } from './../../services/topic.service';
import { Component, OnDestroy } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ReplaySubject, takeUntil } from 'rxjs';
import { AuthService } from 'src/app/auth/auth.service';
import { User } from 'src/shared/models/user.model';
import { PostService } from '../../services/post.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Roles } from 'src/shared/roles.enum';
import { Page } from 'src/shared/page.model';


@Component({
  selector: 'app-topic',
  templateUrl: './topic.component.html',
  styleUrls: ['./topic.component.css']
})
export class TopicComponent implements OnDestroy {

  topic = null;
  posts: any[] = [];

  user: User = null;
  roles = Roles;

  postsOnPage = 5;
  currentPage = -1;
  topicId;

  newPostContent = '';

  private destroy$ = new ReplaySubject<boolean>();
  private firstTopicLoad = true;

  constructor(
    authService: AuthService,
    private topicService: TopicService,
    private postService: PostService,
    private router: Router,
    private route: ActivatedRoute) {

    authService.user$.pipe(takeUntil(this.destroy$))
      .subscribe(user => {this.user = user;})

    this.route.params.subscribe(async params => {
      await this.handleNewPage(params['page']);
      this.handleNewTopicId(params['id']);
    });
  }

  async handleNewTopicId(newTopicId: number) {
    this.firstTopicLoad = false;
    if(newTopicId == this.topicId)
      return;

    let offset = new Page(this.currentPage, this.postsOnPage).getOffset();
    this.topicId = newTopicId;

    if(+this.topicId) {
      this.topicService.getTopic(this.topicId, offset).subscribe({
        next: (topic:any) => {
          this.topic = topic;
          this.posts = topic.posts;
        }
      });
    }
  }

  async handleNewPage(newPage) {
    if(newPage == this.currentPage)
      return;

    this.currentPage = newPage;

    // skip loading if topic is not loaded yet
    if(!this.firstTopicLoad)
      this.loadNewPostsPage();
  }

  changePage(page: number) {
    this.router.navigate(['../', page], {relativeTo: this.route});
  }

  loadNewPostsPage() {
    let offset = new Page(this.currentPage, this.postsOnPage).getOffset();

    this.postService.getComments(this.topic?.post.id, offset)
      .subscribe({
        next: (posts:any[]) => {
          this.posts = [];
          this.posts.push(...posts);
        }
      });
  }

  //post methods
  onPostDelete() {
    this.topic.postsCount--;
    this.loadNewPostsPage();
  }

  onPostCreate(data) {;
    this.postService.createPost(data).subscribe({
      next: data => {
        this.topic.postsCount++;
        //because one-way binding
        this.newPostContent = new Date().toString();
        setTimeout(() => {this.newPostContent = ''});

        let page = this.topic.postsCount / this.postsOnPage;
        page = page % 1 > 0 ? Math.floor(page + 1) : page;
        if(page == this.currentPage)
          this.loadNewPostsPage();
        else
          this.changePage(page);
      }
    })
  }

  //topic methods

  onTopicEdit(data) {
    this.topicService
      .updateTopic(this.topic.id, data)
      .subscribe({
        next: topicResponse => {
          //also save old values
          this.topic = {
            ...this.topic,
            ...topicResponse
          }
        }
      })
  }

  onTopicDelete() {
    this.topicService.deleteTopic(this.topic.id).subscribe({
      next: () => {
        this.router.navigate(['/', 'forum', this.topic.forumId]);
      }
    });
  }

  ngOnDestroy(): void {
    this.destroy$.next(true);
    this.destroy$.unsubscribe();
  }
}

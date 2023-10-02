import { Component, OnDestroy } from '@angular/core';
import { Topic } from '../../models/topic.model';
import { User } from 'src/shared/models/user.model';
import { AuthService } from 'src/app/auth/auth.service';
import { ReplaySubject, takeUntil } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { ForumService } from '../../services/forum.service';
import { Forum } from '../../models/forum.model';

@Component({
  selector: 'app-topic-list',
  templateUrl: './topic-list.component.html',
  styleUrls: ['./topic-list.component.css']
})
export class TopicListComponent implements OnDestroy {
  topics: Topic[] = [
    {id: 1, title: 'Topic 1',createdAt: new Date(), isClosed: false},
    {id: 2, title: 'Topic 2',createdAt: new Date(), isClosed: true},
    {id: 3, title: 'Topic 3',createdAt: new Date(), isClosed: false},
    {id: 4, title: 'Topic 4',createdAt: new Date(), isClosed: true},
    {id: 5, title: 'Topic 5',createdAt: new Date(), isClosed: false},
  ];

  forum: Forum = null;

  user: User = null;
  private destroy$ = new ReplaySubject<boolean>(1);


  forumId: number = 0;
  currentPage: number = 1;
  topicsOnPage: number = 5;

  showNewTopic: boolean = false;

  constructor(
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router,
    private forumService: ForumService) {

    authService.user$.pipe(takeUntil(this.destroy$))
      .subscribe(user => {this.user = user;})

    route.params.subscribe(async params => {
      this.handleForumIdChange(params['id']);
      this.handlePageChange(params['page']);
    });
  }

  async handleForumIdChange(newForumId: number) {
    if(+newForumId) {
      if(this.forumId == newForumId) {
        return;
      }
      this.forumId = newForumId;

      this.forumService.getForum(this.forumId)
        .subscribe((forum: Forum) => {
          this.forum = forum;
        });

      this.loadTopicsPage(this.currentPage);
    }
  }

  async handlePageChange(newPage: number) {
    if(+newPage) {
      if(this.currentPage == newPage) {
        return;
      }

      this.currentPage = newPage;
      this.loadTopicsPage(this.currentPage);
    }
  }

  changePage(page: number) {
    this.router.navigate(['../', page], {relativeTo: this.route})
  }

  toggleNewTopic() {
    this.showNewTopic = !this.showNewTopic;
  }

  loadTopicsPage(page: number) {
    this.forumService
      .getForumTopics(this.forumId, {pageNumber: this.currentPage, pageSize: this.topicsOnPage})
      .subscribe((topics: Topic[]) => {
        this.topics = topics;
      });
  }

  onCreated() {
    this.toggleNewTopic();
    this.loadTopicsPage(this.forum.topicsCount / this.topicsOnPage);
    this.forum.topicsCount++;
    this.forum.postsCount++;
  }

  ngOnDestroy(): void {
    this.destroy$.next(true);
    this.destroy$.complete();
  }
}

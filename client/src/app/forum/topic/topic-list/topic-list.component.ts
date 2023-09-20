import { Component, OnDestroy } from '@angular/core';
import { Topic } from '../../models/topic.model';
import { User } from 'src/shared/models/user.model';
import { AuthService } from 'src/app/auth/auth.service';
import { ReplaySubject, takeUntil } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';

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

  user: User = null;
  private destroy$ = new ReplaySubject<boolean>(1);

  showNewTopic: boolean = false;

  forumId: number;
  currentPage: number = 1;

  constructor(
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router) {

    authService.user$.pipe(takeUntil(this.destroy$))
      .subscribe(user => {this.user = user;})

    route.params.subscribe(params => {
      this.forumId = params['id'];
      this.currentPage = params['page'];
    });
  }

  changePage(page: number) {
    this.router.navigate(['../', page], {relativeTo: this.route})
  }

  toggleNewTopic() {
    console.log(1)
    this.showNewTopic = !this.showNewTopic;
  }

  ngOnDestroy(): void {
    this.destroy$.next(true);
    this.destroy$.complete();
  }
}

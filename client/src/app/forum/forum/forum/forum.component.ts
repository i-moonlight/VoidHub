import { Component, OnDestroy } from '@angular/core';
import { Topic } from '../../models/topic.model';
import { User } from 'src/shared/models/user.model';
import { AuthService } from 'src/app/auth/auth.service';
import { ReplaySubject, takeUntil } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { ForumService } from '../../services/forum.service';
import { Forum } from '../../models/forum.model';
import { Roles } from 'src/shared/roles.enum';
import { Page } from 'src/shared/page.model';
import { ToastrService } from 'ngx-toastr';
import { ToastrExtension } from 'src/shared/toastr.extension';


@Component({
  selector: 'app-forum',
  templateUrl: './forum.component.html',
  styleUrls: ['./forum.component.css']
})
export class ForumComponent implements OnDestroy {
  topics: Topic[] = [];
  forum: Forum = null;

  user: User = null;
  private destroy$ = new ReplaySubject<boolean>(1);

  forumId: number = 0;
  currentPage: number = 0;
  topicsOnPage: number = 5;

  showNewTopic: boolean = false;

  roles = Roles;

  constructor(
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router,
    private forumService: ForumService,
    private toastr: ToastrService) {

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
    }
  }

  async handlePageChange(newPage: number) {
    if(+newPage) {
      if(this.currentPage == newPage) {
        return;
      }

      this.currentPage = newPage;
      this.loadTopicsPage();
    }
  }

  changePage(page: number) {
    this.router.navigate(['../', page], {relativeTo: this.route})
  }

  toggleNewTopic() {
    this.showNewTopic = !this.showNewTopic;
  }

  loadTopicsPage() {
    let page = new Page(this.currentPage, this.topicsOnPage);
    this.topics = [];

    this.forumService
      .getForumTopics(this.forumId, page)
      .subscribe((topics: Topic[]) => {
        this.topics = topics;
      });
  }

  onEdit(data) {
    this.forumService.updateForum(this.forum.id, data)
      .subscribe({
        next: (forum:any) => {
          this.forum.title = forum.title;
          this.toastr.success('Forum updated');
        },
        error: errs => {
          ToastrExtension.handleErrors(this.toastr, errs);
        }

      })
  }

  onDelete() {
    this.forumService.deleteForum(this.forum.id).subscribe({
      next: () => {
        this.router.navigate(['../../'], {relativeTo: this.route});
      }
    })
  }

  onCreated() {
    this.forum.topicsCount++;
    this.forum.postsCount++;
    this.toggleNewTopic();
    this.loadTopicsPage();
  }

  ngOnDestroy(): void {
    this.destroy$.next(true);
    this.destroy$.complete();
  }
}

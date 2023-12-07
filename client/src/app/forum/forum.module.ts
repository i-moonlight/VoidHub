import { NgModule } from "@angular/core";
import { ForumListComponent } from "./forum/forum-list/forum-list.component";
import { ForumElementComponent } from "./forum/forum-element/forum-element.component";
import { SectionListComponent } from "./section/section-list/section-list.component";
import { SectionElementComponent } from "./section/section-element/section-element.component";
import { TopicElementComponent } from "./topic/topic-element/topic-element.component";
import { PostElementComponent } from "./post/post-element/post-element.component";
import { RouterModule } from "@angular/router";
import { MainComponent } from './main/main.component';
import { TopicComponent } from './topic/topic/topic.component';
import { NewSectionComponent } from './section/new-section/new-section.component';
import { SharedModule } from "src/shared/shared.module";
import { ErrorMessageListComponent } from "../error-message-list/error-message-list.component";
import { SectionService } from "./services/section.service";
import { NewForumComponent } from './forum/new-forum/new-forum.component';
import { ForumService } from "./services/forum.service";
import { NewTopicComponent } from './topic/new-topic/new-topic.component';
import { TopicService } from "./services/topic.service";
import { PaginatorComponent } from './paginator/paginator.component';
import { PostService } from "./services/post.service";
import { NewPostComponent } from './post/post-editor/post-editor.component';
import { DeleteComponent } from './shared/delete/delete.component';
import { TitleEditorComponent } from './title-editor/title-editor.component';
import { ForumComponent } from "./forum/forum/forum.component";
import { PinnedIconComponent } from './shared/pinned-icon/pinned-icon.component';
import { ClosedIconComponent } from './shared/closed-icon/closed-icon.component';
import { AdminService } from "./admin/services/admin.service";
import { BanService } from "./admin/services/ban.service";
import { CKEditorModule } from "@ckeditor/ckeditor5-angular";
import { CommentsComponent } from './post/comments/comments.component';
import { AccountService } from "./services/account.service";
import { RecentComponent } from "./recent/recent.component";
import { ReducePost } from "./recent/reduce-post.pipe";

@NgModule({
  providers: [
    SectionService,
    ForumService,
    TopicService,
    PostService,
    BanService,
    AdminService,
    AccountService
  ],
  declarations: [
    SectionListComponent,
    SectionElementComponent,
    ForumListComponent,
    PostElementComponent,
    MainComponent,
    TopicComponent,
    NewSectionComponent,
    NewForumComponent,
    NewTopicComponent,
    NewPostComponent,
    DeleteComponent,
    TitleEditorComponent,
    ForumComponent,
    ForumElementComponent,
    CommentsComponent,
    RecentComponent
  ],
  imports: [
    SharedModule,
    CKEditorModule,
    ErrorMessageListComponent,
    PaginatorComponent,
    TopicElementComponent,
    PinnedIconComponent,
    ClosedIconComponent,
    ReducePost,
    RouterModule.forChild([
        {path: '', component: MainComponent, children: [
          {path: '', redirectTo: 'sections', pathMatch: 'full'},
          {path:'sections', component: SectionListComponent},
          {path:'new-section', component: NewSectionComponent},
          {path:'recent', component: RecentComponent},
          {path:'section/:id/new-forum', component: NewForumComponent},
          {path:'topic/:id/:page', component: TopicComponent},
          {path:'topic/:id', redirectTo: 'topic/:id/1', pathMatch: 'full'},
          {path:'search', loadChildren: () => import('./search/search.module').then(m => m.SearchModule)},
          {path:'admin-panel', loadChildren: () => import('./admin/admin-panel.module').then(m => m.AdminPanelModule)},
          {path:':id', redirectTo: ':id/1', pathMatch: 'full'},
          {path:':id/:page', component: ForumComponent},
        ]}
    ])
  ],
  exports: [
  ]
})
export class ForumModule{}

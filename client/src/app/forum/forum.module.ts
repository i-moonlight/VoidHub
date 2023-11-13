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
import { BanMenuComponent } from './admin/ban-menu/ban-menu.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { AdminService } from "./admin/admin.service";
import { BanService } from "./services/ban.service";
import { canActivateAdmin } from "./admin/admin.guard";
import { CKEditorModule } from "@ckeditor/ckeditor5-angular";
import { CommentsComponent } from './post/comments/comments.component';

@NgModule({
  providers: [
    SectionService,
    ForumService,
    TopicService,
    PostService,
    BanService,
    AdminService,
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
    BanMenuComponent,
    AdminPanelComponent,
    CommentsComponent,
  ],
  imports: [
    SharedModule,
    CKEditorModule,
    ErrorMessageListComponent,
    PaginatorComponent,
    TopicElementComponent,
    PinnedIconComponent,
    ClosedIconComponent,
    RouterModule.forChild([
        {path: '', component: MainComponent, children: [
          {path: '', redirectTo: 'sections', pathMatch: 'full'},
          {path:'sections', component: SectionListComponent},
          {path:'new-section', component: NewSectionComponent},
          {path:'section/:id/new-forum', component: NewForumComponent},
          {path:'topic/:id/:page', component: TopicComponent},
          {path:'topic/:id', redirectTo: 'topic/:id/1', pathMatch: 'full'},
          {path:'admin-panel', component: AdminPanelComponent, canActivate:[canActivateAdmin],  children: [
            {path:'ban-menu', component: BanMenuComponent}
          ]},
          {path:'search', loadChildren: () => import('./search/search.module').then(m => m.SearchModule)},
          {path:':id', redirectTo: ':id/1', pathMatch: 'full'},
          {path:':id/:page', component: ForumComponent},
        ]}
    ])
  ],
  exports: [
  ]
})
export class ForumModule{}

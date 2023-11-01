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
    TopicElementComponent,
    PostElementComponent,
    MainComponent,
    TopicComponent,
    NewSectionComponent,
    NewForumComponent,
    NewTopicComponent,
    PaginatorComponent,
    NewPostComponent,
    DeleteComponent,
    TitleEditorComponent,
    ForumComponent,
    ForumElementComponent,
    PinnedIconComponent,
    ClosedIconComponent,
    BanMenuComponent,
    AdminPanelComponent
  ],
  imports: [
    SharedModule,
    ErrorMessageListComponent,
    RouterModule.forChild([
      {path: 'forum', redirectTo: 'forum/sections', pathMatch: 'full'},
      {path:'forum', component: MainComponent, children: [
        {path:'sections', component: SectionListComponent},
        {path:'new-section', component: NewSectionComponent},
        {path:'section/:id/new-forum', component: NewForumComponent},
        {path:'topic/:id/:page', component: TopicComponent},
        {path:'topic/:id', redirectTo: 'topic/:id/1', pathMatch: 'full'},
        {path:'admin-panel', component: AdminPanelComponent, canActivate:[canActivateAdmin],  children: [
          {path:'ban-menu', component: BanMenuComponent}
        ]},
        {path:':id', redirectTo: ':id/1', pathMatch: 'full'},
        {path:':id/:page', component: ForumComponent},
      ]},
    ])
  ],
  exports: [

  ]
})
export class ForumModule{}

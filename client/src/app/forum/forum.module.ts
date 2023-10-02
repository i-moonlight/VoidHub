import { NgModule } from "@angular/core";
import { ForumListComponent } from "./forum/forum-list/forum-list.component";
import { ForumComponent } from "./forum/forum-element/forum-element.component";
import { SectionListComponent } from "./section/section-list/section-list.component";
import { SectionElementComponent } from "./section/section-element/section-element.component";
import { TopicListComponent } from "./topic/topic-list/topic-list.component";
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

@NgModule({
  providers: [
    SectionService,
    ForumService,
    TopicService,
    PostService
  ],
  declarations: [
    SectionListComponent,
    SectionElementComponent,
    ForumListComponent,
    ForumComponent,
    TopicListComponent,
    TopicElementComponent,
    PostElementComponent,
    MainComponent,
    TopicComponent,
    NewSectionComponent,
    NewForumComponent,
    NewTopicComponent,
    PaginatorComponent,
    NewPostComponent,
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
        {path:':id', redirectTo: ':id/1', pathMatch: 'full'},
        {path:':id/:page', component: TopicListComponent},
      ]},
    ])
  ],
  exports: [

  ]
})
export class ForumModule{}

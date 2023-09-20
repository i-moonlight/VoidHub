import { NgModule } from "@angular/core";
import { ForumListComponent } from "./forum/forum-list/forum-list.component";
import { ForumComponent } from "./forum/forum/forum.component";
import { SectionListComponent } from "./section/section-list/section-list.component";
import { SectionElementComponent } from "./section/section-element/section-element.component";
import { TopicListComponent } from "./topic/topic-list/topic-list.component";
import { TopicElementComponent } from "./topic/topic-element/topic-element.component";
import { PostListComponent } from "./post/post-list/post-list.component";
import { PostComponent } from "./post/post/post.component";
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

@NgModule({
  providers: [
    SectionService,
    ForumService,
    TopicService
  ],
  declarations: [
    SectionListComponent,
    SectionElementComponent,
    ForumListComponent,
    ForumComponent,
    TopicListComponent,
    TopicElementComponent,
    PostListComponent,
    PostComponent,
    MainComponent,
    TopicComponent,
    NewSectionComponent,
    NewForumComponent,
    NewTopicComponent,
    PaginatorComponent,
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
        {path:'forum/:id', redirectTo: 'forum/:id/1', pathMatch: 'full'},
        {path:'forum/:id/:page', component: TopicListComponent},
        {path:'topic/:id', component: TopicComponent}
      ]},
    ])
  ],
  exports: [

  ]
})
export class ForumModule{}

import { NgModule } from "@angular/core";
import { SearchBarComponent } from "./search-bar/search-bar.component";
import { SearchService } from "../services/search.service";
import { SearchComponent } from "./search/search.component";
import { ErrorMessageListComponent } from "src/app/error-message-list/error-message-list.component";
import { LimitLoaderComponent } from "src/app/limitter/limit-loader/limit-loader.component";
import { SharedModule } from "src/shared/shared.module";
import { PaginatorComponent } from "../paginator/paginator.component";
import { TopicElementComponent } from "../topic/topic-element/topic-element.component";

@NgModule({
  declarations: [
    SearchComponent,
  ],
  imports: [
    SharedModule,
    SearchBarComponent,
    ErrorMessageListComponent,
    PaginatorComponent,
    TopicElementComponent
  ],
  providers: [
    SearchService,
  ],
  exports: []
})
export class SearchModule {}

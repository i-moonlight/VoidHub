import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { RouterModule } from "@angular/router";
import { TitleEditorComponent } from "src/app/forum/title-editor/title-editor.component";
import { LimitLoaderComponent } from "src/app/limitter/limit-loader/limit-loader.component";

@NgModule({
  declarations: [
    LimitLoaderComponent
  ],
  imports:[
    FormsModule,
    CommonModule,
    RouterModule,
  ],
  exports:[
    FormsModule,
    CommonModule,
    RouterModule,
    LimitLoaderComponent
  ]
})
export class SharedModule{}

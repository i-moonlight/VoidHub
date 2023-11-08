import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { RouterModule } from "@angular/router";
import { QuillModule } from "ngx-quill";
import { LimitLoaderComponent } from "src/app/limitter/limit-loader/limit-loader.component";

@NgModule({
  declarations: [
    LimitLoaderComponent
  ],
  imports:[
    FormsModule,
    CommonModule,
    BrowserModule,
    RouterModule,
    QuillModule
  ],
  exports:[
    FormsModule,
    CommonModule,
    RouterModule,
    QuillModule,
    LimitLoaderComponent
  ]
})
export class SharedModule{}

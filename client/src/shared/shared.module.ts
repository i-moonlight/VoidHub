import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { RouterModule } from "@angular/router";
import { QuillModule } from "ngx-quill";
import { ToastrModule } from "ngx-toastr";

@NgModule({
  declarations: [
  ],
  imports:[
    FormsModule,
    CommonModule,
    BrowserModule,
    RouterModule,
    QuillModule,
  ],
  exports:[
    FormsModule,
    CommonModule,
    RouterModule,
    QuillModule
  ]
})
export class SharedModule{}

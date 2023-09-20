import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { RouterModule } from "@angular/router";

@NgModule({
  declarations: [
  ],
  imports:[
    FormsModule,
    CommonModule,
    BrowserModule,
    RouterModule
  ],
  exports:[
    FormsModule,
    CommonModule,
    RouterModule
  ]
})
export class SharedModule{}

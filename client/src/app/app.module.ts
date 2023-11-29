import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule, ToastrService } from 'ngx-toastr';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import { HeaderComponent } from './header/header.component';
import { AuthModule } from './auth/auth.module';
import { HomeComponent } from './home/home.component';
import { ForumModule } from './forum/forum.module';
import { SharedModule } from 'src/shared/shared.module';
import { AuthInterceptor } from './auth/auth.interceptor';
import { HttpExceptionInterceptor } from 'src/shared/error/http-exception.interceptor';
import { RouterModule } from '@angular/router';
import { LimitterInterceptor } from 'src/app/limitter/limitter.interceptor';
import { LimitterService } from './limitter/limitter.service';
import { SearchBarComponent } from './forum/search/search-bar/search-bar.component';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HomeComponent,
  ],
  imports: [
    SharedModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    AuthModule,
    SearchBarComponent,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    RouterModule.forChild([
      {path: '**', component: HomeComponent, pathMatch: 'full'},
    ])
  ],
  providers: [
    LimitterService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpExceptionInterceptor,
      deps: [ToastrService],
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LimitterInterceptor,
      deps: [LimitterService],
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import { HeaderComponent } from './header/header.component';
import { FormsModule } from '@angular/forms';
import { AuthModule } from './auth/auth.module';
import { HomeComponent } from './home/home.component';
import { ForumModule } from './forum/forum.module';
import { SharedModule } from 'src/shared/shared.module';
import { AuthInterceptor } from './auth/auth.interceptor';
import { HttpExceptionInterceptor } from 'src/shared/error/http-exception.interceptor';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HomeComponent
  ],
  imports: [
    SharedModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    AuthModule,
    ForumModule,
    RouterModule.forChild([
      {path: '**', component: HomeComponent, pathMatch: 'full'}
    ])
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpExceptionInterceptor,
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

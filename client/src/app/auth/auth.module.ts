import { APP_INITIALIZER, NgModule } from "@angular/core";
import { AuthService } from "./auth.service";
import { LoginComponent } from "./login/login.component";
import { RegisterComponent } from "./register/register.component";
import { RouterModule } from "@angular/router";
import { SharedModule } from "src/shared/shared.module";
import { ErrorMessageListComponent } from "../error-message-list/error-message-list.component";

function onAppLoad(authService: AuthService): () => Promise<any> {
  return async () => {
    const user = localStorage.getItem('user')
    if(user) {
      try {
        const parsedUser = JSON.parse(user);
        authService.setUser(parsedUser);
      } catch(err) {
        console.log('Error while trying to auto-login user', err)
      }
    }

    return;
  };
}

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    SharedModule,
    ErrorMessageListComponent,
    RouterModule.forChild([
      {path: 'login', component: LoginComponent},
      {path: 'register', component: RegisterComponent}
    ])
  ],
  exports: [
    LoginComponent,
    RegisterComponent
  ],
  providers: [
    AuthService,
    {
      provide: APP_INITIALIZER,
      useFactory: onAppLoad,
      deps: [AuthService],
      multi: true
    }
  ],
})
export class AuthModule {}

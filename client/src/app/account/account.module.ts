import { NgModule } from "@angular/core";
import { ProfileComponent } from "./profile/profile.component";
import { SettingsComponent } from "./settings/settings.component";
import { AccountService } from "./account.service";
import { SharedModule } from "src/shared/shared.module";
import { RouterModule } from "@angular/router";
import { canActivateSelf } from "./self.guard";
import { AdminComponentsModule } from "../forum/admin/admin-components.module";
import { BanMenuComponent } from "../forum/admin/ban-menu/ban-menu.component";
import { RoleMenuComponent } from "../forum/admin/role-menu/role-menu.component";
import { canActivateAdmin, canActivateModer } from "../forum/admin/role.guard";
import { ErrorMessageListComponent } from "../error-message-list/error-message-list.component";
import { RenameMenuComponent } from "../forum/admin/rename-menu/rename-menu.component";

@NgModule({
  declarations: [
    ProfileComponent,
    SettingsComponent,
  ],
  imports: [
    SharedModule,
    AdminComponentsModule,
    ErrorMessageListComponent,
    RouterModule.forChild([
      {path: 'settings', component: SettingsComponent, canActivate: [canActivateSelf]},
      {path: '', component: ProfileComponent},
      {path: ':id', component: ProfileComponent, children: [
        {path: 'ban-menu', component: BanMenuComponent, canActivate: [canActivateAdmin]},
        {path: 'role-menu', component: RoleMenuComponent, canActivate: [canActivateAdmin]},
        {path: 'rename-menu', component: RenameMenuComponent, canActivate: [canActivateModer]},
      ]},
    ])
  ],
  providers: [
    AccountService
  ]
})
export class AccountModule {}

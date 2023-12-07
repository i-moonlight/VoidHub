import { NgModule } from "@angular/core";
import { SharedModule } from "src/shared/shared.module";
import { BanMenuComponent } from "./ban-menu/ban-menu.component";
import { RoleMenuComponent } from "./role-menu/role-menu.component";
import { AdminService } from "./services/admin.service";
import { BanService } from "./services/ban.service";
import { ErrorMessageListComponent } from "src/app/error-message-list/error-message-list.component";
import { AdminPanelComponent } from "./admin-panel/admin-panel.component";
import { AccountService } from "../services/account.service";

@NgModule({
  declarations: [
    BanMenuComponent,
    RoleMenuComponent,
    AdminPanelComponent
  ],
  imports: [
    SharedModule,
    ErrorMessageListComponent
  ],
  providers: [
    AccountService,
    BanService,
    AdminService
  ],
  exports: [
    BanMenuComponent,
    RoleMenuComponent,
    AdminPanelComponent
  ]
})
export class AdminComponentsModule {}

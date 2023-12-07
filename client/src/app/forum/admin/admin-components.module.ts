import { NgModule } from "@angular/core";
import { SharedModule } from "src/shared/shared.module";
import { BanMenuComponent } from "./ban-menu/ban-menu.component";
import { RoleMenuComponent } from "./role-menu/role-menu.component";
import { AdminService } from "./services/admin.service";
import { BanService } from "./services/ban.service";
import { ErrorMessageListComponent } from "src/app/error-message-list/error-message-list.component";

@NgModule({
  declarations: [
    BanMenuComponent,
    RoleMenuComponent,
  ],
  imports: [
    SharedModule,
    ErrorMessageListComponent
  ],
  providers: [
    AdminService,
    BanService
  ],
  exports: [
    BanMenuComponent,
    RoleMenuComponent
  ]
})
export class AdminComponentsModule {}

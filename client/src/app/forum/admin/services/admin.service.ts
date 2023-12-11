import { Injectable } from "@angular/core";
import { Subject } from "rxjs";
import { User } from "src/shared/models/user.model";

@Injectable()
export class AdminService {
  public cancelClicked = new Subject<void>();
  public user: User | null = null;
  public userIdBlocked: boolean;
}

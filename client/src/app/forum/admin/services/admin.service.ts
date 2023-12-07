import { Injectable } from "@angular/core";
import { Subject } from "rxjs";

@Injectable()
export class AdminService {
  public cancelClicked = new Subject<void>();
  public userId: string | null = null;
  public userIdBlocked: boolean;
}

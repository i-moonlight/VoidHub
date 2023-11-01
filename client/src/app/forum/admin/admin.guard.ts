import { inject } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivateChildFn, CanActivateFn, Router, RouterStateSnapshot } from "@angular/router";
import { map, take } from "rxjs";
import { AuthService } from "src/app/auth/auth.service";
import { User } from "src/shared/models/user.model";
import { Roles } from "src/shared/roles.enum";

export const canActivateAdmin: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  return authService.user$.pipe(take(1), map((user:User) => {
    if (!user || user.role == Roles.USER) {
      router.navigate(["../"]);
      return false;
    }

    return true;
  }));
};

export const canActivateAdminChild: CanActivateChildFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => canActivateAdmin(route, state);

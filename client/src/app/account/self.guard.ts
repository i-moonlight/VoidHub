import { inject } from "@angular/core";
import { ActivatedRoute, ActivatedRouteSnapshot, CanActivateChildFn, CanActivateFn, Router, RouterStateSnapshot } from "@angular/router";
import { map, take } from "rxjs";
import { AuthService } from "src/app/auth/auth.service";
import { User } from "src/shared/models/user.model";

export const canActivateSelf: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  return authService.user$.pipe(take(1), map((user:User) => {
    if (!user) {
      router.navigate(["../"]);
      return false;
    }

    return true;
  }));
};

export const canActivateSelfChild: CanActivateChildFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => canActivateSelf(route, state);

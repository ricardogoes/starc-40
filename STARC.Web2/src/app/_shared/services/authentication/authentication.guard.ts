import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';


import { AuthService } from './authentication.service';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(
      private router: Router,
      private authService: AuthService
    ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        if (this.authService.checkLogin()) {
            //TODO: implementar codigo para não acessar diretamente pela url
            return true;
        }

        // not logged in so redirect to login page with the return url
        alert('User not authenticated');        
        this.router.navigate(['/login'], { queryParams: { returnUrl: state.url }});
        return false;
    }
}
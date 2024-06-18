import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { ApiService } from './api-service.service';
import { Role } from './models/user.model';
import { AuthService } from './auth-service.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {
  userRoles: Role[] | undefined = []
  constructor(private apiService: ApiService, private router: Router, private authService: AuthService) {

  }

  async canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Promise<boolean> {
      const expectedRoles = next.data['expectedRoles'];

      if (!this.authService.isAuthenticated()) {
        return false;
      }
      
      var user = await this.apiService.getUserAsync();

      this.userRoles = user?.roles;

      if (this.userRoles?.length === 0 && expectedRoles.length === 0) {
        return true;
      }
      if (!this.userRoles?.some(role => expectedRoles.includes(role))) {
        this.router.navigate(['/']);
        return false;
      }
      return true;
  }

}
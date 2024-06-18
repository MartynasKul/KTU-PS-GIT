import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './auth-service.service';
import { ApiService } from './api-service.service';
import { User } from './models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService,
    private apiService: ApiService,
    private router: Router) {
}

  canActivate(): boolean {
    if (localStorage.getItem('token')) {
      // User is logged in, so return true
      return true;
    }

    // User is not logged in, so redirect to login page
    this.router.navigate(['/login']);
    return false;
  }
}
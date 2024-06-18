import { Component } from '@angular/core';
import { AuthService } from './auth-service.service';
import { NavigationEnd, Router } from '@angular/router';
import { ApiService } from './api-service.service';
import { UserType } from './models/user.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'portal-frontend';
  isAdmin: Boolean = false;
  hideButton = true;
  checked: Boolean = false;
  constructor(private authService: AuthService, private router: Router, private apiService: ApiService) {
    if (authService.isAuthenticated()) {
      this.isUserAdmin();
    }
    this.router.events.subscribe((event) => {
      if (event) {
        if (this.authService.isAuthenticated()) {
          if (!this.checked) {
            this.checked = true;
            this.isUserAdmin();
          }
        } else {
          this.checked = false;
          this.isAdmin = false;
        }
        if (this.router.url === '/create-organization') {
          this.hideButton = true;
        } else {
          this.hideButton = !this.authService.isAuthenticated();
        }
      }
    });
  }

  ngOnInit() {
    this.authService.validateToken();
    this.isUserAdmin();
      
    this.forceToCreateOrganization()

    setInterval(() => {
      this.authService.validateToken();
    }, 3 * 60 * 1000);
  }

  forceToCreateOrganization() {
    if (this.authService.isAuthenticated()) {
      this.apiService.getUser().subscribe(user => {
        if (user.organizationId === null) {
          this.router.navigate(['/create-organization']);
        }
      });
      }
  }

  isUserAdmin() {
    if (this.authService.isAuthenticated()) {
      
      this.apiService.getUser().subscribe(user => {
        if (user.roles.length === 2) {
          this.isAdmin = true;
        } else {
          this.isAdmin = false;
        }
      })
    }
  }
}

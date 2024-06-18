import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './auth.guard';
import { AppComponent } from './app.component';
import { OrganizationSettingsComponent } from './organization-settings/organization-settings.component';
import { ProjectSettingsComponent } from './project-settings/project-settings.component';
import { RegistrationComponent } from "./registration/registration.component";
import { CreateOrganizationComponent } from './create-organization/create-organization.component';
import { RoleGuard } from './role-guard.service';
import { HomeComponent } from './home/home.component';
import { BusinessInfoComponent } from './business-info/business-info.component';
import { ProfilePageComponent } from './profile-page/profile-page.component';
import { SponsorPageComponent } from './sponsor-page/sponsor-page.component';
import { EmailsComponent } from './emails/emails.component';
import { LogoutComponent } from './logout/logout.component';
import { NotFoundComponent } from './not-found/not-found.component';


const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard]},
  { path: 'organization-settings', component:OrganizationSettingsComponent, canActivate: [AuthGuard, RoleGuard], data: { expectedRoles: ["Administratorius"] }},
  { path: 'project-settings', component:ProjectSettingsComponent, canActivate: [AuthGuard]},
  { path: 'login', component: LoginComponent },
  { path: 'logout', component: LogoutComponent },
  { path: 'register', component: RegistrationComponent },
  { path: 'business-info/:id', component: BusinessInfoComponent,  canActivate: [AuthGuard]},
  { path: 'create-organization', component: CreateOrganizationComponent, canActivate: [AuthGuard, RoleGuard], data: { expectedRoles: [] }},
  { path: 'profile', component: ProfilePageComponent, canActivate: [AuthGuard]},
  { path: 'sponsor-page', component:SponsorPageComponent, canActivate: [AuthGuard]},
  { path: 'emails', component:EmailsComponent, canActivate: [AuthGuard]},
  { path: 'not-found', component: NotFoundComponent },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

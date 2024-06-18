import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginComponent } from './login/login.component';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { AuthGuard } from './auth.guard';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ApiService } from './api-service.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthService } from './auth-service.service';
import { AuthInterceptor } from './auth-interceptor';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatSelectModule } from '@angular/material/select';
import { MatSortModule } from '@angular/material/sort';
import { MatDialogModule } from '@angular/material/dialog';

import { OrganizationSettingsComponent } from './organization-settings/organization-settings.component';
import { AddUserModalComponent } from './add-user-modal/add-user-modal.component';
import { AddProjectUserModalComponent } from './add-projectuser-modal/add-projectuser-modal.component';
import { AddProjectModalComponent } from './add-project-modal/add-project-modal.component';
import { RegistrationComponent } from './registration/registration.component';
import { CreateOrganizationComponent } from './create-organization/create-organization.component';
import { HomeComponent } from './home/home.component';
import { BusinessInfoComponent } from './business-info/business-info.component';
import { ProfilePageComponent } from './profile-page/profile-page.component';
import { SponsorPageComponent } from './sponsor-page/sponsor-page.component';
import { EmailsComponent } from './emails/emails.component';
import { LogoutComponent } from './logout/logout.component';
import { ProjectSettingsComponent } from './project-settings/project-settings.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { SendEmailModalComponent } from './send-email-modal/send-email-modal.component';
import { EmailDisplayModalComponent } from './email-display-modal/email-display-modal.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { AddSponsorModalComponent } from './add-sponsor-modal/add-sponsor-modal.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { AddProjectSponsorModalComponent } from './add-project-sponsor-modal/add-project-sponsor-modal.component';
import { DatePipe } from '@angular/common';
import { AddCommentModalComponent } from './add-comment-modal/add-comment-modal.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    OrganizationSettingsComponent,
    AddProjectUserModalComponent,
    ProjectSettingsComponent,
    HomeComponent,
    BusinessInfoComponent,
    ProfilePageComponent,
    AddUserModalComponent,
    AddProjectModalComponent,
    RegistrationComponent,
    CreateOrganizationComponent,
    SponsorPageComponent,
    EmailsComponent,
    LogoutComponent,
    SendEmailModalComponent,
    EmailDisplayModalComponent,
    NotFoundComponent,
    AddSponsorModalComponent,
    AddProjectSponsorModalComponent,
    AddCommentModalComponent
  ],
  imports: [
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    MatSidenavModule,
    MatToolbarModule,
    MatDividerModule,
    MatTableModule,
    MatSelectModule,
    MatSortModule,
    MatIconModule,
    MatDialogModule,
    MatExpansionModule,
    ReactiveFormsModule,
    CKEditorModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }, AuthGuard, ApiService, AuthService, DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }

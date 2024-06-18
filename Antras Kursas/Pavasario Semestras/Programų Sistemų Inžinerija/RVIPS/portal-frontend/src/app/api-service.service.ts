import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User, UserType } from './models/user.model';
import { map } from 'rxjs';
import { Organization } from './models/organization.model';
import { Sponsor } from './models/sponsor.model'
import { Validators } from '@angular/forms';
import { Email } from './models/email.model';
import { Project } from './models/project.model';
import { OutgoingEmail } from './models/outgoingemail.model';
import { AddSponsorRequest } from './models/addsponsor.model';
import { SponsorComment } from './models/comment.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

// Inject the HttpClient module in your component or service constructor
constructor(private http: HttpClient) {}

// Create a function to send the HTTP POST request
login(email: string, password: string) {
  const url = `api/login`;

  // Define the request headers
  var options = {
    headers: new HttpHeaders({
       'Accept':'text/plain',
       'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Methods': 'GET, POST, PUT, DELETE, OPTIONS',
        'Access-Control-Allow-Headers': 'Origin, X-Requested-With, Content-Type, Accept, Authorization',
        'Allow': 'GET, POST, PUT, DELETE, HEAD, OPTIONS, TRACE'
    }),
    'responseType': 'text' as 'json'
 }

  // Define the request body
  const body = {
    email: email,
    password: password
  };

  // Send the HTTP POST request
  return this.http.post<string>(url, body, options);
}

getOrganization() {
  const url = `api/organization`;

  var respone = this.http.get<Organization>(url).pipe(
    map((org => {
      const organization: Organization = {
        id: org.id,
        title: org.title,

        imapServer: org.imapServer,
        imapServerPort: org.imapServerPort,
        imapServerUserName: org.imapServerUserName,
        
        smtpServer: org.smtpServer,
        smtpServerPort: org.smtpServerPort,
        smtpServerUserName: org.smtpServerUserName
      };
      return organization;
    })))

  return respone;
}

getSponsor(id: number) {
  const url = `api/sponsor/${id}`

  var respone = this.http.get<Sponsor>(url).pipe(
    map((sponsor: any) => {
      const displaySponsor: Sponsor = {
        id: sponsor.id,
        sponsorName: sponsor.sponsorName,
        telephoneNumber: sponsor.telephoneNumber,
        email: sponsor.email,
        address: sponsor.address,
        description: sponsor.description,
      };
      return displaySponsor;
    }))
    return respone;
}

addSponsor(request: AddSponsorRequest) {
  const url = `api/sponsor`

    return this.http.post(url, request);
}

getSponsors() {
  const url = `api/sponsor/list`

  var respone = this.http.get<Sponsor[]>(url).pipe(
    map((sponsors: any[]) => sponsors.map(sponsor => {
      const displaySponsor: Sponsor = {
        id: sponsor.id,
        sponsorName: sponsor.sponsorName,
        telephoneNumber: sponsor.telephoneNumber,
        email: sponsor.email,
        address: sponsor.address,
        description: sponsor.description,
      };
      return displaySponsor;
    })))
    return respone;
}

getProjects() {
  const url = `api/project/list`

  var respone = this.http.get<Project[]>(url).pipe(
    map((projects: any[]) => projects.map(projects => {
      const displayProject: Project = {
        id: projects.id,
        title: projects.title,
        description: projects.description,
        creationDate: projects.creationDate,
        endofprojectDate: projects.endOfProjectDate,
        users: projects.users,
        sponsors: projects.sponsors
      };
      return displayProject;
    })))
    return respone;
}

getActiveUserProjects() {
  const url = `api/project/active-list`

  var respone = this.http.get<Project[]>(url).pipe(
    map((projects: any[]) => projects.map(projects => {
      const displayProject: Project = {
        id: projects.id,
        title: projects.title,
        description: projects.description,
        creationDate: projects.creationDate,
        endofprojectDate: projects.endOfProjectDate,
        users: projects.users,
        sponsors: projects.sponsors
      };
      return displayProject;
    })))
    return respone;
}


getOrganizationUserList() {
  const url = `api/organization/organization-users`;

  var respone = this.http.get<User[]>(url).pipe(
    map((users: any[]) => users.map(user => {
      const displayUser: User = {
        id: user.id,
        name: user.name,
        lastName: user.lastName,
        email: user.email,
        creationDate: new Date(user.creationDate),
        roles: user.roles.map((role: { userType: any; }) => role.userType === UserType.Administratorius ? 'Administratorius' : 'Narys'),
        organizationId: user.organizationId
      };
      return displayUser;
    })))

  return respone;
}

  register(name: string, surname: string, email: string, password: string) {
    const url = `api/register`;

    // Define the request body
    const body = {
      name: name,
      surname: surname,
      email: email,
      password: password
    };

    // Send the HTTP POST request
    return this.http.post(url, body);
  }

  addCommentForSponsor(sponsorId: number, commentText: string) {
    const url = `api/comment`;

    // Define the request body
    const body = {
      sponsorId: sponsorId,
      commentText: commentText
    };

    // Send the HTTP POST request
    return this.http.post(url, body);
  }


  changePassword(password: string){
    const url = `api/change-password`

    const body = {
      newPassword: password
    };

    return this.http.post(url, body);
  }

  renameOrganization(name: string) {
    const url = `api/organization/rename`;

    // Define the request body
    const body = {
      name: name
    };

    // Send the HTTP POST request
    return this.http.put(url, body);
  }

  createOrganization(name: string) {
    const url = `api/organization/create`;

    // Define the request body
    const body = {
      name: name
    };

    // Send the HTTP POST request
    return this.http.post(url, body);
  }

  addUserToOrganization(name: string, surname: string, email: string, password: string) {
    const url = `api/add-user`;

    const body = {
      name: name,
      surname: surname,
      email: email,
      password: password
    };

    return this.http.post(url, body);
  }

  addUserToProject(userToProjectId: number, projectId: number) {
    const url = 'api/project/user';

    const body = {
      userToProjectId: userToProjectId,
      projectId: projectId
    };

    return this.http.post(url, body);
  }

  addSponsorToProject(sponsorToProjectId: number, projectId: number) {
    const url = 'api/project/sponsor';

    const body = {
      sponsorToProjectId: sponsorToProjectId,
      projectId: projectId
    };

    return this.http.post(url, body);
  }


  createProject(name: string, description: string, creationDate: Date, endOfProjectDate: Date){
    const url = 'api/project';

    const body = {
      name: name,
      description: description,
      creationDate: creationDate,
      endOfProjectDate: endOfProjectDate
    };

    return this.http.post(url, body);

  }

  updateProject(id: number, name: string, description: string, creationDate: Date, endOfProjectDate: Date){
    const url = 'api/project';
    
    const body = {
      id: id,
      name: name,
      description: description,
      creationDate: creationDate,
      endOfProjectDate: endOfProjectDate
    };

    return this.http.put(url, body);

  }

  addOrganizationImapServer(serverName: string, port: number, userName: string, password: string) {
    const url = `api/organization/configure/imap`;

    const body = {
      imapServer: serverName,
      imapServerPort: port,
      imapServerUserName: userName,
      imapServerUserPassword: password
    }

    return this.http.put(url, body);
  }

  addOrganizationSmtpServer(serverName: string, port: number, userName: string, password: string) {
    const url = `api/organization/configure/smtp`;

    const body = {
      smtpServer: serverName,
      smtpServerPort: port,
      smtpServerUserName: userName,
      smtpServerUserPassword: password
    }

    return this.http.put(url, body);
  }

  async getUserAsync() {
    const url = `api/user`;
  
    var respone = this.http.get<User>(url).pipe(
      map((user: any) => {
        const displayUser: User = {
          id: user.id,
          name: user.name,
          lastName: user.lastName,
          email: user.email,
          creationDate: new Date(user.creationDate),
          roles: user.roles.map((role: { userType: any; }) => role.userType === UserType.Administratorius ? 'Administratorius' : 'Narys'),
          organizationId: user.organizationId
        };
        return displayUser;
      })).toPromise();
  
    return respone;
  }

  getUser() {
    const url = `api/user`;
  
    return this.http.get<User>(url).pipe(
      map((user: any) => {
        const displayUser: User = {
          id: user.id,
          name: user.name,
          lastName: user.lastName,
          email: user.email,
          creationDate: new Date(user.creationDate),
          roles: user.roles.map((role: { userType: any; }) => role.userType === UserType.Administratorius ? 'Administratorius' : 'Narys'),
          organizationId: user.organizationId
        };
        return displayUser;
      }));
  }



  getOrganizationReceivedEmailList() {
    const url = `api/email/list`;
  
    var respone = this.http.get<Email[]>(url).pipe(
      map((emails: any[]) => emails.map(email => {
        const displayEmail: Email = {
          messageId: email.messageId,
          organizationId: email.organizationId,
          subject: email.subject,
          fromName: email.fromName,
          fromEmail: email.fromEmail,
          textBody: email.textBody,
          htmlBody: email.htmlBody,
          date: email.date
        };
        return displayEmail;
      })))
  
    return respone;
  }

  getOrganizationReceivedEmailListByAddress(address: string) {
    const url = `api/email/list/${encodeURIComponent(address)}`;
  
    var respone = this.http.get<Email[]>(url).pipe(
      map((emails: any[]) => emails.map(email => {
        const displayEmail: Email = {
          messageId: email.messageId,
          organizationId: email.organizationId,
          subject: email.subject,
          fromName: email.fromName,
          fromEmail: email.fromEmail,
          textBody: email.textBody,
          htmlBody: email.htmlBody,
          date: email.date
        };
        return displayEmail;
      })))
  
    return respone;
  }

  getOrganizationSentEmailList() {
    const url = `api/email/outgoing-list`;
  
    var respone = this.http.get<OutgoingEmail[]>(url).pipe(
      map((emails: any[]) => emails.map(email => {
        const displayEmail: OutgoingEmail = {
          id: email.id,
          organizationId: email.organizationId,
          subject: email.subject,
          user: email.user,
          receiverEmail: email.receiverEmail,
          body: email.body,
          date: email.date
        };
        return displayEmail;
      })))
  
    return respone;
  }

  getOrganizationSentEmailListByAddress(address: string) {
    const url = `api/email/outgoing-list/${encodeURIComponent(address)}`;
  
    var respone = this.http.get<OutgoingEmail[]>(url).pipe(
      map((emails: any[]) => emails.map(email => {
        const displayEmail: OutgoingEmail = {
          id: email.id,
          organizationId: email.organizationId,
          subject: email.subject,
          user: email.user,
          receiverEmail: email.receiverEmail,
          body: email.body,
          date: email.date
        };
        return displayEmail;
      })))
  
    return respone;
  }

  sendEmail(receiverEmail: string, subject: string, body: string) {
    const url = `api/email/send`;

    // Define the request body
    const Body = {
      receiverEmail: receiverEmail,
      subject: subject,
      body: body
    };

    // Send the HTTP POST request
    return this.http.post(url, Body);
  }

  getSponsorComments(sponsorId: number) {
    const url = `api/comment/list?SponsorId=${sponsorId}`;
  
    var respone = this.http.get<any[]>(url).pipe(
      map((comments: any[]) => comments.map((comment: SponsorComment) => {
        const displayComment: SponsorComment = {
          id: comment.id,
          user: comment.user,
          sponsorId: comment.sponsorId,
          commentText: comment.commentText,
          date: comment.date
        };

        return displayComment;
      }))
    )
  
    return respone;
  }
}

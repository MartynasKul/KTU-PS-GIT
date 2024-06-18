export enum UserType {
  Administratorius,
  Narys
}

export interface Role {
  userType: UserType;
}

export interface User {
  roles: Role[];
  name: string;
  lastName: string;
  email: string;
  id: number;
  creationDate: Date;
  organizationId: number;
}
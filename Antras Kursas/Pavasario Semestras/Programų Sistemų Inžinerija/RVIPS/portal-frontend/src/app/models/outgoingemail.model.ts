import { User } from "./user.model";

export interface OutgoingEmail {
    id: string,
    organizationId: number,
    user: User,
    receiverEmail: string,
    subject: string,
    body: string,
    date: Date
  }
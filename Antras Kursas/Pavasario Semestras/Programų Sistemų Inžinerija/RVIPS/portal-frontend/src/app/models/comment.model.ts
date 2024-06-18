import { User } from "./user.model"

export interface SponsorComment {
    id: number;
    user: User;
    sponsorId: number;
    commentText: string;
    date: Date;
  }
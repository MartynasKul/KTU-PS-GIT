import { Sponsor } from './sponsor.model';
import { User } from './user.model'

export interface Project {
    id: number;
    title: string;
    description: string;
    creationDate: Date;
    endofprojectDate: Date;
    users: User[];
    sponsors: Sponsor[];
}
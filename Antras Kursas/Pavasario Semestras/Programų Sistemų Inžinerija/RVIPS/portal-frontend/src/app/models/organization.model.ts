export interface Organization {
    id: number;
    title: string;
    imapServer?: string;
    imapServerPort?: number;
    imapServerUserName?: string;
    smtpServer?: string;
    smtpServerPort?: number;
    smtpServerUserName?: string;
  }
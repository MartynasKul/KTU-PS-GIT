export interface Email {
    messageId: string,
    organizationId: number,
    subject: string,
    fromName: string,
    fromEmail: string,
    textBody: string,
    htmlBody: string,
    date: Date
  }
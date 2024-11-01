export interface Event {
    eventID: string;            // Guid in C# translates to a string in TypeScript
    organizationID: string;     // Guid in C# translates to a string in TypeScript
    eventName: string;
    eventDateTime: string;      // DateTime in C# is typically represented as a string in TypeScript (e.g., ISO format)
    eventLocation: string;
    eventAge: number;
    eventCategory: string;
    eventImage: string;
    eventDescription: string;
}

export interface Ticket{
    eventID: String;
    ticketID: String;
    ticketName: String;
    ticketPrice: String;
    ticketMaxAvailable: number;
    ticketsRemaining: number;
    ticketStatus: boolean;
}
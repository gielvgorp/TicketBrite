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
    tickets: Ticket[];
}

export interface Ticket{
    eventID: string;
    ticketID: string;
    ticketName: string;
    ticketPrice: string;
    ticketMaxAvailable: number;
    ticketsRemaining: number;
    ticketStatus: boolean;
}

export interface TicketStatistic{
    ticket: Ticket;
    ticketReserve: number;
    ticketSold: number;
}

export interface Purchase {
    purchaseID: string;
    userID: string;
    guestID: string;
}

export interface shoppingCartItem{
    reservedTicket: Reservation;
    eventTicket: Ticket;
    event: Event;
}

export interface Reservation {
    reservedID: string;
    ticketID: string;
    userID: string;
    reservedAt: Date;
}

export interface ApiResponse<T> {
    statusCode: number;
    message?: string;
    value?: T;
}
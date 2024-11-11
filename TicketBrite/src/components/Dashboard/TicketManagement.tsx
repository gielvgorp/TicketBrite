import React, { useState } from 'react';
import { Ticket } from '../../Types';


interface TicketManagementProps {
    initialTickets: Ticket[];
    eventId: string;
    onSaveTickets: (tickets: Ticket[]) => void;
}

const TicketManagement: React.FC<TicketManagementProps> = ({ initialTickets, eventId, onSaveTickets }) => {
    const [tickets, setTickets] = useState<Ticket[]>(initialTickets);

    const handleAddTicket = () => {
        const newTicket: Ticket = {
            eventID: eventId,
            ticketID: `${Date.now()}`, // Tijdelijk ID
            ticketName: '',
            ticketPrice: '',
            ticketMaxAvailable: 0,
            ticketStatus: true,
            ticketsRemaining: 0,
            eventDateTime: ''
        };
        setTickets([...tickets, newTicket]);
    };

    const handleInputChange = (index: number, field: keyof Ticket, value: string | number | boolean) => {
        const updatedTickets = [...tickets];
        updatedTickets[index] = { ...updatedTickets[index], [field]: value };
        setTickets(updatedTickets);
    };

    const handleSave = () => {
        onSaveTickets(tickets);
    };

    return (
        <div className="card p-4 mb-3">
            <h5>Ticketbeheer</h5>
            {tickets.map((ticket, index) => (
                <div key={ticket.ticketID} className="mb-3">
                    <div className="form-group mb-2">
                        <label>Ticket Naam</label>
                        <input
                            type="text"
                            value={ticket.ticketName}
                            onChange={(e) => handleInputChange(index, 'ticketName', e.target.value)}
                            className="form-control"
                        />
                    </div>
                    <div className="form-group mb-2">
                        <label>Ticket Prijs</label>
                        <input
                            type="text"
                            value={ticket.ticketPrice}
                            onChange={(e) => handleInputChange(index, 'ticketPrice', e.target.value)}
                            className="form-control"
                        />
                    </div>
                    <div className="form-group mb-2">
                        <label>Maximaal Beschikbaar</label>
                        <input
                            type="number"
                            value={ticket.ticketMaxAvailable}
                            onChange={(e) => handleInputChange(index, 'ticketMaxAvailable', parseInt(e.target.value))}
                            className="form-control"
                        />
                    </div>
                    <div className="form-group form-check mb-2">
                        <input
                            type="checkbox"
                            className="form-check-input"
                            checked={ticket.ticketStatus}
                            onChange={(e) => handleInputChange(index, 'ticketStatus', e.target.checked)}
                        />
                        <label className="form-check-label">Ticket Actief</label>
                    </div>
                </div>
            ))}
            <button onClick={handleAddTicket} className="btn btn-secondary mt-2">Ticket Toevoegen</button>
            <button onClick={handleSave} className="btn btn-primary mt-2 ml-2">Opslaan</button>
        </div>
    );
};

export default TicketManagement;

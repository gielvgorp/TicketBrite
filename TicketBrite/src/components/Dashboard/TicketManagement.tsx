import { useState } from 'react';
import { ApiResponse, Ticket } from '../../Types';
import TicketManagementItem from './TicketManagementComponent';
import { ErrorNotification, SuccessNotification } from '../Notifications/Notifications';


type Props = {
    initialTickets: Ticket[];
    eventId: string;
}

function TicketManagement({initialTickets, eventId}: Props){
    const [tickets, setTickets] = useState<Ticket[]>(initialTickets);

    function handleSave(ticket: Ticket, index: number){
        console.log("Ticket", ticket);
        const updatedTickets = [...tickets];
        updatedTickets[index] = ticket;
        setTickets(updatedTickets);
    }

    const storeTickets = () => {
        try {
            const token = localStorage.getItem("jwtToken");

            fetch('http://localhost:7150/api/Ticket/tickets/save', {
                method: 'PUT',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(
                    tickets
                )
            })
            .then(response => response.json()) // Verwerk de response als JSON
            .then(data => {
                // validation error
                if(data.statusCode !== 200){
                    ErrorNotification({text: "Gegevens kunnen niet worden opgeslagen!"});
                }

                // successful registered
                if(data.statusCode === 200){
                    SuccessNotification({text: 'Gegevens opgeslagen!'});
                }         
                })
            .catch(error => {
                ErrorNotification({text: "Gegevens kunnen niet worden opgeslagen!"});
            });
        } catch (error) {
            ErrorNotification({text: "Gegevens kunnen niet worden opgeslagen!"});
        }
    }

    const handleAddTicket = () => {
        const newTicket: Ticket = {
            eventID: eventId,
            ticketID: '00000000-0000-0000-0000-000000000000', // Tijdelijk ID
            ticketName: '',
            ticketPrice: '',
            ticketMaxAvailable: 0,
            ticketStatus: true,
            ticketsRemaining: 0
        };
        setTickets([...tickets, newTicket]);
    };

    return (
        <div className="card p-4 mb-3">
            <h5>Ticketbeheer</h5>
            {tickets.map((ticket, index) => (
                 <TicketManagementItem key={index} index={index} _ticket={ticket} saveTicket={handleSave} />
            ))}
            <button onClick={handleAddTicket} className="btn btn-secondary mt-2">Ticket Toevoegen</button>
            <button onClick={storeTickets} className="btn btn-primary mt-2 ml-2">Opslaan</button>
        </div>
    );
}

export default TicketManagement;

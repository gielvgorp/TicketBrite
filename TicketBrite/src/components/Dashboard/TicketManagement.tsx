import { useState } from 'react';
import { Ticket } from '../../Types';
import TicketManagementItem from './TicketManagementComponent';


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

    const storeTickets = async () => {
        try {
            // Verzend het formulier naar het endpoint
            const res = await fetch('http://localhost:7150/dashboard/tickets/save', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(
                    tickets
                ) // Zet de formData om naar JSON
            });
            
            const data = await res.json(); // Ontvang de JSON-response
            console.log(data);
            // validation error
            if(data.statusCode === 400){
                console.log(data);
                //setErrorMsg(data.value);
            }

            // successful registered
            // if(data.statusCode === 200){
            //     login(data.value.token);
            //     console.log("Token:", localStorage.getItem('jwtToken'));
            //     navigate("/", {replace: true});
            // }           
        } catch (error) {
            console.error('Er is een fout opgetreden:', error);
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

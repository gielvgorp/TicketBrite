import { useEffect, useState } from "react";
import { Ticket } from "../../Types"

type TicketManagementItemProps = {
    _ticket: Ticket
    index: number;
    saveTicket: (ticket: Ticket, index: number) => void;
}




function TicketManagementItem({_ticket, index, saveTicket}: TicketManagementItemProps){
    
    const [ticket, setTicket] = useState<Ticket>({
        ticketID: '',
        eventID: '',
        ticketMaxAvailable: 0,
        ticketName: '',
        ticketPrice: '',
        ticketsRemaining: 0,
        ticketStatus: true,
    });
    
    const handleInputChange = <K extends keyof Ticket>(property: K, value: string | number | boolean) => {    
        setTicket(prevTicket => ({
            ...prevTicket, 
            [property]: value,
        }));

        handleSave(ticket, index);
    };
    
    // set endpoint api to save ticket
    const handleSave = (ticket: Ticket, index: number) => {
        saveTicket(ticket, index);
    };
    
    useEffect(() => {
        setTicket(_ticket);
    }, []);

    return (
        <div key={ticket.ticketID} className="mb-3">
            <div className="form-group mb-2">
                <label>Ticket Naam</label>
                <input
                    type="text"
                    value={ticket.ticketName}
                    onChange={(e) => handleInputChange('ticketName', e.target.value)}
                    className="form-control"
                />
            </div>
            <div className="form-group mb-2">
                <label>Ticket Prijs</label>
                <input
                    type="text"
                    value={ticket.ticketPrice}
                    onChange={(e) => handleInputChange('ticketPrice', e.target.value)}
                    className="form-control"
                />
            </div>
            <div className="form-group mb-2">
                <label>Maximaal Beschikbaar</label>
                <input
                    type="text"
                    value={ticket.ticketMaxAvailable}
                    onChange={(e) => handleInputChange('ticketMaxAvailable', e.target.value)}
                    className="form-control"
                />
            </div>
            <div className="form-group form-check mb-2">
                <input
                    type="checkbox"
                    className="form-check-input"
                    checked={ticket.ticketStatus}
                    onChange={(e) => handleInputChange('ticketStatus', e.target.checked)}
                />
                <label className="form-check-label">Ticket Actief</label>
            </div>
        </div>
    )
}

export default TicketManagementItem
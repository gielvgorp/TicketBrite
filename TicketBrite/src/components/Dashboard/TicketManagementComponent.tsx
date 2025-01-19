import { useEffect, useState } from "react";
import { Ticket } from "../../Types";

type TicketManagementItemProps = {
    _ticket: Ticket;
    index: number;
    saveTicket: (ticket: Ticket, index: number) => void;
};

function TicketManagementItem({ _ticket, index, saveTicket }: TicketManagementItemProps) {
    const [ticket, setTicket] = useState<Ticket>({
        ticketID: "",
        eventID: "",
        ticketMaxAvailable: 0,
        ticketName: "",
        ticketPrice: "",
        ticketsRemaining: 0,
        ticketStatus: true,
    });

    const handleInputChange = <K extends keyof Ticket>(property: K, value: string | number | boolean) => {
        setTicket((prevTicket) => ({
            ...prevTicket,
            [property]: value,
        }));
    };

    const [prevTicket, setPrevTicket] = useState<Ticket | null>(null);

    useEffect(() => {
        if (prevTicket && JSON.stringify(prevTicket) !== JSON.stringify(ticket)) {
            saveTicket(ticket, index);
        }
        setPrevTicket(ticket);
    }, [ticket, index, saveTicket]);
    

    useEffect(() => {
        setTicket(_ticket);
    }, [_ticket]);

    return (
        <div key={ticket.ticketID} className="mb-3">
            <div className="form-group mb-2">
                <label>Ticket Naam</label>
                <input
                    type="text"
                    value={ticket.ticketName}
                    onChange={(e) => {handleInputChange("ticketName", e.target.value)}}
                    className="form-control"
                />
            </div>
            <div className="form-group mb-2">
                <label>Ticket Prijs</label>
                <input
                    type="text"
                    value={ticket.ticketPrice}
                    onChange={(e) => handleInputChange("ticketPrice", e.target.value)}
                    className="form-control"
                />
            </div>
            <div className="form-group mb-2">
                <label>Maximaal Beschikbaar</label>
                <input
                    type="text"
                    value={ticket.ticketMaxAvailable}
                    onChange={(e) => handleInputChange("ticketMaxAvailable", e.target.value)}
                    className="form-control"
                />
            </div>
            <div className="form-group form-check mb-2">
                <input
                    type="checkbox"
                    className="form-check-input"
                    checked={ticket.ticketStatus}
                    onChange={(e) => handleInputChange("ticketStatus", e.target.checked)}
                />
                <label className="form-check-label">Ticket Actief</label>
            </div>
        </div>
    );
}

export default TicketManagementItem;

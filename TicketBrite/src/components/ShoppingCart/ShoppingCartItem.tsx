import { Button, ListGroup, Image} from 'react-bootstrap';
import { shoppingCartItem } from '../../Types';
import { useState } from 'react';
import { WarningNotification, SuccessNotification } from '../Notifications/Notifications';

type Props = {
    ticket: shoppingCartItem;
    onRemoveItem: (value: number) => void;
}

function ShoppingCartItem({ticket, onRemoveItem}: Props){
    const [showItem, setShowItem] = useState(true);

    const showWarningNotification = () => {
        WarningNotification({
            text: "Weet je zeker dat je dit item wilt verwijderen?",
            onConfirm: () => {
                handleDeleteItem().catch((error) => {
                    console.error('Fout bij verwijderen:', error);
                });
            },
            onCancel: () => {
                console.log("Verwijderen geannuleerd.");
            },
        });
            
    }

    const handleDeleteItem = async () => {
        try {
            const token = localStorage.getItem("jwtToken");

            const response = await fetch(`http://localhost:7150/shopping-cart/${ticket.reservedTicket.reservedID}/delete`, {
                method: 'DELETE',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                }
            });

            if (!response.ok) {
                throw new Error('Fout bij het ophalen van gebruikersgegevens');
            }

            setShowItem(false);
            onRemoveItem(parseInt(ticket.eventTicket.ticketPrice) * -1);
            SuccessNotification({text: "Ticket is verwijderd uit je winkelwagen!"})
            
        } finally {
            //setLoading(false);
        }
    }

    {
        return showItem ? (
        <ListGroup.Item key={ticket.reservedTicket.reservedID} data-test="cart-item" className="cart-item d-flex justify-content-between align-items-center">
            <div className="d-flex align-items-center col-9">
                <Image src={ticket.event.eventImage} alt="Ticket Icon" rounded className="ticket-icon me-2" />
                <span className="ticket-name"><span data-test="cart-item-ticket-name">{ticket.eventTicket.ticketName}</span> - <strong data-test="cart-item-event-name">{ticket.event.eventName}</strong></span>
            </div>
            <div className='col-1 text-center'>
            <span className="price"><strong>00:00</strong></span>
            </div>
            <div className='col-1 text-end'>
                <span data-test="cart-item-ticket-price" className="price">€{(ticket.eventTicket.ticketPrice)}</span>
            </div>
            <div className='col-1 text-end'>
                <Button data-test='btn-remove-item' variant="outline-danger" size="sm" onClick={showWarningNotification}>
                    <i className="fas fa-trash"></i>
                </Button>
            </div>
        </ListGroup.Item>) : <></>
    }
}

export default ShoppingCartItem
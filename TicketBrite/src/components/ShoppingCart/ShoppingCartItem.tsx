import { Card, Button, ListGroup, Dropdown, Form, Image, Row, Col } from 'react-bootstrap';
import { shoppingCartItem, Ticket } from '../../Types';
import { useState } from 'react';

type Props = {
    ticket: shoppingCartItem;
    onRemoveItem: (value: number) => void;
}

function ShoppingCartItem({ticket, onRemoveItem}: Props){
    const [showItem, setShowItem] = useState(true);
    const handleDeleteItem = async () => {
        try {
            const token = localStorage.getItem("jwtToken");

            const response = await fetch(`https://localhost:7150/shopping-cart/${ticket.reservedTicket.reservedID}/delete`, {
                method: 'DELETE',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                }
            });

            if (!response.ok) {
                throw new Error('Fout bij het ophalen van gebruikersgegevens');
            }

            const data = await response.json();
           
            setShowItem(false);
            onRemoveItem(parseInt(ticket.eventTicket.ticketPrice) * -1);
        } finally {
            //setLoading(false);
        }
    }

    {
        return showItem ? (
        <ListGroup.Item key={ticket.reservedTicket.reservedID} data-test="cart-item" className="cart-item d-flex justify-content-between align-items-center">
            <div className="d-flex align-items-center col-9">
                <Image src={ticket.event.eventImage} alt="Ticket Icon" rounded className="ticket-icon me-2" />
                <span className="ticket-name"><span id='ticket-name'>{ticket.eventTicket.ticketName}</span> - <strong>{ticket.event.eventName}</strong></span>
            </div>
            <div className='col-1 text-center'>
            <span className="price"><strong>00:00</strong></span>
            </div>
            <div className='col-1 text-end'>
                <span className="price">€{(ticket.eventTicket.ticketPrice)}</span>
            </div>
            <div className='col-1 text-end'>
                <Button id='btn-remove-item' variant="outline-danger" size="sm" onClick={() => handleDeleteItem()}>
                    <i className="fas fa-trash"></i>
                </Button>
            </div>
        </ListGroup.Item>) : <></>
    }
}

export default ShoppingCartItem
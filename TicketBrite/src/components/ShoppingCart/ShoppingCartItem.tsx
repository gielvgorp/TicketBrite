import { Card, Button, ListGroup, Dropdown, Form, Image, Row, Col } from 'react-bootstrap';
import { shoppingCartItem, Ticket } from '../../Types';

type Props = {
    ticket: shoppingCartItem;
}

function ShoppingCartItem({ticket}: Props){
    const handleDeleteItem = async (reserveID: string) => {
        //setCartItems(prevItems => prevItems.filter(item => item.reservedTicket.reservedID !== reserveID));
        try {
            const token = localStorage.getItem("jwtToken");

            const response = await fetch(`https://localhost:7150/shopping-cart/${reserveID}/delete`, {
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
            console.log(data.value);
        } catch (error) {
            console.error('Er is een fout opgetreden:', error);
        } finally {
            //setLoading(false);
        }
    }

    return (
        <ListGroup.Item key={ticket.reservedTicket.reservedID} className="cart-item d-flex justify-content-between align-items-center">
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
                <Button id='btn-remove-item' variant="outline-danger" size="sm" onClick={() => handleDeleteItem(ticket.reservedTicket.reservedID)}>
                    <i className="fas fa-trash"></i>
                </Button>
            </div>
        </ListGroup.Item>
    )
}

export default ShoppingCartItem
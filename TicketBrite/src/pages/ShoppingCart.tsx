import { useEffect, useState } from 'react';
import { Card, Button, ListGroup, Dropdown, Form, Image, Row, Col } from 'react-bootstrap';
import '../ShoppingCart.css';
import { useNavigate } from 'react-router-dom';
import { Event, Ticket } from '../Types';

interface ReservedTicket {
    ticket: Ticket;
    reservation: Reservation;
}

interface Reservation {
    reservedID: string;
    ticketID: string;
    userID: string;
    reservedAt: Date;
}

interface ShoppingCart {
    totalPrice: string;
    items: shoppingCartItem[];
}

interface shoppingCartItem{
    reservedTicket: Reservation;
    eventTicket: Ticket;
    event: Event;
}

const banks = [
    "ABN AMRO", "ASN Bank", "Bunq", "ING", "Knab", "Rabobank", "RegioBank", "SNS Bank", "Triodos Bank", "Van Lanschot"
];

function ShoppingCart(){
    const navigate = useNavigate();
    const [shoppingCart, setShoppingCart] = useState<ShoppingCart>();
    const [cartItems, setCartItems] = useState<shoppingCartItem[]>([]);
    const [paymentMethod, setPaymentMethod] = useState<string>("iDeal");
    const [selectedBank, setSelectedBank] = useState<string | null>(null);

    useEffect(() => {
        handleFetchTickets();
    }, []);

    const handleFetchTickets = async () => {
        try {
            const token = localStorage.getItem("jwtToken");

            const response = await fetch('https://localhost:7150/shopping-cart/get-items', {
                method: 'GET',
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
            setShoppingCart(data.value);
            setCartItems(data.value.items);
        } catch (error) {
            console.error('Er is een fout opgetreden:', error);
        } finally {
            //setLoading(false);
        }
    }

    useEffect(() => {
        if (shoppingCart?.items) {
            setCartItems([...shoppingCart.items]);
        } else {
            setCartItems([]);
        }
    }, [shoppingCart]);

    const handleDeleteItem = async (reserveID: string) => {
        setCartItems(prevItems => prevItems.filter(item => item.reservedTicket.reservedID !== reserveID));
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

    const handlePurscheTickets = async () => {
        try {
            const token = localStorage.getItem("jwtToken");

            const response = await fetch('https://localhost:7150/tickets/buy', {
                method: 'POST',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                }
            });

            if (!response.ok) {
                throw new Error('Fout bij het ophalen van gebruikersgegevens');
            }

            const data = await response.json();
            console.log(data);
            navigate(`/Payment-success/${data.value}`, {replace: true});
        } catch (error) {
            console.error('Er is een fout opgetreden:', error);
        } finally {
            //setLoading(false);
        }
    }

    return (
        <div className="container mt-5">
            <Card className="p-4 shadow-lg shopping-cart-card mx-auto" style={{ maxWidth: '850px' }}>
                <Card.Body>
                    <h2 className="text-center mb-4" style={{ fontWeight: 'bold', fontSize: '2rem' }}>
                        <i className="fas fa-shopping-cart me-3 text-primary"></i>Winkelwagen
                    </h2>
                    {
                        shoppingCart !== undefined && shoppingCart?.items.length > 0 ? <ListGroup variant="flush" className="mb-4">
                        {cartItems.map(ticket => (
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
                                    <Button variant="outline-danger" size="sm" onClick={() => handleDeleteItem(ticket.reservedTicket.reservedID)}>
                                        <i className="fas fa-trash"></i>
                                    </Button>
                                </div>
                            </ListGroup.Item>
                        ))}
                    </ListGroup> : <h6 className='text-center'>Geen items in de winkelwagen!</h6>
                    }
                   

                    <hr />

                    <Row className="align-items-center mb-4">
                        <Col>
                            <h5>Totaal:</h5>
                        </Col>
                        <Col className="text-end">
                            <h5 className="text-success">€{shoppingCart?.totalPrice}</h5>
                        </Col>
                    </Row>

                    <h5 className="mb-3">Kies uw betaalmethode:</h5>
                    <div className="d-flex align-items-center mb-4">
                        <Form.Check 
                            inline 
                            label="iDeal" 
                            type="radio" 
                            name="paymentMethod" 
                            checked={paymentMethod === "iDeal"} 
                            onChange={() => setPaymentMethod("iDeal")} 
                        />
                        <Form.Check 
                            inline 
                            label="Mastercard" 
                            type="radio" 
                            name="paymentMethod" 
                            checked={paymentMethod === "Mastercard"} 
                            onChange={() => setPaymentMethod("Mastercard")} 
                        />
                        <Form.Check 
                            inline 
                            label="Apple Pay" 
                            type="radio" 
                            name="paymentMethod" 
                            checked={paymentMethod === "Apple Pay"} 
                            onChange={() => setPaymentMethod("Apple Pay")} 
                        />
                    </div>
                    
                    {paymentMethod === "iDeal" && (
                            <Dropdown onSelect={(value) => setSelectedBank(value || null)} className="mt-3 w-25">
                                <Dropdown.Toggle variant="secondary" id="dropdown-basic">
                                    {selectedBank || "Selecteer uw bank"}
                                </Dropdown.Toggle>
                                <Dropdown.Menu>
                                    {banks.map((bank, index) => (
                                        <Dropdown.Item key={index} eventKey={bank}>{bank}</Dropdown.Item>
                                    ))}
                                </Dropdown.Menu>
                            </Dropdown>
                        )}

                    <Button onClick={handlePurscheTickets} variant="success" className="w-100 payment-button mt-3">
                        <i className="fas fa-credit-card me-2"></i>
                        Doorgaan naar Betaling
                    </Button>
                </Card.Body>
            </Card>
        </div>
    );
};


export default ShoppingCart;

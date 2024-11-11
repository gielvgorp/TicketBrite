import React from 'react';
import { Card, Button, ListGroup, Dropdown, Form, Image, Row, Col } from 'react-bootstrap';
import '../ShoppingCart.css';
import { useNavigate } from 'react-router-dom';

interface Ticket {
    id: string;
    name: string;
    price: number;
    quantity: number;
}

const banks = [
    "ABN AMRO", "ASN Bank", "Bunq", "ING", "Knab", "Rabobank", "RegioBank", "SNS Bank", "Triodos Bank", "Van Lanschot"
];

function ShoppingCart(){
    const navigte = useNavigate();
    const [tickets, setTickets] = React.useState<Ticket[]>([
        { id: "1", name: "Concert Ticket", price: 45.00, quantity: 2 },
        { id: "2", name: "VIP Pass", price: 75.00, quantity: 1 }
    ]);
    const [paymentMethod, setPaymentMethod] = React.useState<string>("iDeal");
    const [selectedBank, setSelectedBank] = React.useState<string | null>(null);

    // Calculating total cost
    const totalCost = tickets.reduce((total, ticket) => total + ticket.price * ticket.quantity, 0).toFixed(2);

    // const handleQuantityChange = (id: string, delta: number) => {
    //     setTickets(prevTickets =>
    //         prevTickets.map(ticket =>
    //             ticket.id === id ? { ...ticket, quantity: Math.max(1, ticket.quantity + delta) } : ticket
    //         )
    //     );
    // };

    const handleRemoveTicket = (id: string) => {
        setTickets(prevTickets => prevTickets.filter(ticket => ticket.id !== id));
    };

    return (
        <div className="container mt-5">
            <Card className="p-4 shadow-lg shopping-cart-card mx-auto" style={{ maxWidth: '850px' }}>
                <Card.Body>
                    <h2 className="text-center mb-4" style={{ fontWeight: 'bold', fontSize: '2rem' }}>
                        <i className="fas fa-shopping-cart me-3 text-primary"></i>Winkelwagen
                    </h2>

                    <ListGroup variant="flush" className="mb-4">
                        {tickets.map(ticket => (
                            <ListGroup.Item key={ticket.id} className="d-flex justify-content-between align-items-center">
                                <div className="d-flex align-items-center col-5">
                                    <Image src="https://www.agentsafterall.nl/wp-content/uploads/Naamloos-1-header-1-1600x740.jpg" alt="Ticket Icon" rounded className="ticket-icon me-2" />
                                    <span className="ticket-name">{ticket.name} - <strong>Festival name 123 Live in concert</strong></span>
                                </div>
                                <div className='col-3 text-center'>
                                <span className="price"><strong>00:00</strong></span>
                                </div>
                                <div className='col-2 text-end'>
                                    <span className="price">€{(ticket.price * ticket.quantity).toFixed(2)}</span>
                                </div>
                                <div className='col-1 text-end'>
                                    <Button variant="outline-danger" size="sm" onClick={() => handleRemoveTicket(ticket.id)}>
                                        <i className="fas fa-trash"></i>
                                    </Button>
                                </div>
                            </ListGroup.Item>
                        ))}
                    </ListGroup>

                    <hr />

                    <Row className="align-items-center mb-4">
                        <Col>
                            <h5>Totaal:</h5>
                        </Col>
                        <Col className="text-end">
                            <h5 className="text-success">€{totalCost}</h5>
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

                    <Button onClick={() => navigte('/payment-success')} variant="success" className="w-100 payment-button mt-3">
                        <i className="fas fa-credit-card me-2"></i>
                        Doorgaan naar Betaling
                    </Button>
                </Card.Body>
            </Card>
        </div>
    );
};


export default ShoppingCart;

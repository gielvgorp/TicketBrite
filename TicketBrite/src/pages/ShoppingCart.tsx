import React, { useState } from 'react';
import { Button, Card, ListGroup, Form } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';

interface Ticket {
    name: string;
    quantity: number;
    price: number; // prijs per ticket
}

const ShoppingCart: React.FC = () => {
    const navigate = useNavigate();
    const [tickets, setTickets] = useState<Ticket[]>([
        { name: 'Concert A', quantity: 2, price: 50 },
        { name: 'Concert B', quantity: 1, price: 75 },
    ]);
    const [paymentMethod, setPaymentMethod] = useState<string>('iDeal');
    const [selectedBank, setSelectedBank] = useState<string>(''); // Huidige geselecteerde bank

    // Lijst van banken die iDeal ondersteunen
    const idealBanks = [
        'ABN AMRO',
        'ING',
        'Rabobank',
        'SNS Bank',
        'ASN Bank',
        'Triodos Bank',
        'Van Lanschot',
        'Bunq',
        'Revolut'
    ];

    // Bereken de totale prijs
    const totalCost = tickets.reduce((total, ticket) => total + ticket.price * ticket.quantity, 0);

    return (
        <div className="container mt-5">
            <h2>Winkelwagen</h2>
            <Card className="mb-4">
                <Card.Header>Gekozen Tickets</Card.Header>
                <ListGroup variant="flush">
                    {tickets.map((ticket, index) => (
                        <ListGroup.Item key={index} className="d-flex justify-content-between align-items-center">
                            <div>
                                <strong>{ticket.name}</strong> (x{ticket.quantity})
                            </div>
                            <span>€{(ticket.price * ticket.quantity).toFixed(2)}</span>
                        </ListGroup.Item>
                    ))}
                </ListGroup>
                <Card.Footer className="d-flex justify-content-between">
                    <strong>Totaal:</strong>
                    <strong>€{totalCost.toFixed(2)}</strong>
                </Card.Footer>
            </Card>

            <Card>
                <Card.Header>Kies uw Betaalmethode</Card.Header>
                <Card.Body>
                    <Form>
                        <Form.Check
                            type="radio"
                            label="iDeal"
                            name="paymentMethod"
                            id="ideal"
                            checked={paymentMethod === 'iDeal'}
                            onChange={() => {
                                setPaymentMethod('iDeal');
                                setSelectedBank(''); // Reset de geselecteerde bank als iDeal is geselecteerd
                            }}
                        />
                        {paymentMethod === 'iDeal' && (
                            <Form.Group controlId="formBankSelection" className="mb-3">
                                <Form.Label>Selecteer uw Bank</Form.Label>
                                <Form.Control as="select" value={selectedBank} onChange={(e) => setSelectedBank(e.target.value)}>
                                    <option value="">Kies een bank...</option>
                                    {idealBanks.map((bank, index) => (
                                        <option key={index} value={bank}>
                                            {bank}
                                        </option>
                                    ))}
                                </Form.Control>
                            </Form.Group>
                        )}

                        <Form.Check
                            type="radio"
                            label="Mastercard"
                            name="paymentMethod"
                            id="mastercard"
                            checked={paymentMethod === 'Mastercard'}
                            onChange={() => setPaymentMethod('Mastercard')}
                        />
                        <Form.Check
                            type="radio"
                            label="Apple Pay"
                            name="paymentMethod"
                            id="applePay"
                            checked={paymentMethod === 'Apple Pay'}
                            onChange={() => setPaymentMethod('Apple Pay')}
                        />
                    </Form>
                    <Button onClick={() => navigate('/payment-success')} variant="primary" className="mt-3">
                        Bestelling Plaatsen
                    </Button>
                </Card.Body>
            </Card>
        </div>
    );
};

export default ShoppingCart;

import React, { useState } from 'react';
import { Card, Button } from 'react-bootstrap';
import { useAuth } from '../AuthContext';

interface Ticket {
    name: string;
    quantity: number;
    price: number; // prijs per ticket
}

interface PaymentSuccessProps {
    tickets: Ticket[];
    purchaseId: string;
}

const PaymentSuccess: React.FC<PaymentSuccessProps> = () => {
    const {isAuthenticated} = useAuth();
    const [purchaseId, setPurchaseID] = useState('00000000-0000-0000-0000-000000000000');
    const [tickets, setTickets] = useState<Ticket[]>([
        { name: 'Concert A', quantity: 2, price: 50 },
        { name: 'Concert B', quantity: 1, price: 75 },
    ]);
    // Bereken de totale prijs
    const totalCost = tickets.reduce((total, ticket) => total + ticket.price * ticket.quantity, 0);

    return (
        <div className="container mt-5">
            <Card className="text-center">
                <Card.Header className="bg-success text-white">
                    <h2>Betaling Geslaagd!</h2>
                </Card.Header>
                <Card.Body>
                    <h4>Bedankt voor uw aankoop!</h4>
                    <p>Uw betaling is succesvol verwerkt. Hieronder vindt u een overzicht van uw aankoop.</p>
                    {
                        isAuthenticated ? <p>U kunt uw bestelling terug vinden in de bestelling overzicht bij uw profiel</p> : <p>U krijgt binnen 1-2 minuten een e-mail met uw tickets!</p>
                    }
                    
                    <h5>Aankoopnummer: <strong>{purchaseId}</strong></h5>

                    <Card className="mt-3">
                        <Card.Header>Gekozen Tickets</Card.Header>
                        <Card.Body>
                            <ul className="list-group">
                                {tickets.map((ticket, index) => (
                                    <li key={index} className="list-group-item d-flex justify-content-between align-items-center">
                                        <span>{ticket.name} (x{ticket.quantity})</span>
                                        <span>€{(ticket.price * ticket.quantity).toFixed(2)}</span>
                                    </li>
                                ))}
                            </ul>
                            <div className="mt-3">
                                <strong>Totaal:</strong> €{totalCost.toFixed(2)}
                            </div>
                        </Card.Body>
                    </Card>

                    <Button variant="primary" className="mt-4" href="/">Terug naar Hoofdpagina</Button>
                </Card.Body>
            </Card>
        </div>
    );
};

export default PaymentSuccess;

import { useEffect, useState } from 'react';
import { Card, Button } from 'react-bootstrap';
import { useAuth } from '../AuthContext';
import { useParams } from 'react-router-dom';
import { Ticket } from '../Types';

function PaymentSuccess(){
    const {isAuthenticated} = useAuth();
    const { id } = useParams();
    const [tickets, setTickets] = useState<Ticket[]>([]);
    // Bereken de totale prijs
    const totalCost = tickets.reduce((total, ticket) => total + parseInt(ticket.ticketPrice) * 1, 0);

    useEffect(() => {
        handleFetchTickets();
    }, []);

    const handleFetchTickets = async ()=> {
        try {
            const token = localStorage.getItem("jwtToken");

            const response = await fetch(`https://localhost:7150/get-purchase/${id}`, {
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
            console.log(data);
            setTickets(data.value);
        } catch (error) {
            console.error('Er is een fout opgetreden:', error);
        } finally {
            //setLoading(false);
        }
    }

    return (
        <div className="container mt-5">
            <Card className="text-center">
                <Card.Header className="bg-success text-white">
                    <h2 className='text-white'>Betaling Geslaagd!</h2>
                </Card.Header>
                <Card.Body>
                    <h4>Bedankt voor uw aankoop!</h4>
                    <p>Uw betaling is succesvol verwerkt. Hieronder vindt u een overzicht van uw aankoop.</p>
                    {
                        isAuthenticated ? <p>U kunt uw bestelling terug vinden in de bestelling overzicht bij uw profiel</p> : <p>U krijgt binnen 1-2 minuten een e-mail met uw tickets!</p>
                    }
                    
                    <h5>Aankoopnummer: <strong>{id}</strong></h5>

                    <Card className="mt-3">
                        <Card.Header>Gekozen Tickets</Card.Header>
                        <Card.Body>
                            <ul className="list-group">
                                {tickets.map((ticket, index) => (
                                    <li key={index} className="list-group-item d-flex justify-content-between align-items-center">
                                        <span>{ticket.ticketName} (x{1})</span>
                                        <span>€{(parseInt(ticket.ticketPrice) * 1).toFixed(2)}</span>
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
}

export default PaymentSuccess;

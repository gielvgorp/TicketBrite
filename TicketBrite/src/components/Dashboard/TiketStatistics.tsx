import React, { useEffect, useState } from 'react';
import { getTicketOfEvent } from '../../hooks/useTIcket';
import { Ticket } from '../../Types';
import { Card, ListGroup, Spinner, Alert } from "react-bootstrap";

interface TicketStatisticsProps {
    eventId: string;
}

const TicketStatistics: React.FC<TicketStatisticsProps> = ({ eventId }) => {
    const [stats, setStats] = useState<Ticket[]>([]);
    const [loading, setLoading] = useState(true);
    const [error] = useState<string | null>(null);

    useEffect(() => {
        const fetchStats = async () => {
            const data = await getTicketOfEvent(eventId);
            setStats(data.value);
            setLoading(false);
        };
        fetchStats();
    }, [eventId]);

    return (
        <Card className="p-4">
            <Card.Header as="h5">Ticketstatistieken</Card.Header>
            <Card.Body>
                {loading && <Spinner animation="border" />}
                {error && <Alert variant="danger">{error}</Alert>}
                {stats.length > 0 ? (
                    <ListGroup variant="flush">
                        {stats.map((ticket) => (
                            <ListGroup.Item key={ticket.ticketID} className="d-flex justify-content-between">
                                <div className='col-8'>
                                    <strong>{ticket.ticketName}</strong>
                                </div>
                                <div className='d-flex col-4'>
                                    <div className="col-6 text-end"> <span className="text-success">Verkocht: {ticket.ticketsRemaining}</span></div>
                                    <div className="col-6 text-end"> <span className="mx-3 text-warning">Gereserveerd: {ticket.ticketsRemaining}</span></div>
                                   
                                   
                                </div>
                            </ListGroup.Item>
                        ))}
                    </ListGroup>
                ) : (
                    <Alert variant="info">Geen ticketstatistieken beschikbaar.</Alert>
                )}
            </Card.Body>
        </Card>
    );
};

export default TicketStatistics;

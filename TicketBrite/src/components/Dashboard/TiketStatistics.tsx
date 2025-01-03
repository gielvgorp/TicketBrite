import { useEffect, useState } from 'react';
import { TicketStatistic } from '../../Types';
import { Card, ListGroup, Spinner, Alert } from "react-bootstrap";
import { HubConnectionBuilder, HubConnection } from "@microsoft/signalr";

type Props = {
    eventId: string;
}

function TicketStatistics({eventId}: Props){
    const [stats, setStats] = useState<TicketStatistic[]>([]);
    const [loading, setLoading] = useState(true);
    const [error] = useState<string | null>(null);
    const [connection, setConnection] = useState<HubConnection | null>(null);

    useEffect(() => {
        const fetchStats = async () => {
            // const data = await getTicketOfEvent(eventId);
            // setStats(data.value);
            // setLoading(false);

            try {
                // Verzend het formulier naar het endpoint
                const res = await fetch(`http://localhost:7150/dashboard/tickets-statistics/${eventId}`, {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json'
                    }
                });
                
                const data = await res.json(); // Ontvang de JSON-response
                // validation error
                if(data.statusCode === 400){
                    console.log(data);
                    //setErrorMsg(data.value);
                }
    
                // successful registered
                if(data.statusCode === 200){
                    setLoading(false);
                    setStats(data.value);
                }           
            } catch (error) {
                console.error('Er is een fout opgetreden:', error);
            }
        };
        fetchStats();
    }, [eventId]);

    useEffect(() => {
        const newConnection = new HubConnectionBuilder()
            .withUrl("http://localhost:7150/hubs/ticketStatistics")
            .withAutomaticReconnect()
            .build();
    
        setConnection(newConnection);
    }, []);

    useEffect(() => {
        if (!connection) return;
    
        connection.start()
            .then(() => {
                console.log("Verbonden met SignalR hub");
    
                connection.on("UpdateTicketStats", (updatedStats) => {
                    console.log("Ontvangen update:", updatedStats);
                    setStats((prevStats) =>
                        prevStats.map((ticket) =>
                            ticket.ticket.ticketID === updatedStats.ticketID
                                ? {
                                    ...ticket,
                                    ticketSold: updatedStats.soldTickets,
                                    ticketReserve: updatedStats.reservedTickets
                                }
                                : ticket
                        )
                    );
                });
            })
            .catch((error) => console.error("SignalR connectiefout:", error));
    
        return () => {
            connection.stop();
        };
    }, [connection]);

    useEffect(() => {
        console.log("Updated tickets: ", stats);
    }, [stats]);

    return (
        <Card className="p-4">
            <Card.Header as="h5">Ticketstatistieken</Card.Header>
            <Card.Body>
                {loading && <Spinner animation="border" />}
                {error && <Alert variant="danger">{error}</Alert>}
                {stats.length > 0 ? (
                    <ListGroup variant="flush">
                        {stats.map((ticket) => (
                            <ListGroup.Item key={ticket.ticket.ticketID} className="d-flex justify-content-between">
                                <div className='col-8'>
                                    <strong>{ticket.ticket.ticketName}</strong>
                                </div>
                                <div className='d-flex col-4'>
                                    <div className="col-6 text-end"> <span className="text-success">Verkocht: {ticket.ticketSold}/{ticket.ticket.ticketMaxAvailable}</span></div>
                                    <div className="col-6 text-end"> <span className="mx-3 text-warning">Gereserveerd: {ticket.ticketReserve}</span></div>
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
}

export default TicketStatistics;

import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import EventDetailsForm from '../components/Dashboard/EventDetailsForm';
import { Event, Ticket } from '../Types';
import TicketManagement from '../components/Dashboard/TicketManagement';
import TicketStatistics from '../components/Dashboard/TiketStatistics';
import { updateEventDetails } from '../hooks/useEvent';
import '../Dashboard.css';

function DashboardPage(){
    const { eventId } = useParams<{ eventId: string | undefined }>();
    const [eventDetails, setEventDetails] = useState<Event | null>(null);
    const [eventTickets, setEventTickets] = useState<Ticket[] | null>(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        fetchData()
    }, []);

    const fetchData = () => {
        fetch(`http://localhost:7150/get-event/${eventId}`)
        .then(response => response.json())
        .then(data => {
            console.log(data.value);
            setEventDetails(data.value.event);
            setEventTickets(data.value.tickets)
            setLoading(false);
        })
        .catch(error => {
            console.error('Error fetching data:', error);  
            setLoading(false);
        });
    }

    const handleUpdateEvent = async (updatedDetails: Event) => {
        if (!eventId) {
            console.error("Event ID is missing, cannot update event details.");
            return;
        }
        
        await updateEventDetails(eventId, updatedDetails);
        setEventDetails(updatedDetails);
    };

    if (loading) return <div>Loading...</div>;

    return (
        <div className="container mt-5">
            <h2>Dashboard voor {eventDetails?.eventName}</h2>
            <div className="row">
                <div className="col-md-6">
                    <EventDetailsForm eventDetails={eventDetails!} onSave={handleUpdateEvent} />
                </div>
                <div className="col-md-6">
                    {eventId ? <TicketManagement initialTickets={eventTickets || []} eventId={eventId} />: <div>Error: Event ID is missing.</div>}
                </div>
            </div>
            <div className="mt-4">
                {eventId ? <TicketStatistics eventId={eventId} /> : <div>Error: Event ID is missing.</div>}
            </div>
        </div>
    );
}

export default DashboardPage;

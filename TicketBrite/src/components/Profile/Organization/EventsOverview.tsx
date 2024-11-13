import React, { useEffect, useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Modal, Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { Ticket, Event } from '../../../Types';

interface NewEventRequest {
    event: Event;
    tickets: Ticket[];
}

const EventsOverview: React.FC<{ organizationID: string }> = ({ organizationID }) => {
    const navigate = useNavigate();
    const [events, setEvents] = useState<Event[]>([]);
    const [showModal, setShowModal] = useState<boolean>(false);
    const [eventDetails, setEventDetails] = useState<Event>({
        eventID: '00000000-0000-0000-0000-000000000000',
        eventName: '',
        eventDateTime: '',
        eventLocation: '',
        eventCategory: '',
        eventDescription: '',
        eventImage: '',
        organizationID: organizationID,
        eventAge: 0,
        tickets: []
    });

    useEffect(() => {
        fetchEvents();
    }, []);

    const fetchEvents = async () => {
        try {
            fetch(`https://localhost:7150/api/Organization/get-events/${organizationID}`)
            .then(response => response.json())
            .then(data => {
                console.log(data.value);
                setEvents(data.value);
            })
            .catch(error => {
                console.error('Error fetching data:', error);  
            });
        } catch (error) {
            console.error("Er is een fout opgetreden bij het ophalen van evenementen:", error);
        }
    };

    // Toggle modal visibility
    const handleShow = () => setShowModal(true);
    const handleClose = () => setShowModal(false);

    // Functie om evenement details te wijzigen
    const handleEventChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setEventDetails({
            ...eventDetails,
            [e.target.name]: e.target.value,
        });
    };

    // Functie om een nieuw ticket toe te voegen
    // const addTicket = () => {
    //     setEventDetails((prevDetails) => ({
    //         ...prevDetails,
    //         tickets: [...prevDetails.tickets, { ticketName: '', ticketPrice: '', ticketMaxAvailable: 0, ticketID: '', eventID: '', eventDateTime: '', ticketsRemaining: 0, ticketStatus: true }],
    //     }));
    // };

    // Functie om ticketgegevens te wijzigen
    // const handleTicketChange = (index: number, e: React.ChangeEvent<HTMLInputElement>) => {
    //     const updatedTickets = [...eventDetails.tickets];
    //     const fieldName = e.target.name as keyof Ticket;
    
    //     //updatedTickets[index][fieldName] = e.target.value as any;
    //     setEventDetails((prevDetails) => ({ ...prevDetails, tickets: updatedTickets }));
    // };

    // Functie om evenement en tickets toe te voegen aan de API
    const handleAddEvent = async () => {
        const model: Event = {
            eventID: eventDetails.eventID,
            eventName: eventDetails.eventName,
            eventDateTime: eventDetails.eventDateTime,
            eventLocation: eventDetails.eventLocation,
            eventAge: eventDetails.eventAge,
            eventCategory: eventDetails.eventCategory,
            eventDescription: eventDetails.eventDescription,
            eventImage: eventDetails.eventImage,
            tickets: eventDetails.tickets,
            organizationID: eventDetails.organizationID
        };

        try {
            // Verzend het formulier naar het endpoint
            const res = await fetch('https://localhost:7150/api/Organization/event/new', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(model) // Zet de formData om naar JSON
            });

            const data = await res.json(); // Ontvang de JSON-response

            // validation error
            if(res.status === 400){
                //setErrorMsg("Een of meerdere velden zijn leeg!");
            }

            console.log(eventDetails);

            // successful registered
            if(res.status === 200){
                //localStorage.setItem('jwtToken', data.token);
                console.log(data);
                //navigate("/", {replace: true});
            }           
        } catch (error) {
            console.error('Er is een fout opgetreden:', error);
        }
    };

    return (
        <div className="container" style={{ marginTop: '20px' }}>
            <h3 className="mb-4">Mijn Evenementen</h3>
            <Button variant="success" onClick={handleShow} style={{ padding: '10px 20px', marginBottom: '20px' }}>
                Evenement toevoegen
            </Button>

            {/* Modal for Adding Event */}
            <Modal show={showModal} onHide={handleClose} size="lg">
                <Modal.Header closeButton>
                    <Modal.Title>Nieuw Evenement Toevoegen</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <form>
                        <div className="row">
                            <div className="col-md-6 mb-3">
                                <label className="form-label">Naam evenement</label>
                                <input type="text" className="form-control" name="eventName" value={eventDetails.eventName} onChange={handleEventChange} />
                            </div>
                            <div className="col-md-6 mb-3">
                                <label className="form-label">Datum van evenement</label>
                                <input type="date" className="form-control" name="eventDateTime" value={eventDetails.eventDateTime} onChange={handleEventChange} />
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-md-6 mb-3">
                                <label className="form-label">Locatie</label>
                                <input type="text" className="form-control" name="eventLocation" value={eventDetails.eventLocation} onChange={handleEventChange} />
                            </div>
                            <div className="col-md-6 mb-3">
                                <label className="form-label">Minimum leeftijd</label>
                                <input type="number" className="form-control" name="eventAge" value={eventDetails.eventAge} onChange={handleEventChange} />
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-md-6 mb-3">
                                <label className="form-label">Categorie</label>
                                <input type="text" className="form-control" name="eventCategory" value={eventDetails.eventCategory} onChange={handleEventChange} />
                            </div>
                            <div className="col-md-6 mb-3">
                                <label className="form-label">Afbeelding URL</label>
                                <input type="text" className="form-control" name="eventImage" value={eventDetails.eventImage} onChange={handleEventChange} />
                            </div>
                        </div>
                        <div className="mb-3">
                            <label className="form-label">Beschrijving</label>
                            <textarea className="form-control" name="eventDescription" rows={3} value={eventDetails.eventDescription} onChange={handleEventChange}></textarea>
                        </div>

                        {/* Tickets Section */}
                        {/* <h5>Tickets</h5>
                        {eventDetails.tickets.map((ticket, index) => (
                            <div key={index} className="border rounded p-3 mb-3">
                                <div className="row">
                                    <div className="col-md-4 mb-2">
                                        <label className="form-label">Ticket naam</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            name="ticketName"
                                            value={ticket.ticketName}
                                            onChange={(e) => handleTicketChange(index, e)}
                                        />
                                    </div>
                                    <div className="col-md-4 mb-2">
                                        <label className="form-label">Prijs</label>
                                        <input
                                            type="number"
                                            className="form-control"
                                            name="ticketPrice"
                                            value={ticket.ticketPrice}
                                            onChange={(e) => handleTicketChange(index, e)}
                                        />
                                    </div>
                                    <div className="col-md-4 mb-2">
                                        <label className="form-label">Maximaal beschikbaar</label>
                                        <input
                                            type="number"
                                            className="form-control"
                                            name="ticketMaxAvailable"
                                            value={ticket.ticketMaxAvailable}
                                            onChange={(e) => handleTicketChange(index, e)}
                                        />
                                    </div>
                                </div>
                            </div>
                        ))}
                        <Button variant="secondary" onClick={addTicket} className="w-100 mb-3">
                            Ticket toevoegen
                        </Button> */}
                    </form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Annuleren
                    </Button>
                    <Button variant="primary" onClick={handleAddEvent}>
                        Evenement opslaan
                    </Button>
                </Modal.Footer>
            </Modal>

            {/* Events List */}
            <div className="list-group" style={{ marginTop: '20px' }}>
                {events.map((event, index) => (
                    <div key={index} className="list-group-item d-flex justify-content-between align-items-center" style={{ padding: '20px', marginBottom: '15px' }}>
                        <div>
                            <h5>{event.eventName}</h5>
                            <p>Datum: {event.eventDateTime}</p>
                            <p>Locatie: {event.eventLocation}</p>
                        </div>
                        <Button onClick={() => navigate(`/organisatie/dashboard/${event.eventID}`)} className='align-self-end' variant="primary" style={{ padding: '10px 20px' }}>
                            Open dashboard
                        </Button>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default EventsOverview;

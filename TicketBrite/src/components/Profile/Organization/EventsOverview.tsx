import { useEffect, useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Modal, Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { Ticket, Event, ApiResponse } from '../../../Types';
import { ErrorNotification, SuccessNotification } from '../../Notifications/Notifications';
import ProtectedRoute from '../../../hooks/useAuth';

interface Events {
    verifiedEvents: Event[];
    unverifiedEvents: Event[];
}

const EventsOverview: React.FC<{ organizationID: string }> = ({ organizationID }) => {
    const navigate = useNavigate();
    const [events, setEvents] = useState<Events>({
        verifiedEvents: [],
        unverifiedEvents: []
    });

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
            fetch(`http://localhost:7150/organization/get-events/overview/${organizationID}`)
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
            const res = await fetch('http://localhost:7150/api/Organization/event/new', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(model)
            });

             const data = await res.json() as ApiResponse<string>;
       
            if(data.value && data.statusCode === 400){
                ErrorNotification({text: data.value});
            }

            if(data.statusCode === 200){
              handleClose();  
              SuccessNotification({text: "Evenement aangemaakt!"});
            }           
        } catch (error) {
            console.error('Er is een fout opgetreden:', error);
        }
    };

    return (
      <ProtectedRoute roleRequired='Organization'>
        <div className="container" style={{ marginTop: "20px" }}>
            <h3 className="mb-4">Mijn Evenementen</h3>
            <Button data-test="btn-new-event" variant="success" onClick={handleShow} style={{ padding: "10px 20px", marginBottom: "20px" }}>
                Evenement toevoegen
            </Button>

            {/* Modal for Adding Event */}
            <Modal className="add-event-modal" show={showModal} onHide={handleClose} size="lg">
                <Modal.Header closeButton>
                    <Modal.Title>Nieuw Evenement Toevoegen</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                      <form>
                          <div className="row">
                              <div className="col-md-6 mb-3">
                                  <label className="form-label">Naam evenement</label>
                                  <input data-test="event-name" type="text" className="form-control" name="eventName" value={eventDetails.eventName} onChange={handleEventChange} />
                              </div>
                              <div className="col-md-6 mb-3">
                                  <label className="form-label">Datum van evenement</label>
                                  <input data-test="event-dateTime" type="date" className="form-control" name="eventDateTime" value={eventDetails.eventDateTime} onChange={handleEventChange} />
                              </div>
                          </div>
                          <div className="row">
                              <div className="col-md-6 mb-3">
                                  <label className="form-label">Locatie</label>
                                  <input data-test="event-location" type="text" className="form-control" name="eventLocation" value={eventDetails.eventLocation} onChange={handleEventChange} />
                              </div>
                              <div className="col-md-6 mb-3">
                                  <label className="form-label">Minimum leeftijd</label>
                                  <input data-test='event-age' type="number" className="form-control" name="eventAge" value={eventDetails.eventAge} onChange={handleEventChange} />
                              </div>
                          </div>
                          <div className="row">
                              <div className="col-md-6 mb-3">
                                  <label className="form-label">Categorie</label>
                                  <input data-test="event-category" type="text" className="form-control" name="eventCategory" value={eventDetails.eventCategory} onChange={handleEventChange} />
                              </div>
                              <div className="col-md-6 mb-3">
                                  <label className="form-label">Afbeelding URL</label>
                                  <input data-test='event-image' type="text" className="form-control" name="eventImage" value={eventDetails.eventImage} onChange={handleEventChange} />
                              </div>
                          </div>
                          <div className="mb-3">
                              <label className="form-label">Beschrijving</label>
                              <textarea data-test='event-description' className="form-control" name="eventDescription" rows={3} value={eventDetails.eventDescription} onChange={handleEventChange}></textarea>
                          </div>
                      </form>
                  </Modal.Body>
                  <Modal.Footer>
                      <Button variant="secondary" onClick={handleClose}>
                          Annuleren
                      </Button>
                      <Button data-test="btn-submit-new-event" variant="primary" onClick={handleAddEvent}>
                          Evenement opslaan
                      </Button>
                  </Modal.Footer>
              </Modal>

              <div className="container mt-4">
                {/* Geverifieerde evenementen */}
                <h4 className="text-success mb-3">
                  <i className="fa-solid fa-check-circle me-2"></i> Geverifieerde Evenementen
                </h4>
                <div className="list-group">
                  {events.verifiedEvents.map((event, index) => (
                    <div
                      key={index}
                      className="list-group-item d-flex justify-content-between align-items-center border border-success shadow-sm"
                      style={{ padding: '20px', marginBottom: '15px' }}
                    >
                      <div>
                        <h5>{event.eventName}</h5>
                        <p>
                          <i className="fa-solid fa-calendar text-secondary me-2"></i>Datum: {event.eventDateTime}
                        </p>
                        <p>
                          <i className="fa-solid fa-location-dot text-secondary me-2"></i>Locatie: {event.eventLocation}
                        </p>
                      </div>
                      <Button
                        onClick={() => { navigate(`/organisatie/dashboard/${event.eventID}`)}}
                        className="align-self-end"
                        variant="outline-success"
                        style={{ padding: '10px 20px' }}
                      >
                        Open dashboard
                      </Button>
                    </div>
                  ))}
                </div>

            {/* Niet-geverifieerde evenementen */}
            <h4 className="text-danger mt-5 mb-3">
              <i className="fa-solid fa-times-circle me-2"></i> Niet-Geverifieerde Evenementen
            </h4>
            <div data-test="list-group-unverified-events" id="list-group-unverified-events" className="list-group">
              {events.unverifiedEvents.map((event, index) => (
                <div
                  key={index}
                  className="list-group-item d-flex justify-content-between align-items-center border border-danger shadow-sm bg-light"
                  style={{ padding: '20px', marginBottom: '15px' }}
                  data-test="list-group-item"
                >
                  <div>
                    <h5>{event.eventName}</h5>
                    <p>
                      <i className="fa-solid fa-calendar text-secondary me-2"></i>Datum: {event.eventDateTime}
                    </p>
                    <p>
                      <i className="fa-solid fa-location-dot text-secondary me-2"></i>Locatie: {event.eventLocation}
                    </p>
                  </div>
                  <Button
                    id="btn-open-dashboard"
                    onClick={() =>{ navigate(`/organisatie/dashboard/${event.eventID}`)}}
                    className="align-self-end"
                    variant="outline-danger"
                    style={{ padding: '10px 20px' }}
                  >
                    Open dashboard
                  </Button>
                </div>
              ))}
            </div>
          </div>
        </div>
      </ProtectedRoute>
        
    );
};

export default EventsOverview;

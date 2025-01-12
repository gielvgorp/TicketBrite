import { useEffect, useState } from "react";
import { Button, Card, Modal, Tab, Tabs } from "react-bootstrap";
import { Event } from "../../../Types";
import ProtectedRoute from "../../../hooks/useAuth";
import { ErrorNotification } from "../../Notifications/Notifications";

function VerifyEvents(){
    const [showModal, setShowModal] = useState(false);
    const [events, setEvents] = useState<Event[]>();
  const [selectedEvent, setSelectedEvent] = useState<Event | null>(null);
  const [key, setKey] = useState<string>("to-approve"); // Tab-sleutel

  // Functie om de modal te tonen
  const handleShowModal = (event: Event) => {
    setSelectedEvent(event);
    setShowModal(true);
  };

  // Functie om de modal te sluiten
  const handleCloseModal = () => {
    setSelectedEvent(null);
    setShowModal(false);
  };

  // Functie om een evenement goed te keuren
  const approveEvent = (eventID: string) => {
    console.log(`Event ${eventID} goedgekeurd`);
    // Voeg hier API-logica toe
    handleUpdateStatus(eventID, true);
  };

  // Functie om een evenement af te keuren
  const declineEvent = (eventID: string) => {
    console.log(`Event ${eventID} afgekeurd`);
    // Voeg hier API-logica toe
    handleUpdateStatus(eventID, false);
  };

  useEffect(() => {
    handleFetchEvents();
  }, [])

  const handleFetchEvents = async () => {
    try {
        const token = localStorage.getItem('jwtToken');
        // Verzend het formulier naar het endpoint
        const res = await fetch('http://localhost:7150/api/Admin/get-unverified-events', {
            method: 'GET',
            headers: {
              'Authorization': `Bearer ${token}`,
              'Content-Type': 'application/json'
            }
        });

        const data = await res.json(); // Ontvang de JSON-response

        // validation error
        if(res.status === 400){
            //setErrorMsg("Een of meerdere velden zijn leeg!");
        }

        // successful registered
        if(res.status === 200){
            setEvents(data.value);
            console.log(data);
            //navigate("/", {replace: true});
        }           
    } catch (error) {
        console.error('Er is een fout opgetreden:', error);
    }
  }

  const handleUpdateStatus = async (eventID: string, value: boolean) => {
    try {
        const token = localStorage.getItem('jwtToken');
        // Verzend het formulier naar het endpoint
        const res = await fetch(`http://localhost:7150/api/Admin/update-event-status/${eventID}`, {
            method: 'PUT',
            headers: {
              'Authorization': `Bearer ${token}`,
              'Content-Type': 'application/json'
            },
            body: JSON.stringify({
              isVerified: value
            })
        });

        const data = await res.json(); // Ontvang de JSON-response

        // validation error
        if(res.status !== 204){
            ErrorNotification({text: data.value});
        }else{
          setEvents(data.value);   
        }

    } catch (error) {
        console.error('Er is een fout opgetreden:', error);
    }
  }

  return (
    <ProtectedRoute roleRequired="Beheerder">
<div className="container my-5">
      <h2 className="text-center mb-4">Evenementen Beheren</h2>

      {/* Tabs voor goedgekeurde en niet-goedgekeurde evenementen */}
      <Tabs
        id="event-management-tabs"
        activeKey={key}
        onSelect={(k) => setKey(k || "to-approve")}
        className="mb-3"
      >
        {/* Tab: Te keuren evenementen */}
        <Tab eventKey="to-approve" title="Te Keuren Evenementen">
          <div className="row">
            {events != null && events.map((event) => (
              <div key={event.eventID} className="col-md-4 mb-4">
                <Card>
                  <Card.Body>
                    <Card.Title>{event.eventName}</Card.Title>
                    <Card.Text>
                      <strong>Datum:</strong> {event.eventDateTime}
                    </Card.Text>
                    <Card.Text>
                      <strong>Locatie:</strong> {event.eventLocation}
                    </Card.Text>
                    <Button
                      variant="outline-primary"
                      onClick={() => handleShowModal(event)}
                    >
                      Bekijk Details
                    </Button>
                    <div className="mt-3">
                      <Button
                        variant="success"
                        className="me-2"
                        onClick={() => approveEvent(event.eventID)}
                      >
                        Goedkeuren
                      </Button>
                      <Button
                        variant="danger"
                        onClick={() => declineEvent(event.eventID)}
                      >
                        Afkeuren
                      </Button>
                    </div>
                  </Card.Body>
                </Card>
              </div>
            ))}
          </div>
        </Tab>
      </Tabs>

      {/* Modal: Evenementdetails */}
      <Modal show={showModal} onHide={handleCloseModal} size="lg">
        <Modal.Header closeButton>
          <Modal.Title>{selectedEvent?.eventName}</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          {selectedEvent && (
            <>
              <p>
                <strong>Datum & Tijd:</strong> {selectedEvent.eventDateTime}
              </p>
              <p>
                <strong>Locatie:</strong> {selectedEvent.eventLocation}
              </p>
              <p>
                <strong>Beschrijving:</strong> {selectedEvent.eventDescription}
              </p>
            </>
          )}
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleCloseModal}>
            Sluiten
          </Button>
          <Button
            variant="success"
            onClick={() => {
              if(selectedEvent){
                approveEvent(selectedEvent.eventID);
              }
              handleCloseModal();
            }}
          >
            Goedkeuren
          </Button>
          <Button
            variant="danger"
            onClick={() => {
              if(selectedEvent){
                declineEvent(selectedEvent.eventID);
              }
              handleCloseModal();
            }}
          >
            Afkeuren
          </Button>
        </Modal.Footer>
      </Modal>
    </div>
    </ProtectedRoute>
    
  );
}

export default VerifyEvents;
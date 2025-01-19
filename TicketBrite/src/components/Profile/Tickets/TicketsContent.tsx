import { useState, useEffect } from 'react';
import { Card, Button, ListGroup, Modal, Badge } from 'react-bootstrap';
import { Ticket, Purchase } from '../../../Types';
import './TicketContent.css';

interface PurchaseViewModel {
  userPurchase: Purchase;
  eventTickets: Ticket[];
}

function TicketContent() {
  const [purchases, setPurchases] = useState<PurchaseViewModel[]>([]);
  const [selectedPurchase, setSelectedPurchase] = useState<PurchaseViewModel | null>(null);
  const [showModal, setShowModal] = useState(false);
  const [showQRModal, setShowQRModal] = useState(false);
  const [selectedTicket, setSelectedTicket] = useState<Ticket | null>(null);


  useEffect(() => {
    fetchEvents();
}, []);

const fetchEvents = async () => {
    const token = localStorage.getItem("jwtToken");

    try {
      // Verzend het formulier naar het endpoint
      const res = await fetch(`http://localhost:7150/user/purchase`, {
          method: 'GET',
          headers: {
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'application/json'
        }
      });

      const data = await res.json(); // Ontvang de JSON-response

      console.log("Guest auth reponse: ",data);  
      
      if(data.statusCode !== 200){
          console.log("error while loggin in as guest:", data);
      }

      // successful registered
      if(data.statusCode === 200){
          console.log(data.value);
          setPurchases(data.value);
      }           

  } catch (error) {
      console.error('Er is een fout opgetreden:', error);
  }
};

  const handlePurchaseClick = (purchase: PurchaseViewModel) => {
    setSelectedPurchase(purchase);
    setShowModal(true);
  };

  const handleCloseModal = () => {
    setShowModal(false);
    setSelectedPurchase(null);
  };

  const handleOpenQRModal = (ticket: Ticket) => {
    setSelectedTicket(ticket);
    setShowQRModal(true);
  };
  
  const handleCloseQRModal = () => {
    setSelectedTicket(null);
    setShowQRModal(false);
  };

  return (
    <div className="container my-5">
      <h2 className="text-center mb-4">Mijn Aankopen</h2>
      {
        purchases.length > 0 ? 
        <>
        <Card className={showQRModal ? 'shadow-sm blur-background' : 'shadow-sm'}>
        <Card.Body>
          <ListGroup variant="flush">
            {purchases.map((purchase) => (
              <ListGroup.Item key={purchase.userPurchase.purchaseID} className="d-flex justify-content-between align-items-center">
                <div>
                  <h5>
                    <i className="fa-solid fa-ticket text-primary"></i> Aankoop ID: <span data-test="purchase-id">{purchase.userPurchase.purchaseID}</span>
                  </h5>
                  <p>
                    <i className="fa-solid fa-calendar text-secondary"></i> Aankoopdatum: {"01-01-2024"}
                  </p>
                </div>
                <Button variant="outline-primary" onClick={() => handlePurchaseClick(purchase)}>
                  Bekijk Details
                </Button>
              </ListGroup.Item>
            ))}
          </ListGroup>
        </Card.Body>
      </Card>

      {/* Modal voor aankoopdetails */}
      <Modal className={showQRModal ? 'blur-background' : ''} show={showModal} onHide={handleCloseModal} size="lg">
        <Modal.Header closeButton>
          <Modal.Title>Aankoop Details</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          {selectedPurchase && (
            <>
              <div className="d-flex justify-content-between mb-3 d-flex align-items-center">
                <h5>Aankoop ID: {selectedPurchase.userPurchase.purchaseID}</h5>
                <Badge bg="success">Geldig</Badge>
              </div>
              <p>Aankoopdatum: {"21-01-2025"}</p>
              <ListGroup variant="flush">
                {selectedPurchase.eventTickets.map((ticket: Ticket) => (
                  <ListGroup.Item key={ticket.ticketID} className="d-flex justify-content-between align-items-center">
                    <div>
                      <h6>
                         <i className="fa-solid fa-ticket text-primary"></i> {ticket.ticketName}
                      </h6>
                      <p>
                        <i className="fa-solid fa-calendar text-secondary"></i> Datum & Tijd: {"22-01-2025"}
                      </p>
                    </div>
                    <Button
      variant="outline-primary"
      size="sm"
      onClick={() => handleOpenQRModal(ticket)}
      className="me-2"
    >
      Open Ticket
    </Button>
                    <div className="text-muted d-flex align-items-center">
                        <i className="fa-solid fa-euro-sign text-warning me-1"></i>
                      <strong>{ticket.ticketPrice}</strong>
                    </div>
                  </ListGroup.Item>
                ))}
              </ListGroup>
            </>
          )}
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleCloseModal}>
            Sluiten
          </Button>
        </Modal.Footer>
      </Modal>
      <Modal show={showQRModal} onHide={handleCloseQRModal} size="sm" centered>
  <Modal.Header closeButton>
    <Modal.Title>Ticket QR-code</Modal.Title>
  </Modal.Header>
  <Modal.Body className="text-center">
    {selectedTicket && (
      <div className='d-flex flex-column'>
        <h5 className='fw-bold'>{selectedTicket.ticketName}</h5>
        <div className="d-flex justify-content-center">
          <img
            src={`/src/assets/QRlogo.png`}
            alt={`QR Code for ticket ${selectedTicket.ticketID}`}
            className="img-fluid"
          />
        </div>
        <i>Scan je ticket hier!</i>
      </div>
    )}
  </Modal.Body>
  <Modal.Footer>
    <Button variant="secondary" onClick={handleCloseQRModal}>
      Sluiten
    </Button>
  </Modal.Footer>
</Modal>
        </> : <h3 className='text-center pt-3'>Je hebt nog geen aankopen!</h3>
      }
     
    </div>
  );
}

export default TicketContent;

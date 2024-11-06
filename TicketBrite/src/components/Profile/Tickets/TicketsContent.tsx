import React, { useState, useEffect } from 'react';
import { Card, Button, ListGroup, Modal, Badge } from 'react-bootstrap';
import { Ticket } from '../../../Types';

interface Purchase {
  purchaseId: string;
  purchaseDate: string;
  tickets: Ticket[];
}

const TicketContent: React.FC = () => {
  const [purchases, setPurchases] = useState<Purchase[]>([]);
  const [selectedPurchase, setSelectedPurchase] = useState<Purchase | null>(null);
  const [showModal, setShowModal] = useState(false);

  useEffect(() => {
    setPurchases([
      {
        purchaseId: '12345',
        purchaseDate: '2024-11-01',
        tickets: [
          { ticketID: "T1", ticketName: 'Concert Ticket', ticketPrice: "50", eventDateTime: '2024-12-01 19:00' },
          { ticketID: 'T2', ticketName: 'VIP Pass', ticketPrice: "100", eventDateTime: '2024-12-01 19:00' },
        ],
      },
      {
        purchaseId: '67890',
        purchaseDate: '2024-10-15',
        tickets: [
          { ticketID: 'T3', ticketName: 'Festival Ticket', ticketPrice: "80", eventDateTime: '2024-11-15 15:00' },
        ],
      },
    ]);
  }, []);

  const handlePurchaseClick = (purchase: Purchase) => {
    setSelectedPurchase(purchase);
    setShowModal(true);
  };

  const handleCloseModal = () => {
    setShowModal(false);
    setSelectedPurchase(null);
  };

  return (
    <div className="container my-5">
      <h2 className="text-center mb-4">Mijn Aankopen</h2>
      <Card className="shadow-sm">
        <Card.Body>
          <ListGroup variant="flush">
            {purchases.map((purchase) => (
              <ListGroup.Item key={purchase.purchaseId} className="d-flex justify-content-between align-items-center">
                <div>
                  <h5>
                    <i className="fa-solid fa-ticket text-primary"></i> Aankoop ID: {purchase.purchaseId}
                  </h5>
                  <p>
                    <i className="fa-solid fa-calendar text-secondary"></i> Aankoopdatum: {purchase.purchaseDate}
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
      <Modal show={showModal} onHide={handleCloseModal} size="lg">
        <Modal.Header closeButton>
          <Modal.Title>Aankoop Details</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          {selectedPurchase && (
            <>
              <div className="d-flex justify-content-between mb-3 d-flex align-items-center">
                <h5>Aankoop ID: {selectedPurchase.purchaseId}</h5>
                <Badge bg="danger">Verlopen</Badge>
              </div>
              <p>Aankoopdatum: {selectedPurchase.purchaseDate}</p>
              <ListGroup variant="flush">
                {selectedPurchase.tickets.map((ticket) => (
                  <ListGroup.Item key={ticket.ticketID} className="d-flex justify-content-between align-items-center">
                    <div>
                      <h6>
                         <i className="fa-solid fa-ticket text-primary"></i> {ticket.ticketName}
                      </h6>
                      <p>
                        <i className="fa-solid fa-calendar text-secondary"></i> Datum & Tijd: {/*ticket.eventDateTime*/}
                      </p>
                    </div>
                    <div className="text-muted d-flex align-items-center">
                        <i className="fa-solid fa-euro-sign text-warning me-1"></i>
                      <strong>{/*ticket.ticketPrice.toFixed(2)*/}</strong>
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
    </div>
  );
};

export default TicketContent;

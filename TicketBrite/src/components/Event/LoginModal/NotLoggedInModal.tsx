import React, { useState } from 'react';
import { Modal, Button, Form } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { useAuth } from "../../../AuthContext";

interface NotLoggedInModalProps {
  onHide: () => void;
  onGuestContinue: (email: string, fullName: string) => void;
  onLogin: () => void;
}

const NotLoggedInModal: React.FC<NotLoggedInModalProps> = ({ onHide, onGuestContinue, onLogin }) => {
    const {login} = useAuth();
  const [email, setEmail] = useState('');
  const [fullName, setFullName] = useState('');
  const navigate = useNavigate();
  const [errorMsg, setErrorMsg] = useState("");

  const handleGuestContinue = () => {
    if (email && fullName) {
      onGuestContinue(email, fullName);
      handleCreateGuest(email, fullName);
    } else {
      alert("Vul alstublieft zowel uw e-mailadres als volledige naam in.");
    }
  };

  const handleCreateGuest = async (email: string, fullName: string) =>{
    try {
        // Verzend het formulier naar het endpoint
        const res = await fetch(`https://localhost:7150/guest/create`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                guestEmail: email,
                guestName: fullName
            }) // Zet de formData om naar JSON
        });

        const data = await res.json(); // Ontvang de JSON-response

        console.log(data);

        // validation error
        if(data.statusCode !== 200){
            setErrorMsg(data.value);
        }

        // successful registered
        if(data.statusCode === 200){
            console.log(data);
            login(data.value.token);
            navigate("/", {replace: true});
        }           
    } catch (error) {
        console.error('Er is een fout opgetreden:', error);
    }
  }

  return (
    <Modal show={true} onHide={onHide} centered>
      <Modal.Header closeButton>
        <Modal.Title>Niet ingelogd</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <p>
          U bent momenteel niet ingelogd. Het wordt aangeraden om in te loggen voor een betere gebruikerservaring, maar
          u kunt ook doorgaan als gast.
        </p>
        <Form>
          <Form.Group controlId="guestEmail" className="mb-3">
            <Form.Label>E-mailadres</Form.Label>
            <Form.Control
              type="email"
              placeholder="Voer uw e-mailadres in"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
            />
          </Form.Group>
          <Form.Group controlId="guestFullName" className="mb-3">
            <Form.Label>Volledige naam</Form.Label>
            <Form.Control
              type="text"
              placeholder="Voer uw volledige naam in"
              value={fullName}
              onChange={(e) => setFullName(e.target.value)}
            />
          </Form.Group>
          <p className='text-danger'>{errorMsg}</p>
        </Form>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={onLogin}>
          Inloggen
        </Button>
        <Button variant="primary" onClick={handleGuestContinue}>
          Doorgaan als gast
        </Button>
      </Modal.Footer>
    </Modal>
  );
};

export default NotLoggedInModal;

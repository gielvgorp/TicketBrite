import React from 'react';
import { Modal, Button } from 'react-bootstrap';

interface NotLoggedInModalProps {
    show: boolean;
    handleClose: () => void;
    handleLogin: () => void;
    handleContinue: () => void;
}

const NotLoggedInModal: React.FC<NotLoggedInModalProps> = ({
    show,
    handleClose,
    handleLogin,
    handleContinue,
}) => {
    return (
        <Modal show={show} onHide={handleClose}>
            <Modal.Header closeButton>
                <Modal.Title>Login Vereist</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <p>U bent momenteel niet ingelogd. Het wordt sterk aanbevolen om eerst in te loggen voor een betere ervaring en om uw aankopen te beheren. U kunt echter ook doorgaan met het kopen van een ticket zonder een account aan te maken.</p>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={handleClose}>
                    Annuleren
                </Button>
                <Button variant="primary" onClick={handleLogin}>
                    Inloggen
                </Button>
                <Button variant="outline-primary" onClick={handleContinue}>
                    Doorgaan zonder account
                </Button>
            </Modal.Footer>
        </Modal>
    );
};

export default NotLoggedInModal;

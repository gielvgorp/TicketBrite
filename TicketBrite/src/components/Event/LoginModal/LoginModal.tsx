import React, { useEffect, useState } from 'react';
import NotLoggedInModal from './NotLoggedInModal';
import { Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';

type Props = {
    showModal: boolean;
}

function TicketPurchaseComponent(props: Props){
    const [showModal, setShowModal] = useState(false);
    const isLoggedIn = false; // Replace with your authentication logic
    const navigate = useNavigate();

    useEffect(() => {
        setShowModal(props.showModal);
    }, [props.showModal]);

    const handleShowModal = () => setShowModal(true);
    const handleCloseModal = () => setShowModal(false);
    const handleLogin = () => {
        // Redirect to login or open login form
        navigate("/authenticatie");
        handleCloseModal();
    };
    const handleContinueWithoutAccount = () => {
        // Logic to continue without an account
        console.log('Continuing without account...');
        handleCloseModal();
    };

    const handlePurchaseTicket = () => {
        if (!isLoggedIn) {
            handleShowModal();
        } else {
            // Proceed with ticket purchase logic
            console.log('Proceeding with ticket purchase...');
        }
    };

    return (
        <div>
            {/* <Button onClick={handlePurchaseTicket}>Koop Ticket</Button> */}
            <NotLoggedInModal
                show={showModal}
                handleClose={handleCloseModal}
                handleLogin={handleLogin}
                handleContinue={handleContinueWithoutAccount}
            />
        </div>
    );
}

export default TicketPurchaseComponent;

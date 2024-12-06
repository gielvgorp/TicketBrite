import { useEffect, useState } from 'react';
import styles from '../EventInfo.module.css'
import TicketSelector from '../components/Event/TicketsSelector/TicketSelector';
import { useNavigate, useParams } from 'react-router-dom';
import { Event, Ticket } from '../Types'
import LoginModal from '../components/Event/LoginModal/LoginModal';
import {useAuth} from '../AuthContext';

function EventInfo(){
    const { id } = useParams();
    const navigate = useNavigate();
    const [event, setEvent] = useState<Event>();  // State to store the fetched events
    const [tickets, setTickets] = useState<Ticket[]>([]);  // State to store the fetched events
    const [loading, setLoading] = useState(true); // State to show loading spinner or message
    const [showTickets, setShowTickets] = useState(false);
    const [selectedTickets, setSelectedTickets] = useState<{ ticketID: string, quantity: number }[]>([]);  // Array to store ticketID and quantity
    const [showLoginWarning, setShowLoginWarning] = useState(false);
    const { isAuthenticated } = useAuth();

    useEffect(() => {
        // Check if the parsed ID is NaN and navigate if necessary
        if (id === undefined) {
          navigate(`/events`, { replace: true });
        }
    }, [id, navigate]);

    useEffect(() => {      
        console.log(id); 
          fetch(`https://localhost:7150/get-event/${id}`)
              .then(response => response.json())
              .then(data => {
                setLoading(false);
                setEvent(data.value.event);
                setTickets(data.value.tickets);
              })
              .catch(error => {
                  console.error('Error fetching data:', error);  
                  setLoading(false);
              });
    }, []);

    if (loading) {
        return <p>Loading...</p>;
    }

    const handleTicketSelect = (ticketID: string, quantity: number) => {
        setSelectedTickets((prevSelected) => {
            // Check if ticket already exists in the array
            const existingTicket = prevSelected.find(ticket => ticket.ticketID === ticketID);

            if (existingTicket) {
                // Update quantity if the ticket exists
                return prevSelected.map(ticket =>
                    ticket.ticketID === ticketID ? { ...ticket, quantity } : ticket
                );
            } else {
                // Add new ticket with quantity if it doesn't exist
                return [...prevSelected, { ticketID, quantity }];
            }
        });
        console.log(selectedTickets);
    };

    const handlePayTickets = () => {
        if (!isAuthenticated) {
            setShowLoginWarning(true);
            console.log("Not logged in!");
            return;
        } 

        handleReserveTickets();
    };

    const handleReserveTickets = async () => {
        const token = localStorage.getItem('jwtToken');

        try {
            const response = await fetch('https://localhost:7150/ticket/set-reserve', {
                method: 'POST',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(selectedTickets) // Zet de formData om naar JSON
            });

            if (!response.ok) {
                throw new Error('Fout bij het ophalen van gebruikersgegevens');
            }

            const data = await response.json();
            //setUser(data);
        } catch (error) {
            console.error('Er is een fout opgetreden:', error);
        } finally {
            setLoading(false);
        }
    }

    const handleShowTickets = () => {
        setShowTickets(!showTickets);
        console.log(showTickets);
    };

    const optionsDate: Intl.DateTimeFormatOptions = {
        weekday: 'short',
        day: 'numeric',
        month: 'short',
        year: 'numeric'
    };
      
    return (
        <>
            {
                event ? (
                <div className={styles.mainContainer}>
                    <div className={`${styles.eventInfoContainer} col-9`}>
                        <div className={`${styles.header} p-3 d-flex align-items-center shadow`}>
                            <img className={styles.eventImage} src={event.eventImage} alt="" />
                            <div className={`${styles.eventInfo} ps-3`}>
                                <h1 data-test="event-name">{event.eventName}</h1>
                                <p>{event.eventLocation}</p>
                                <p>{new Date(event.eventDateTime).toLocaleDateString('nl-NL', optionsDate)} {new Date(event.eventDateTime).toLocaleTimeString('nl-NL', { hour: '2-digit', minute: '2-digit' })}</p>
                                <p>Leeftijdsrestrictie: {event.eventAge}+</p>
                            </div>
                            <button onClick={handleShowTickets} className='btn btn-primary align-self-end ms-auto px-3 py-2'>Koop je tickets <i className="fa-solid fa-chevron-right px-2"></i></button>
                        </div>
                        <div className="w-100 p-5">
                            <h1 className='font'>{event.eventName}</h1>
                            <p>{event.eventDescription}</p>
                        </div>
                    </div>
                    <div className={`${styles.sideBar} shadow ${showTickets ? styles.show : ''} col-3`}>
                        <div className={`${styles.topContainer} d-flex align-items-center justify-content-center`}>
                            <h4>Koop nu je tickets van Snelle!</h4>
                        </div>
                        <div data-test="ticket-selector-container" className={`${styles.ticketSelectorContainer}`}>
                            {
                                tickets.length > 0 ? (
                                    tickets.map((ticket: Ticket) => (
                                        <TicketSelector
                                            key={ticket.ticketID}
                                            ticket={ticket}
                                            onTicketSelect={(amount) => handleTicketSelect(ticket.ticketID, amount)}
                                            maxAmount={10}
                                            ticketPrice={ticket.ticketPrice}
                                            name={ticket.ticketName}
                                        />
                                    ))
                                ) : (
                                    <h5 className='pt-3'>Er zijn (nog) geen tickets beschikbaar</h5>
                                )
                            }     
                        </div>
                        <div className='align-self-end p-3'>
                            <button data-test="btn-buy-tickets" onClick={handlePayTickets} className='btn btn-success px-4 py-2'>Betalen <i className="fa-solid fa-money-bill px-2"></i></button>
                        </div>
                    </div>
                    { showLoginWarning && <LoginModal setShowModal={setShowLoginWarning} showModal={showLoginWarning} /> }
                </div>
                ) : (
                    <h3>Loading...</h3>
                )
            }
        </>  
    );
}

export default EventInfo;

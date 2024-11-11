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
    const [tickets, setTickets] = useState<Ticket | any>(null);  // State to store the fetched events
    const [loading, setLoading] = useState(true); // State to show loading spinner or message
    const [showTickets, setShowTickets] = useState(false);
    const [ticketAmount, setTicketAmount] = useState(0);
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
                setTickets(data.value.tickets)
              })
              .catch(error => {
                  console.error('Error fetching data:', error);  
                  setLoading(false);
              });
      }, []);

    if (loading) {
        return <p>Loading...</p>;
    }

    const handlePayTickets = () => {
        if(!isAuthenticated){
            setShowLoginWarning(true);
            console.log("not logged in!");
            return;
        } 

        console.log("loggin!");
    }

    const handleShowTickets = () => {
        setShowTickets(!showTickets);
        console.log(showTickets);
    }

    const optionsDate: Intl.DateTimeFormatOptions = {
        weekday: 'short', // Zo
        day: 'numeric',   // 1
        month: 'short',   // okt.
        year: 'numeric'   // 2024
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
                            <h1>{event.eventName}</h1>
                            <p>{event.eventLocation}</p>
                            <p>{new Date(event.eventDateTime).toLocaleDateString('nl-NL', optionsDate)} {new Date(event.eventDateTime).toLocaleTimeString('nl-NL', { hour: '2-digit', minute: '2-digit' })}</p>
                            <p>Leeftijdsrestrictie: {event.eventAge}+</p>
                        </div>
                        <button onClick={handleShowTickets} className='btn btn-primary align-self-end ms-auto px-3 py-2'>Koop je tickets <i className="fa-solid fa-chevron-right px-2"></i></button>
                    </div>
                    <div className="w-100 p-5">
                        <h1 className='font'>{event.eventName}</h1>
                        <p className=''>
                            {event.eventDescription}
                        </p>
                    </div>
                </div>
                <div className={`${styles.sideBar} shadow ${showTickets ? styles.show : ''} col-3`}>
                    <div className={`${styles.topContainer} d-flex align-items-center justify-content-center`}>
                        <h4>Koop nu je tickets van Snelle!</h4>
                    </div>
                    <div className={`${styles.ticketSelectorContainer}`}>
                        {
                            tickets.length > 0 ? (
                                tickets.map((ticket: Ticket) => <TicketSelector ticket={ticket} onTicketSelect={(value) => setTicketAmount(value + ticketAmount)} maxAmount={10} ticketPrice={ticket.ticketPrice} name={ticket.ticketName} />)
                            ) : (
                                <h5 className='pt-3'>Er zijn (nog) geen tickets beschikbaar</h5>
                            )
                           
                        }     
                    </div>
                    <div className='align-self-end p-3'>
                        <button onClick={handlePayTickets} className='btn btn-success px-4 py-2'>Betalen <i className="fa-solid fa-money-bill px-2"></i></button>
                    </div>
                    <div className={`${styles.bottomContainer} px-3 py-2 d-flex align-items-center justify-content-between`}>
                        <p>Maximale aantal tickets: 10</p>
                        <p><i className="fa-solid fa-ticket"></i> <span className='ps-2'>{ticketAmount}</span></p>
                    </div>
                </div>
                { showLoginWarning && <LoginModal showModal={showLoginWarning} /> }
            </div>
            ) : (
                <h3>Loading...</h3>
            )
        } 

    </>  )
}

export default EventInfo;
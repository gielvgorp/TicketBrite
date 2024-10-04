import { useEffect, useState } from 'react';
import styles from '../EventInfo.module.css'
import TicketSelector from '../components/Event/TicketsSelector/TicketSelector';
import events from '../mockdata';
import { useLocation, useNavigate, useParams } from 'react-router-dom';

function EventInfo(){
    const { id } = useParams();
    const parsedID = parseInt(id ?? "0");
    const navigate = useNavigate();

    useEffect(() => {
        // Check if the parsed ID is NaN and navigate if necessary
        if (isNaN(parsedID)) {
          navigate(`/events`, { replace: true });
        }
      }, [parsedID, navigate]); // Dependency array includes parsedID and navigate

    const event = events().filter(event => event.id === parsedID)[0];
    console.log(event);

    const [showTickets, setShowTickets] = useState(true);

    const handleShowTickets = () => {
        setShowTickets(!showTickets);
        console.log(showTickets);
    }

    const [ticketAmount, setTicketAmount] = useState(0);

    const optionsDate: Intl.DateTimeFormatOptions = {
        weekday: 'short', // Zo
        day: 'numeric',   // 1
        month: 'short',   // okt.
        year: 'numeric'   // 2024
      };
      
      const formattedDate = event.eventDate.toLocaleDateString('nl-NL', optionsDate); // "Zo 1 okt. 2024"
      const formattedTime = event.eventDate.toLocaleTimeString('nl-NL', { hour: '2-digit', minute: '2-digit' }); // "19:15"
      const capitalizedDate = formattedDate.charAt(0).toUpperCase() + formattedDate.slice(1);
    return (
        <div className={styles.mainContainer}>
            <div className={styles.eventInfoContainer}>
                <div className={`${styles.header} p-3 d-flex align-items-center shadow`}>
                    <img className={styles.eventImage} src="https://www.agentsafterall.nl/wp-content/uploads/Naamloos-1-header-1-1600x740.jpg" alt="" />
                    <div className={`${styles.eventInfo} ps-3`}>
                        <h1>{event.eventName}</h1>
                        <p>{event.eventLocation}</p>
                        <p>{capitalizedDate} {formattedTime}</p>
                        <p>Leeftijdsrestrictie: {event.eventAge}+</p>
                    </div>
                    <button onClick={handleShowTickets} className='btn btn-primary align-self-end ms-auto px-3 py-2'>Koop je tickets <i className="fa-solid fa-chevron-right px-2"></i></button>
                </div>
            </div>
            <div className={`${styles.sideBar} shadow ${showTickets ? styles.show : ''}`}>
                <div className={`${styles.topContainer} d-flex align-items-center justify-content-center`}>
                    <h4>Koop nu je tickets van Snelle!</h4>
                </div>
                <div className={`${styles.ticketSelectorContainer}`}>
                    <TicketSelector onTicketSelect={(value) => setTicketAmount(value)} maxAmount={10} ticketPrice={45} name="Staan plaatsen" /> 
                    <TicketSelector onTicketSelect={(value) => setTicketAmount(value)} maxAmount={10} ticketPrice={65} name="Zit plaatsen" /> 
                    <TicketSelector onTicketSelect={(value) => setTicketAmount(value)} maxAmount={10} ticketPrice={99} name="VIP Area" />        
                </div>
                <div className='align-self-end p-3'>
                    <button className='btn btn-success px-4 py-2'>Volgende <i className="fa-solid fa-chevron-right ps-3"></i></button>
                </div>
                <div className={`${styles.bottomContainer} px-3 py-2 d-flex align-items-center justify-content-between`}>
                    <p>Maximale aantal tickets: 10</p>
                    <p><i className="fa-solid fa-ticket"></i> <span className='ps-2'>{ticketAmount}</span></p>
                </div>
            </div>
        </div>
    );
}

export default EventInfo;
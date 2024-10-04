import { useState } from 'react';
import styles from '../EventInfo.module.css'
import TicketSelector from '../components/Event/TicketsSelector/TicketSelector';

function EventInfo(){
    const [showTickets, setShowTickets] = useState(true);

    const handleShowTickets = () => {
        setShowTickets(!showTickets);
        console.log(showTickets);
    }

    return (
        <div className={styles.mainContainer}>
            <div className={styles.eventInfoContainer}>
                <div className={`${styles.header} p-3 d-flex align-items-center shadow`}>
                    <img className={styles.eventImage} src="https://www.agentsafterall.nl/wp-content/uploads/Naamloos-1-header-1-1600x740.jpg" alt="" />
                    <div className={`${styles.eventInfo} ps-3`}>
                        <h1>Snelle: LIVE!</h1>
                        <p>Johan Cruijff ArenA</p>
                        <p>Zo 1 okt. 2024 19:15</p>
                        <p>Leeftijdsrestrictie: 12+</p>
                    </div>
                    <button onClick={handleShowTickets} className='btn btn-primary align-self-end ms-auto px-3 py-2'>Koop je tickets <i className="fa-solid fa-chevron-right px-2"></i></button>
                </div>
            </div>
            <div className={`${styles.sideBar} shadow ${showTickets ? styles.show : ''}`}>
                <div className={`${styles.topContainer} d-flex align-items-center justify-content-center`}>
                    <h4>Koop nu je tickets van Snelle!</h4>
                </div>
                <div className={`${styles.ticketSelectorContainer}`}>
                    <TicketSelector maxAmount={10} name="Staan plaatsen" /> 
                    <TicketSelector maxAmount={10} name="Zit plaatsen" /> 
                    <TicketSelector maxAmount={10} name="VIP Area" />        
                </div>
                <div className={`${styles.bottomContainer} px-3 py-2 d-flex align-items-center`}>
                    <p>Maximale aantal tickets: 10</p>
                </div>
            </div>
        </div>
    );
}

export default EventInfo;
import { Navigate } from 'react-router-dom';
import styles from './EventItem.module.css'

function EventItem(){
    return (
        <div className={`${styles.eventItem} w-100 bg-white d-flex pb-3`}>
            <div className={`${styles.dateContaienr} d-flex h-100 flex-column justify-content-center align-items-center`}>
                <h3 className='text-secondary fw-light'>Okt</h3>
                <h3 className='text-secondary fw-light'>1</h3>
            </div>
            <div className={`${styles.eventInfo} d-flex flex-column justify-content-center gap-1`}>
                <span className={styles.subInfo}><i className="fa-solid fa-clock"></i> N/A - 00:00</span>
                <h5 className={styles.eventName}>Unknown event</h5>
                <span className={styles.subInfo}><i className="fa-solid fa-location-dot"></i> Onbekend</span>
            </div>
            <button className={`${styles.ticketBtn} btn btn-primary ms-auto align-self-center h-25 me-3`}>Tickets <i className="fa-solid fa-chevron-right"></i></button>
        </div>
    );
}

export default EventItem
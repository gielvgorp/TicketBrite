import { useNavigate } from 'react-router-dom';
import styles from './EventItem.module.css'
import events from '../../../mockdata';

type Props = {
    id: number
}

function EventItem({id}: Props){
    const navigate = useNavigate();
    const event = events().filter(event => event.id === id)[0];

    return (
        <div className={`${styles.eventItem} w-100 bg-white d-flex pb-3`}>
            <div className={`${styles.dateContaienr} d-flex h-100 flex-column justify-content-center align-items-center`}>
                <h3 className='text-secondary fw-light'>{event.eventDate.toLocaleDateString('nl-NL', {month: 'short'})}</h3>
                <h3 className='text-secondary fw-light'>{event.eventDate.toLocaleDateString('nl-NL', {day: 'numeric'})}</h3>
            </div>
            <div className={`${styles.eventInfo} d-flex flex-column justify-content-center gap-1`}>
                <span className={styles.subInfo}><i className="fa-solid fa-clock"></i> {event.eventDate.toLocaleDateString('nl-NL', {weekday: 'short'})} - {event.eventDate.toLocaleTimeString('nl-NL', { hour: '2-digit', minute: '2-digit' })}</span>
                <h5 className={styles.eventName}>{event.eventName}</h5>
                <span className={styles.subInfo}><i className="fa-solid fa-location-dot"></i> {event.eventLocation}</span>
            </div>
            <button onClick={() => navigate(`/event/${id}`, { replace: true })} className={`${styles.ticketBtn} btn btn-primary ms-auto align-self-center h-25 me-3`}>Tickets <i className="fa-solid fa-chevron-right"></i></button>
        </div>
    );
}

export default EventItem
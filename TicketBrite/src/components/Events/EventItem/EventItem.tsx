import { useNavigate } from 'react-router-dom';
import styles from './EventItem.module.css'
import { useEffect, useState } from 'react';
import { Event } from '../../../Types'
type Props = {
    id: string;
}



function EventItem({id}: Props){
    const navigate = useNavigate();
    const [event, setEvent] = useState<Event | null>(null);
    const [loading, setLoading] = useState(true); // State to show loading spinner or message

    useEffect(() => {      
          fetch(`http://localhost:7150/api/Event/event/${id}`)
              .then(response => response.json())
              .then(data => {
                    setLoading(false);
                  setEvent(data.value.event);
                  console.log(data.value);
              })
              .catch(error => {
                  console.error('Error fetching data:', error);  
                  setLoading(false);
              });
      }, []);

    if (loading) {
        return <p>Loading...</p>;  // Display loading message while fetching data
    }

    return (
        <div className={`${styles.eventItem} w-100 bg-white d-flex pb-3`}>
            <div className={`${styles.dateContaienr} d-flex h-100 flex-column justify-content-center align-items-center`}>
            {event ? (<>
                <h3 className='text-secondary fw-light'>{new Date(event.eventDateTime).toLocaleDateString('nl-NL', {month: 'short'})}</h3>
                <h3 className='text-secondary fw-light'>{new Date(event.eventDateTime).toLocaleDateString('nl-NL', {day: 'numeric'})}</h3>
            </>

    ) : (
      <h3>Loading...</h3>
    )}
                
                
            </div>
            <div className={`${styles.eventInfo} d-flex flex-column justify-content-center gap-1`}>
                {
                    event ? (
                        <>
                            <span className={styles.subInfo}><i className="fa-solid fa-clock"></i> {new Date(event.eventDateTime).toLocaleDateString('nl-NL', {weekday: 'short'})} - {new Date(event.eventDateTime).toLocaleTimeString('nl-NL', { hour: '2-digit', minute: '2-digit' })}</span>
                            <h5 className={styles.eventName}>{event.eventName}</h5>
                            <span className={styles.subInfo}><i className="fa-solid fa-location-dot"></i> {event.eventLocation}</span>
                        </>
                    ): (
                        <h3>Loading...</h3>
                      )
                }
               
            </div>
            <button onClick={() => navigate(`/event/${id}`, { replace: true })} className={`${styles.ticketBtn} btn btn-primary ms-auto align-self-center h-25 me-3`}>Tickets <i className="fa-solid fa-chevron-right"></i></button>
        </div>
    );
}

export default EventItem
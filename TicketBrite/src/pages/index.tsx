import { useEffect, useState } from 'react';
import GridItem from '../components/Index/GridItem/GridItem'
import styles from '../index.module.css'

function Index(){
    const [events, setEvents] = useState<Array<Event>>([]);  // State to store the fetched events
    const [loading, setLoading] = useState(true); // State to show loading spinner or message

    useEffect(() => {       
        fetch(`http://localhost:7150/api/events/verified`)
        .then(response => response.json())
        .then(data => {
            console.log("Events: ", data);
            setEvents(data.value);
            setLoading(false);
        })
        .catch(error => {
            console.error('Error fetching data:', error);  
            setLoading(false);
        });
    }, []);

    if (loading) {
        return <p>Loading...</p>;
    }

    return (
       <>
        <section className={`w-100 ${styles.welcomeSection}`}>
            <div className={`${styles.overlay} w-100 h-100 p-5 d-flex align-items-end`}>
                <div className={styles.overlayInfo}>
                    <h1>Lorem, ipsum dolor.</h1>
                    <p>
                        Lorem ipsum dolor sit amet consectetur adipisicing elit. Minima, laboriosam.  
                    </p>
                    <button className='btn btn-primary mt-3 px-5 py-2'>Ontdek meer!</button>
                </div>
            </div>
        </section>
        <div className={`w-100 p-5 gap-5 ${styles.eventsGrid}`} data-test="event-grid">
            {events.map((event: any, index: number) => <GridItem image={event.eventImage} eventID={event.eventID} key={index} category={event.category} artist={event.eventName} />)}
        </div>
       </>
    )
}

export default Index
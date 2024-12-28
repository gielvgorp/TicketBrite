import { useNavigate, useParams } from 'react-router-dom';
import styles from '../events.module.css';
import EventItem from '../components/Events/EventItem/EventItem';
import { useEffect, useState } from 'react';
import { Event } from '../Types';

function Events() {
    const { id } = useParams();
    const navigate = useNavigate();

    const handleChangeCategory = (event: React.ChangeEvent<HTMLSelectElement>) => {
        navigate(`/events/${event.target.value}`, { replace: true });
    };

    const [events, setEvent] = useState<Array<Event>>([]);  // State to store the fetched events
    const [loading, setLoading] = useState(true); // State to show loading spinner or message

    useEffect(() => {      
        console.log(id); 
        fetchEvents();
    }, []);

    useEffect(() => {
        console.log(id); 
        fetchEvents();
    }, [id]);

    const fetchEvents = () => {
        console.log(id);
        fetch(`http://localhost:7150/event/get-all-verified/${id === undefined ? "" : id}`)
        .then(response => response.json())
        .then(data => {
            setEvent(data.value);
            setLoading(false);
        })
        .catch(error => {
            console.error('Error fetching data:', error);  
            setLoading(false);
        });
    }

    if (loading) {
        return <p>Loading...</p>;  // Display loading message while fetching data
    }

    return (
        <>
            <header className={`${styles.header} w-100 d-flex align-items-center`}>
                <div className={`${styles.overlay} w-100 h-100 p-5 d-flex align-items-center`}>
                    <h1 className={`${styles.overlayInfo}`}>Alle {id} evenementen</h1>
                </div>
            </header>
            <div className="bg-white w-100 px-5 py-3">
                <select name="category" id="category" className="form-select w-25 px-2" onChange={handleChangeCategory}>
                    <option value="">Alle evenementen</option>
                    <option value="muziek">Muziek</option>
                    <option value="sport">Sport</option>
                    <option value="festival">Festival</option>
                </select>
            </div>
            <div className={`${styles.eventList} p-5`}>
                <h1>Alle {id} evenementen</h1>
                <div className="w-75 m-auto d-flex flex-column p-3 mt-5 bg-white shadow gap-3">
                    <div className="d-flex w-100 justify-content-between">
                        <span className="text-secondary">{events.length} evenementen gevonden</span>
                        <input type="date" className={`${styles.dateSelctor} form-control p-2 w-25`} />
                    </div>
                    {/* Example of mapping over events */}
                    {events.map((event: Event) => <EventItem key={event.eventID} id={event.eventID} />)}
                </div>
            </div>
        </>
    );
}

export default Events;

import { useNavigate, useParams } from 'react-router-dom';
import styles from '../events.module.css';
import EventItem from '../components/Events/EventItem/EventItem';
import events from '../mockdata';

function Events() {
    const { id } = useParams();
    const navigate = useNavigate();

    const handleChangeCategory = (event: React.ChangeEvent<HTMLSelectElement>) => {
        navigate(`/events/${event.target.value}`, { replace: true });
    };

    const eventss = events();

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
                    <option value="fesitval">Festival</option>
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
                    {eventss.map((event: any, index: number) => <EventItem key={event.id} id={event.id} />)}
                </div>
            </div>
        </>
    );
}

export default Events;

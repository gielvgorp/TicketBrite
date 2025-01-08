import { useNavigate } from 'react-router-dom';
import styles from './GridItem.module.css'

type Props = {
    artist: string;
    category: string;
    eventID: string;
    image: string;
}

function GridItem(props: Props){
    const navigate = useNavigate();

    return (
        <div data-test="grid-item" className={styles.gridItem} onClick={() => navigate(`/event/${props.eventID}`, { replace: true, state: { id: props.eventID} })}>
            <div className={styles.banner} style={{backgroundImage: `url(${props.image})`}}>
                <div className={styles.hoverOverlay}>
                    <div className={styles.end}>
                    <i className="fa-solid fa-chevron-right"></i>
                    </div>
                </div>
            </div>
            <p data-test="grid-item-category" className={`${styles.category} text-secondary fw-lighter`}>{props.category}</p>
            <p data-test="grid-item-artist" className={styles.artist}>{props.artist}</p>
        </div>
    );
}

export default GridItem
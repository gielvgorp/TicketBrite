import styles from './GridItem.module.css'

type Props = {
    artist: String;
    category: string;
}

function GridItem(props: Props){
    return (
        <div className={styles.gridItem}>
            <div className={styles.banner}>
                <div className={styles.hoverOverlay}>
                    <div className={styles.end}>
                    <i className="fa-solid fa-chevron-right"></i>
                    </div>
                </div>
            </div>
            <p className={`${styles.category} text-secondary fw-lighter`}>{props.category}</p>
            <p className={styles.artist}>{props.artist}</p>
        </div>
    );
}

export default GridItem
import { Link } from 'react-router-dom'
import styles from './navigationBar.module.css'
import 'bootstrap/dist/css/bootstrap.min.css'

function NavigationBar(){
    return (
        <nav className={`${styles.navBar} px-5`}>
            <Link to="/"><h1 className={styles.title}>TicketBrite</h1></Link>
            <ul className={`${styles.navList} d-flex gap-3`}>
                <li><Link to="/">Home</Link></li>
                <li><Link to="/events">Evenementen</Link></li>
            </ul>
        </nav>
    )
}

export default NavigationBar
import { Link } from 'react-router-dom'
import styles from './navigationBar.module.css'
import 'bootstrap/dist/css/bootstrap.min.css'
import { useState } from 'react'

function NavigationBar(){
    const [jwtToken, setJwtToken] = useState(localStorage.getItem('JwtToken'));

    if(jwtToken !== undefined){

    }

    return (
        <nav className={`${styles.navBar} px-5`}>
            <Link to="/"><h1 className={styles.title}>TicketBrite</h1></Link>
            <ul className={`${styles.navList} d-flex gap-3`}>
                <li><Link to="/">Home</Link></li>
                <li><Link to="/events">Evenementen</Link></li>
                <li className='ms-auto me-5'><Link to="/"><i className="fa-solid fa-cart-shopping"></i></Link></li>
            </ul>
            <div className="w-100 h-100 d-flex justify-content-between align-items-center gap-2">
                <input type="text" className='form-control bg-primary p-2 text-white w-50' placeholder='Zoeken...' />
                <div className={`${styles.signInContainer} h-100 d-flex justify-content-center align-items-center text-align-center px-3`}>
                    { jwtToken === null ? <Link className='text-white h-100 w-100 text-align-center' to="/authenticatie">Inloggen / Registreren</Link> : <Link className='text-white h-100 w-100 text-align-center' to="/profile">Welkom, Giel!</Link> }
                </div>
            </div>
        </nav>
    )
}

export default NavigationBar
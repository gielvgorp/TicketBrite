import { Link } from 'react-router-dom'
import styles from './navigationBar.module.css'
import 'bootstrap/dist/css/bootstrap.min.css'
import useUser from '../../hooks/useUser';
import { useAuth } from '../../AuthContext';
import SearchBar from './searchbar';
function NavigationBar(){
    const { user, loading } = useUser();
    const { isAuthenticated } = useAuth();

    console.log(user);
    console.log("Is auth:", isAuthenticated);

    if (loading) return <p>Loading...</p>;

    return (
        <nav className={`${styles.navBar} px-5`}>
            <Link to="/"><h1 className={styles.title}>TicketBrite</h1></Link>
            <ul className={`${styles.navList} d-flex gap-3`}>
                <li id="nav-item-home"><Link to="/">Home</Link></li>
                <li><Link to="/events">Evenementen</Link></li>
                <li id="nav-item-shopping-cart" className='ms-auto me-5'><Link className='position-relative' to="/shopping-cart"><i className="fa-solid fa-cart-shopping"></i></Link></li>
            </ul>
            <div className="w-100 h-100 d-flex justify-content-between align-items-center gap-2">
                <SearchBar />
                <div className={`${styles.signInContainer} h-100 d-flex justify-content-center align-items-center text-align-center px-3 col-6`}>
                    { !isAuthenticated ? <Link className='text-white h-100 w-100 text-align-center' to="/authenticatie">Inloggen / Registreren</Link> : <Link className='text-white h-100 w-100 text-align-center' to="/profile">Welkom, {user.name}!</Link> }
                </div>
            </div>
        </nav>
    )
}

export default NavigationBar
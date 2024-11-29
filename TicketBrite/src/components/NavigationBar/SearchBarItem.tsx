import { useNavigate } from 'react-router-dom'
import './searchbar.css'

function SearchBarItem(){
    const navigate = useNavigate();
    return (
        <div className="p-2 search-bar-item d-flex align-items-center" onClick={() => navigate('/event/f827d813-e04a-4e84-8d69-72baef15fcd4', {replace: true})}>
            <img src="https://snollebollekeslive.nl/wp-content/uploads/SPARK_20240323_221911_SLIC_VB.jpg" alt="" />
            <h6 className='fw-bold ps-2'>Lorem ipsum dolor sit.</h6>
        </div>
    )
}

export default SearchBarItem
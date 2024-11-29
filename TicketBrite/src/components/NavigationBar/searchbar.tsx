import { useState } from 'react'
import './searchbar.css'
import SearchBarItem from './SearchBarItem'

function SearchBar(){
    const [showResults, setShowResults] = useState(false);

    const handleShowResults = (value: boolean) => {
        setShowResults(value);
    }

    return (
        <div className='position-relative' style={{width: '550px'}}>
            <input onFocus={() => handleShowResults(true)} onBlur={() => handleShowResults(false)} type="text" className='form-control bg-primary p-2 text-white w-100' placeholder='Zoeken...' />
            <div className={`results position-absolute top-100 ${showResults && 'show'}`}>
                <SearchBarItem />
                <SearchBarItem />
            </div>
        </div>
    )
}

export default SearchBar
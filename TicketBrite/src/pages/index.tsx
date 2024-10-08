import { useEffect, useState } from 'react';
import GridItem from '../components/Index/GridItem/GridItem'
import styles from '../index.module.css'

function Index(){
    const [events, setEvents] = useState([]);  // State to store the fetched events
    const [loading, setLoading] = useState(true); // State to show loading spinner or message

    useEffect(() => {
        // Fetch data from the API when the component mounts
        fetch('http://localhost:5285/get-events')  // Replace with your actual API URL
            .then(response => response.json())  // Parse JSON response
            .then(data => {
                setEvents(data.value);  // Set events data to state
                setLoading(false);  // Stop loading spinner
            })
            .catch(error => {
                console.error('Error fetching data:', error);  // Handle any errors
                setLoading(false);  // Stop loading spinner even if there's an error
            });
    }, []);  // Empty dependency array ensures this runs only once, when the component mounts

    if (loading) {
        return <p>Loading...</p>;  // Display loading message while fetching data
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
        <div className={`w-100 p-5 gap-5 ${styles.eventsGrid}`}>
            {events.map((event: any, index: number) => <GridItem image={event.image} id={event.id} key={index} category={event.category} artist={event.artist} />)}
        </div>
       </>
    )
}

export default Index
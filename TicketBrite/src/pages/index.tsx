import GridItem from '../components/Index/GridItem/GridItem'
import styles from '../index.module.css'
import events from '../mockdata'

function Index(){
    const festivals = events();
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
            {festivals.map((event: any, index: number) => <GridItem id={event.id} key={index} category={event.category} artist={event.artist} />)}
        </div>
       </>
    )
}

export default Index
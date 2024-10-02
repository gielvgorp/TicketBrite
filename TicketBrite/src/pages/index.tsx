import GridItem from '../components/Index/GridItem/GridItem'
import styles from '../index.module.css'

function Index(){
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
            <GridItem key="Snelle" category="Muziek" artist="Snelle" />
            <GridItem key="Sport" category="Sport" artist="Voetbal" />
            <GridItem key="Sport" category="Sport" artist="Voetbal" />
        </div>
       </>
    )
}

export default Index
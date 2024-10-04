import { useState } from 'react';
import styles from '../TicketsSelector/TicketSelector.module.css'

type Props = {
    name: string
    maxAmount: number;
}

function TicketSelector(props: Props){
    const [amount, setAmount] = useState(0);
    const maxTickets = props.maxAmount;

    const handleSelectTicket = (value: number) => {
        if((amount != 0 || value > 0) && (amount >= maxTickets && value > 0)) return; 
            
            setAmount(amount + value);
    }

    return (
        <div className={`${styles.ticketSelector} px-3`}>
            <p>{props.name}</p>
            <div className={styles.selectTicket}>
                <button onClick={() => handleSelectTicket(-1)} disabled={amount === 0}><i className="fa-solid fa-minus"></i></button>
                <span>{amount}</span>
                <button onClick={() => handleSelectTicket(1)} disabled={amount === maxTickets}><i className="fa-solid fa-plus"></i></button>
            </div>
        </div>
    );
}

export default TicketSelector
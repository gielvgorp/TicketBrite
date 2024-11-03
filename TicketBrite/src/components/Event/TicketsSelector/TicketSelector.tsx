import { useState } from 'react';
import styles from '../TicketsSelector/TicketSelector.module.css'
import { Ticket } from '../../../Types';

type Props = {
    name: String;
    maxAmount: number;
    onTicketSelect: (value: number) => void;
    ticketPrice: String;
    ticket: Ticket
}

function TicketSelector(props: Props){
    const [amount, setAmount] = useState(0);
    const maxTickets = props.maxAmount;

    const handleSelectTicket = (value: number) => {
        if((amount != 0 || value > 0) && (amount >= maxTickets && value > 0)) return; 
            
            setAmount(amount + value);
            props.onTicketSelect(amount + value);
    }

    return (
        <div className={`${styles.ticketSelector} px-3`}>
            <div className='d-flex flex-column'>
                <p>{props.name}</p>
                <span className={styles.ticketPrice}>Prijs per ticket: {props.ticketPrice},-</span>
            </div>
            <div className={styles.selectTicket}>
                {
                    props.ticket.ticketsRemaining > 0 && 
                    <>
                        <button onClick={() => handleSelectTicket(-1)} disabled={amount === 0}><i className="fa-solid fa-minus"></i></button>
                        <span>{amount}</span>
                        <button onClick={() => handleSelectTicket(1)} disabled={amount === maxTickets}><i className="fa-solid fa-plus"></i></button>
                    </>
                }
            </div>
        </div>
    );
}

export default TicketSelector
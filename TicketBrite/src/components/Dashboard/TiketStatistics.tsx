import React, { useEffect, useState } from 'react';
import { getTicketStats } from '../../hooks/useTIcket';

interface TicketStats {
    name: string;
    sold: number;
    reserved: number;
}

interface TicketStatisticsProps {
    eventId: string;
}

const TicketStatistics: React.FC<TicketStatisticsProps> = ({ eventId }) => {
    const [stats, setStats] = useState<TicketStats[]>([]);

    useEffect(() => {
        const fetchStats = async () => {
            const data = await getTicketStats(eventId);
            setStats(data);
        };
        fetchStats();
        console.log(stats);
    }, [eventId]);

    return (
        <div className="card p-4">
            <h5>Ticketstatistieken</h5>
            {stats && stats.map((ticket, index) => (
                <div key={index} className="d-flex justify-content-between mb-2">
                    <span>{ticket.name}</span>
                    <span>Verkocht: {ticket.sold}</span>
                    <span>Gereserveerd: {ticket.reserved}</span>
                </div>
            ))}
        </div>
    );
};

export default TicketStatistics;

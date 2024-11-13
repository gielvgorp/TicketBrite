import axios from 'axios';
import { Event } from '../Types';

export const getEventDetails = async (eventId: string) => {
    try {
        const response = await axios.get(`https://localhost:7150/get-event/${eventId}`);
        console.log(response.data);
        return response.data; // Retourneer de evenementgegevens
    } catch (error) {
        console.error("Error fetching event details:", error);
        throw error;
    }
};

export const updateEventDetails = async (eventId: string, updatedEventDetails: Event) => {
    try {
        const response = await axios.put(`http://localhost:7150/event/update/${eventId}`, updatedEventDetails);
        return response.data; // Retourneer eventueel bijgewerkte gegevens of status
    } catch (error) {
        console.error("Error updating event details:", error);
        throw error;
    }
};

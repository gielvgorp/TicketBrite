import axios from 'axios';
import { ApiResponse, Event } from '../Types';
import { ErrorNotification, SuccessNotification } from '../components/Notifications/Notifications';

export const getEventDetails = async (eventId: string) => {
    try {
        const response = await axios.get(`http://localhost:7150/get-event/${eventId}`);
        console.log(response.data);
        return response.data; // Retourneer de evenementgegevens
    } catch (error) {
        console.error("Error fetching event details:", error);
        throw error;
    }
};

export const updateEventDetails = async (eventId: string, updatedEventDetails: Event) => {
    try {
        await axios.put(`http://localhost:7150/event/update/${eventId}`, updatedEventDetails);
        SuccessNotification({text: "Evenement informatie gewijzigd!"});
    } catch (error) {
        ErrorNotification({text: "Kan event details niet opslaan!"});
        console.error("Error updating event details:", error);
        throw error;
    }
};

import axios from "axios";

export const getTicketOfEvent = async (eventID: string) => {
    try {
        const response = await axios.get(`http://localhost:7150/event/${eventID}/get-tickets`);
        //console.log(response.data);
        return response.data;
    } catch (error) {
        console.error("Error fetching ticket stats:", error);
    }
};
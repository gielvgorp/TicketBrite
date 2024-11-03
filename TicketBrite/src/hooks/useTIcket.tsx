import React, { useState, useEffect } from "react";
import axios from "axios";

export const getTicketStats = async (eventId: string) => {
    try {
        const response = await axios.get(`/tickets/stats/${eventId}`);
        return response.data;
    } catch (error) {
        console.error("Error fetching ticket stats:", error);
    }
};
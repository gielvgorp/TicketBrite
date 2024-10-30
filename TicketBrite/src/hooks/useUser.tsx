import { useState, useEffect } from 'react';

const useUser = () => {
    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchUserData = async () => {
            const token = localStorage.getItem('jwtToken');

            if (!token) {
                setLoading(false);
                return;
            }

            try {
                const response = await fetch('https://localhost:7150/get-user', {
                    method: 'GET',
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        'Content-Type': 'application/json'
                    }
                });

                if (!response.ok) {
                    throw new Error('Fout bij het ophalen van gebruikersgegevens');
                }

                const data = await response.json();
                setUser(data);
            } catch (error) {
                console.error('Er is een fout opgetreden:', error);
            } finally {
                setLoading(false);
            }
        };

        fetchUserData();
    }, []);

    return { user, loading };
};

export default useUser;
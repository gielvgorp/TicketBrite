import { jwtDecode } from 'jwt-decode';
import { useState, useEffect } from 'react';

const useUser = () => {
    const [user, setUser] = useState<any>(null);
    const [loading, setLoading] = useState(true);

    const fetchUserData = async () => {
        const token = localStorage.getItem('jwtToken');

        if (!token) {
            setLoading(false);
            return;
        }

        try {
            const response = await fetch('https://localhost:7150/api/User/get-user', {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                }
            });

            const data = await response.json(); // Ontvang de JSON-response

            console.log("Guest auth reponse: ",data);  
            
            if(data.statusCode !== 200){
                console.log(response);
                throw new Error('Fout bij het ophalen van gebruikersgegevens');
            }

            // successful registered
            if(data.statusCode === 200){
                const decodedToken = jwtDecode(token); // Decodeer de token
                console.log("Decoded Token:", decodedToken); // Controleer de inhoud van de gedecodeerde token
                setUser(data.value);
            }           

          
        } catch (error) {
            console.error('Er is een fout opgetreden:', error);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchUserData();
    }, []);

    return { user, loading };
};

export default useUser;
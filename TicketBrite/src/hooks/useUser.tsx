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

        const decodedToken = jwtDecode(token.toString());
        setUser(decodedToken);

        setLoading(false);
    };

    useEffect(() => {
        fetchUserData();
    }, []);

    return { user, loading };
};

export default useUser;
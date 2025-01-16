import { Outlet } from 'react-router-dom';
import { useAuth } from '../AuthContext';
import { useEffect, useState } from 'react';

function ProtectedRoute() {
    const { isAuthenticated } = useAuth();
    const [authenticated, setIsAuthenticated] = useState(false);

    useEffect(() => {
        setIsAuthenticated(isAuthenticated);
    }, [isAuthenticated])

    if(authenticated){    
        console.log("outlet");
        return  <Outlet />
    }

    return <><h1>Toegang geweigerd!</h1></>
}

export default ProtectedRoute;
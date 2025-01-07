import { ReactNode, useEffect, useState } from 'react';
import { ApiResponse, User } from '../Types';

interface ProtectedRouteProps {
    roleRequired: string;
    children: ReactNode;
}

const getUserRole = async (): Promise<string | null> => {
    try {
        const token = localStorage.getItem("jwtToken");

        const response = await fetch('http://localhost:7150/api/User/get-user', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        });

        if (!response.ok) {
            throw new Error('Fout bij het ophalen van gebruikersgegevens');
        }

        const data = await response.json() as ApiResponse<User>;

        return data.value!.roleName as string;
    } catch (error) {
        console.error('Er is een fout opgetreden:', error);
        return null; // Retourneer null bij een fout
    }
};

const ProtectedRoute = ({ roleRequired, children }: ProtectedRouteProps) => {
    const [userRole, setUserRole] = useState<string | null>(null);

    useEffect(() => {
        const fetchUserRole = async () => {
            const role = await getUserRole();
            setUserRole(role);
        };
    
        void fetchUserRole();
    
    }, []); 
    
    

    if (userRole !== null && userRole === roleRequired) {
        return <>{children}</>;
    }

    return;
};

export default ProtectedRoute;

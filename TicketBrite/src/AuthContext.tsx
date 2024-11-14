import { jwtDecode } from 'jwt-decode';
import React, { createContext, useContext, useState, ReactNode } from 'react';
import useUser from './hooks/useUser';

// AuthContext.tsx
interface AuthContextType {
    isAuthenticated: boolean;
    role: string | null; // Rol type toevoegen
    login: (token: string) => void;
    logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);
    const [role, setRole] = useState<string | null>(null); // Rol state

    const login = (token: string) => {
        localStorage.setItem('jwtToken', token);
        setIsAuthenticated(true);
        const decodedToken: any = jwtDecode(token); // Decode de token
        console.log("User role:", decodedToken.role);
        setRole(decodedToken.role); // Rol instellen
    };

    const logout = () => {
        localStorage.removeItem('jwtToken');
        setIsAuthenticated(false);
        setRole(null); // Rol resetten
    };

    return (
        <AuthContext.Provider value={{ isAuthenticated, role, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => {
    const context = useContext(AuthContext);
    if (context === undefined) {
        throw new Error('useAuth must be used within an AuthProvider');
    }
    return context;
};

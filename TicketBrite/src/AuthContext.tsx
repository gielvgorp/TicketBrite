import { jwtDecode } from 'jwt-decode';
import { createContext, useContext, useState, useEffect, ReactNode } from 'react';
import { SuccessNotification } from './components/Notifications/Notifications';
import { useNavigate } from 'react-router-dom';

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
    const navigate = useNavigate();

    // Gebruik useEffect om bij te werken bij het laden van de applicatie of bij een refresh
    useEffect(() => {
        const storedToken = localStorage.getItem('jwtToken');
        if (storedToken) {
            login(storedToken);
        }
    }, []); // Lege array zorgt ervoor dat de effect alleen bij de eerste render wordt uitgevoerd

    const login = (token: string) => {
        localStorage.setItem('jwtToken', token);  // Sla het token op in localStorage
        setIsAuthenticated(true);
        const decodedToken: any = jwtDecode(token); // Decode de token
        setRole(decodedToken.role);  // Stel de rol in
    };

    const logout = () => {
        localStorage.removeItem('jwtToken');  // Verwijder het token uit localStorage
        setIsAuthenticated(false);  // Stel isAuthenticated in op false
        setRole(null);  // Reset de rol
        SuccessNotification({text: "Je bent nu uitgelogd!"});
        navigate("/");
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

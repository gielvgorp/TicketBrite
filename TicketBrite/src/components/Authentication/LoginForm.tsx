import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useAuth } from "../../AuthContext";

function LoginForm(){
    const {login} = useAuth();

    const navigate = useNavigate();

    const [errorMsg, setErrorMsg] = useState("");

    const [formData, setFormData] = useState({
        UserEmail: '',
        Password: ''
    });
    
    const handleChange = (e: any) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleSubmit = (e: React.FormEvent) => {
        
        e.preventDefault(); // Voorkom de standaard formulierverzending
        const saveData = async () => {
            try {
                // Verzend het formulier naar het endpoint
                const res = await fetch('http://localhost:7150/api/auth/login', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(formData) // Zet de formData om naar JSON
                });
        
                const data = await res.json(); // Ontvang de JSON-response
        
                // Validation error
                if (data.statusCode !== 200) {
                    console.log("Auth response: ", data);
                    setErrorMsg(data.value);
                }
        
                // Successful login
                if (data.statusCode === 200) {
                    login(data.value.token);
                    navigate("/", { replace: true });
                }
            } catch (error) {
                console.error('Er is een fout opgetreden:', error);
            }
        }
        saveData();
    };    

    return (
        <>
            <h1 className="font">Inloggen</h1>
            <p className="text-secondary">
                Nieuw bij TicketBrite? <Link to="register">Maak hier gratis een account aan!</Link>
            </p>
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label htmlFor="email-input">Email address</label>
                    <input onChange={handleChange} type="email" className="form-control p-2" id="email-input" name="UserEmail" aria-describedby="email-input" placeholder="John@doe.com" />
                </div>
                <div className="form-group pt-3">
                    <label htmlFor="password-input">Password</label>
                    <input onChange={handleChange} type="password" className="form-control p-2" id="password-input" name="Password" placeholder="**********" />
                </div>
                <p data-test="validation-message" className="text-danger">{errorMsg}</p>
                <button type="submit" className="btn btn-primary d-block ms-auto mt-3 w-25 py-2">Inloggen</button>
            </form>
        </>
    )
}

export default LoginForm
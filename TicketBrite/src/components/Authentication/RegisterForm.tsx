import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useAuth } from "../../AuthContext";

function RegisterForm(){
    const {login} = useAuth();
    const navigate = useNavigate();
    const [errorMsg, setErrorMsg] = useState("");

    const [formData, setFormData] = useState({
        fullName: '',
        email: '',
        password: ''
    });

    const handleChange = (e: any) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleSubmit = (e: React.FormEvent) => {
        const saveData = async () => {
            e.preventDefault(); // Voorkom de standaard formulierverzending

            try {
                // Verzend het formulier naar het endpoint
                const res = await fetch('http://localhost:7150/api/auth/Register', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(formData) // Zet de formData om naar JSON
                });
    
                const data = await res.json(); // Ontvang de JSON-response
    
                // validation error
                if(data.statusCode !== 200){
                    console.log(data);
                    setErrorMsg(data.value);
                }
    
                // successful registered
                if(data.statusCode === 200){
                    console.log("Result: ", data);
                    login(data.value.token);
                    navigate("/", {replace: true});
                }           
            } catch (error) {
                console.error('Er is een fout opgetreden:', error);
            }
        }
       
        saveData();
    };

    return (
        <>
            <h1 className="font">Account aanmaken</h1>
            <p className="text-secondary">
                Heb je al een TicketBrite account? <Link to="/authenticatie">Log hier in!</Link>
            </p>
            <form onSubmit={handleSubmit}>
                <div className="form-group gap-2 w-100 pt-3">
                    <label htmlFor="full-name-input">Volledige naam</label>
                    <input type="text" className="form-control p-2" id="full-name-input" name="FullName" onChange={handleChange} aria-describedby="full-name-input" placeholder="John Doe" />
                </div>
                <div className="form-group pt-3">
                    <label htmlFor="email-input">Email address</label>
                    <input type="email" className="form-control p-2" id="email-input" name="email" onChange={handleChange} aria-describedby="email-input" placeholder="John@doe.com" />
                </div>
                <div className="form-group pt-3">
                    <label htmlFor="password-input">Password</label>
                    <input type="password" className="form-control p-2" id="password-input" name="password" onChange={handleChange} placeholder="**********" />
                </div>
                <p data-test="validation-message" className="text-danger">{errorMsg}</p>
                <button type="submit" className="btn btn-success d-block ms-auto mt-3 w-25 py-2">Accont aanmaken</button>
            </form>
        </>
    )
}

export default RegisterForm
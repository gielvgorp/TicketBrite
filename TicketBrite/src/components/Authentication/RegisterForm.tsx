import { useState } from "react";
import { Link, replace, useNavigate } from "react-router-dom";

type Props = {
    msg: string;
}

function RegisterForm({msg}: Props){

    const navigate = useNavigate();

    const [errorMsg, setErrorMsg] = useState(msg);

    const [formData, setFormData] = useState({
        fullName: '',
        email: '',
        password: ''
    });
    const [response, setResponse] = useState(null);

    const handleChange = (e: any) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleSubmit = async (e: any) => {
        e.preventDefault(); // Voorkom de standaard formulierverzending

        try {
            // Verzend het formulier naar het endpoint
            const res = await fetch('https://localhost:7150/api/auth/Register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(formData) // Zet de formData om naar JSON
            });

            const data = await res.json(); // Ontvang de JSON-response

            // validation error
            if(res.status === 400){
                setErrorMsg("Een of meerdere velden zijn leeg!");
            }

            // successful registered
            if(res.status === 200){
                localStorage.setItem('jwtToken', data.token);
                console.log("Token:", localStorage.getItem('jwtToken'));
                navigate("/", {replace: true});
            }           
        } catch (error) {
            console.error('Er is een fout opgetreden:', error);
        }
    };

    return (
        <>
            <h1 className="font">Account aanmaken</h1>
            <p className="text-secondary">
                Heb je al een TicketBrite account? <Link to="/authenticatie">Log hier in!</Link>
            </p>
            <form onSubmit={handleSubmit}>
                <div className="form-group gap-2 w-100 pt-3">
                    <label htmlFor="exampleInputEmail1">Volledige naam</label>
                    <input type="text" className="form-control p-2" id="full-name-input" name="fullName" onChange={handleChange} aria-describedby="FullName" placeholder="John Doe" />
                </div>
                <div className="form-group pt-3">
                    <label htmlFor="exampleInputEmail1">Email address</label>
                    <input type="email" className="form-control p-2" id="exampleInputEmail1" name="email" onChange={handleChange} aria-describedby="emailHelp" placeholder="John@doe.com" />
                </div>
                <div className="form-group pt-3">
                    <label htmlFor="exampleInputPassword1">Password</label>
                    <input type="password" className="form-control p-2" id="exampleInputPassword1" name="password" onChange={handleChange} placeholder="**********" />
                </div>
                <p className="text-danger">{errorMsg}</p>
                <button type="submit" className="btn btn-success d-block ms-auto mt-3 w-25 py-2">Accont aanmaken</button>
            </form>
        </>
    )
}

export default RegisterForm
import { useState } from "react";
import { Link } from "react-router-dom";

type Props = {
    msg: string;
}

function LoginForm({msg}: Props){

    const [errorMsg, setErrorMsg] = useState(msg);

    return (
        <>
            <h1 className="font">Inloggen</h1>
            <p className="text-secondary">
                Nieuw bij TicketBrite? <Link to="register">Maak hier gratis een account aan!</Link>
            </p>
            <form>
                <div className="form-group">
                    <label htmlFor="exampleInputEmail1">Email address</label>
                    <input type="email" className="form-control p-2" id="exampleInputEmail1" aria-describedby="emailHelp" placeholder="John@doe.com" />
                </div>
                <div className="form-group pt-3">
                    <label htmlFor="exampleInputPassword1">Password</label>
                    <input type="password" className="form-control p-2" id="exampleInputPassword1" placeholder="**********" />
                </div>
                <p className="text-danger">{errorMsg}</p>
                <button type="submit" className="btn btn-primary d-block ms-auto mt-3 w-25 py-2">Inloggen</button>
            </form>
        </>
    )
}

export default LoginForm
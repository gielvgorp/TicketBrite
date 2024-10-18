import { Link } from "react-router-dom";

function RegisterForm(){
    return (
        <>
            <h1 className="font">Account aanmaken</h1>
            <p className="text-secondary">
                Heb je al een TicketBrite account? <Link to="/authenticatie">Log hier in!</Link>
            </p>
            <form>
                <div className="form-group gap-2 w-100 pt-3">
                    <label htmlFor="exampleInputEmail1">Volledige naam</label>
                    <input type="text" className="form-control p-2" id="full-name-input" aria-describedby="FullName" placeholder="John Doe" />
                </div>
                <div className="form-group pt-3">
                    <label htmlFor="exampleInputEmail1">Email address</label>
                    <input type="email" className="form-control p-2" id="exampleInputEmail1" aria-describedby="emailHelp" placeholder="John@doe.com" />
                </div>
                <div className="form-group pt-3">
                    <label htmlFor="exampleInputPassword1">Password</label>
                    <input type="password" className="form-control p-2" id="exampleInputPassword1" placeholder="**********" />
                </div>
                <button type="submit" className="btn btn-success d-block ms-auto mt-3 w-25 py-2">Accont aanmaken</button>
            </form>
        </>
    )
}

export default RegisterForm
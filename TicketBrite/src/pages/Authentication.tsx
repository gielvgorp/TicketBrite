import { useEffect, useState } from "react";
import 'bootstrap/dist/css/bootstrap.min.css'
import { Link, useParams } from "react-router-dom";
import '../Authentication.css'
import LoginForm from "../components/Authentication/LoginForm";
import RegisterForm from "../components/Authentication/RegisterForm";

type Props = {
    setShowNav: (value: boolean) => void;
}

function Authentication({setShowNav}: Props){
    const { id } = useParams();
    
    useEffect(() => {
        setShowNav(false);
    
        // Show the navbar again when the component is unmounted
        return () => setShowNav(true);
      }, [setShowNav]);
    return (
        <div className="d-flex justify-content-center align-items-center w-100 h-100 p-5 bg">
            <div className="position-absolute top-0 start-0 m-3"><Link className="text-decoration-none" to="/"><i className="fa-solid fa-chevron-left"></i> Terug naar TicketBrite</Link></div>

            <div className="bg-white shadow w-75 h-100 d-flex">
                <div className="col-4 banner">
                    <h1 className="font text-info p-4">TicketBrite</h1>
                    <p className="px-4 text-white font-italic">
                        Meld je aan en zie al jou aankopen in een oog opslag!
                    </p>
                </div>
                <div className="col-8 p-5">
                {
                    // If id is "register", show RegisterForm, otherwise show LoginForm
                    id === "register" ? <RegisterForm /> : <LoginForm />
                }
                </div>
            </div>
        </div>
    )
}

export default Authentication;
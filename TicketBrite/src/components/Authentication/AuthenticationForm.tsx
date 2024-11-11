import { useState } from "react"
import LoginForm from "./LoginForm";
import RegisterForm from "./RegisterForm";

function AuthenticationForm(){
    const [showLogin, setShowLogin] = useState(true);
    return (
       <>
        {
            showLogin ? <LoginForm /> : <RegisterForm />
        }
       </>
    )
}  

export default AuthenticationForm
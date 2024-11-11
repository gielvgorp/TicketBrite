import { useState } from "react"
import LoginForm from "./LoginForm";
import RegisterForm from "./RegisterForm";

function AuthenticationForm(){
    const [showLogin] = useState(true);
    return (
       <>
        {
            showLogin ? <LoginForm /> : <RegisterForm />
        }
       </>
    )
}  

export default AuthenticationForm
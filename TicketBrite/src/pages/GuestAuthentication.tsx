import { useNavigate, useParams } from "react-router-dom";
import { useAuth } from "../AuthContext";
import { useEffect } from "react";

function GuestAuthentication(){
    const {login} = useAuth();
    const { guestID } = useParams();
    const { verificationCode } = useParams();
    const navigate = useNavigate();

    console.log(guestID);
    console.log(verificationCode);

    if(guestID === null || verificationCode === null) return <h1 className="text-danger p-5">De verificatie link is ongeldig!</h1>

    useEffect(() => {
        verifyGuest();
    }, []);

    const verifyGuest = async () => {
        try {
            // Verzend het formulier naar het endpoint
            const res = await fetch(`https://localhost:7150/auth/guest/${guestID}/${verificationCode}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    guestID: guestID,
                    verificationCode: verificationCode
                }) // Zet de formData om naar JSON
            });

            const data = await res.json(); // Ontvang de JSON-response

            console.log(data);

            // validation error
            if(res.status === 404){
                return <h1 className="text-danger p-5">De verificatie link is ongeldig! {data.value}</h1>
            }

            // successful registered
            if(res.status === 200){
                login(data.value.token);
                navigate("/");
            }           
        } catch (error) {
            console.error('Er is een fout opgetreden:', error);
        }
    }


    return (
        <></>
    )
}

export default GuestAuthentication
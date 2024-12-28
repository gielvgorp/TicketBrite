import { useNavigate, useParams } from "react-router-dom";
import { useAuth } from "../AuthContext";
import { useEffect } from "react";

type Props = {
    setShowNav: (value: boolean) => void;
}

function GuestAuthentication({setShowNav}: Props){
    const {login} = useAuth();
    const { guestID } = useParams();
    const { verificationCode } = useParams();
    const navigate = useNavigate();

    if(guestID === null || verificationCode === null) return <h1 className="text-danger p-5">De verificatie link is ongeldig!</h1>

    useEffect(() => {
        setShowNav(false);
        verifyGuest();
    }, []);

    const verifyGuest = async () => {
        try {
            // Verzend het formulier naar het endpoint
            const res = await fetch(`http://localhost:7150/auth/guest/${guestID}/${verificationCode}`, {
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

            console.log("Guest auth reponse: ",data);  
            
            if(data.statusCode !== 200){
                console.log("error while loggin in as guest:", data);
            }

            // successful registered
            if(data.statusCode === 200){
                console.log("Guest is correct!")
                login(data.value.token);
                navigate("/", { replace: true});
                setShowNav(true);
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
import 'bootstrap/dist/css/bootstrap.css';
import { useEffect, useState } from "react";

function Prototype(){
 
    const [inputValue, setInputValue] = useState("");
    const [selectedValue, setSelectedValue] = useState("1");
    const [jwt, setJWT] = useState("");
    const [authCheck, setAuthCheck] = useState("");
    const [requestRole, setRequestRole] = useState("");
    const [errormsg, seterrormsg] = useState("");

    useEffect(() => {
       roleCheck();
    }, [requestRole]);

    const roleCheck = async () => {
        try {
            const token = localStorage.getItem("jwtToken");
        
            const response = await fetch(`http://localhost:7150/api/Prototype/authorize-check/${requestRole}`, {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                }
            });
        
            // De string ophalen met .text()
            const responseData = await response.text();
            console.log(responseData);
            seterrormsg(responseData);
            
          } catch (error) {
              console.error('Er is een fout opgetreden:', error);
          }
    }
  
    const handleOnChange = (e: React.FormEvent<HTMLInputElement>) => {
      setInputValue(e.currentTarget.value);
    }
    
    const handleOnSelectChange = (e: React.FormEvent<HTMLSelectElement>) => {
        setSelectedValue(e.currentTarget.value);
    }

    const handleSubmit = async () => {
      try {
        const response = await fetch('http://localhost:7150/api/Prototype/auth', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(selectedValue)
        });
    
        if (!response.ok) {
            throw new Error('Fout bij het ophalen van gebruikersgegevens');
        }
    
        // De string ophalen met .text()
        const responseData = await response.text();
        console.log(responseData);
        
        setJWT(responseData);
        localStorage.setItem("jwtToken", responseData);
        
    } catch (error) {
        console.error('Er is een fout opgetreden:', error);
    }
    
    }
  
    const handleAuthCheck = async () => {
      try {
        const token = localStorage.getItem("jwtToken");
    
        const response = await fetch('http://localhost:7150/api/Prototype/auth-check', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        });
    

        // De string ophalen met .text()
        const responseData = await response.text();
        setAuthCheck(responseData);
        
      } catch (error) {
          console.error('Er is een fout opgetreden:', error);
      }
    }
  
    return (
      <main className='p-5 w-100'>
        <div className="form-grouop">
          <label htmlFor="username">Gebruikersnaam</label>
          <input type="text" onChange={handleOnChange} name='username' className='form-control w-25' />
        </div>
        <div className="form-grouop">
          <label htmlFor="username">Rol</label>
          <select className='form-control w-25' onSelect={handleOnSelectChange}>
            <option value="1">Klant</option>
            <option value="2">Organisatie</option>
            <option value="3">Beheerder</option>
          </select>
        </div>
        <button className='btn btn-primary mt-3' onClick={handleSubmit}>Inloggen</button>
  
        {
          jwt && <>
            <p className='pt-5'><strong>Gegenereerde token: </strong>{jwt}</p>
  
            <button onClick={handleAuthCheck} className='btn btn-success'>Autorisatie check</button>
  
            {
              authCheck && <div className='d-flex flex-column'>
                <button className="btn btn-primary" onClick={() => setRequestRole("1")}>Klant</button>
                <button className="btn btn-success" onClick={() => setRequestRole("2")}>Organisatie</button>
                <button className="btn btn-danger" onClick={() => setRequestRole("3")}>Beheerder</button>
                <p>{errormsg}</p>
              </div>
            }
          </>
        }
       
      </main>
    )
}

export default Prototype
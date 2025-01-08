import { useState } from 'react';
import NotLoggedInModal from './NotLoggedInModal';

type Props = {
    showModal: boolean;
    setShowModal: (value: boolean) => void;
}

function TicketPurchaseComponent({setShowModal}: Props){
    const [, setShowNotLoggedInModal] = useState(true);

    const handleLogin = () => {
      // Hier implementeer je de logica voor inloggen, bijvoorbeeld een redirect naar een loginpagina
      console.log("Doorverwijzen naar login...");
      setShowNotLoggedInModal(false); // Sluit de modal na doorverwijzing
    };
  
    const handleGuestContinue = (email: string, fullName: string) => {
      // Sla gastgegevens op of stel de sessie in
      console.log("Gastgegevens:", email, fullName);
      setShowNotLoggedInModal(false); // Sluit de modal na voortzetten als gast
      // Mogelijke verdere logica om gast te herkennen in de app
    };
  
    return (
      <div>        
        <NotLoggedInModal
          onHide={() => setShowModal(false)}
          onGuestContinue={handleGuestContinue}
          onLogin={handleLogin}
        />
      </div>
    );
}

export default TicketPurchaseComponent;

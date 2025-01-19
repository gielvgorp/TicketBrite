import { useState } from 'react';
import { Event, ApiResponse } from '../../Types';
import { ErrorNotification, SuccessNotification } from '../Notifications/Notifications';

interface EventDetailsFormProps {
    eventDetails: Event;
}

function EventDetailsForm({ eventDetails }: EventDetailsFormProps){
    console.log("Event details", eventDetails);
    const [formValues, setFormValues] = useState<Event>(eventDetails);

    const handleInputChange = <K extends keyof Event>(property: K, value: string | number | boolean) => {    
        setFormValues(formValues => ({
            ...formValues, 
            [property]: value,
        }));
    };

    const handleSave = () => {
        try {
            const token = localStorage.getItem("jwtToken");
            
            fetch('http://localhost:7150/api/event', {
                method: 'PUT',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(formValues)
            })
            .then(response => response.json()) // Verwerk de response als JSON
            .then(data => {
                if (data.statusCode !== 200) {
                    ErrorNotification({ text: "Gegevens kunnen niet worden opgeslagen!" });
                }
    
                if (data.statusCode === 200) {
                    SuccessNotification({ text: 'Gegevens opgeslagen!' });
                }   
            })
            .catch(error => {
                ErrorNotification({text: "Gegevens kunnen niet worden opgeslagen!"});
            });
        } catch (error) {
            ErrorNotification({text: "Gegevens kunnen niet worden opgeslagen!"});
        }
    };

    return (
        <div className="card p-4 mb-3">
            <h5>Evenement Details</h5>
            <div className="form-group mb-2">
                <label>Naam Evenement</label>
                <input id='event-name-input' type="text" name="eventName" value={formValues.eventName} onChange={(e) => handleInputChange('eventName', e.target.value)} className="form-control" />
            </div>
            <div className="form-group mb-2">
                <label>Datum en Tijd</label>
                <input type="datetime-local" name="eventDateTime" value={formValues.eventDateTime} onChange={(e) => handleInputChange('eventDateTime', e.target.value)} className="form-control" />
            </div>
            <div className="form-group mb-2">
                <label>Locatie</label>
                <input type="text" name="eventLocation" value={formValues.eventLocation} onChange={(e) => handleInputChange('eventLocation', e.target.value)} className="form-control" />
            </div>
            <div className="form-group mb-2">
                <label>Afbeelding URL</label>
                <input type="text" name="eventImage" value={formValues.eventImage} onChange={(e) => handleInputChange('eventImage', e.target.value)} className="form-control" />
            </div>
            <div className="form-group mb-2">
                <label>Minimum Leeftijd</label>
                <input type="number" name="eventAge" value={formValues.eventAge} onChange={(e) => handleInputChange('eventAge', e.target.value)} className="form-control" />
            </div>
            <div className="form-group mb-2">
                <label>Categorie</label>
                <input type="text" name="eventCategory" value={formValues.eventCategory} onChange={(e) => handleInputChange('eventCategory', e.target.value)} className="form-control" />
            </div>
            <div className="form-group mb-2">
                <label>Beschrijving</label>
                <textarea name="eventDescription" value={formValues.eventDescription} onChange={(e) => handleInputChange('eventDescription', e.target.value)} className="form-control" />
            </div>
            <button id="btn-save-event-info" onClick={handleSave} className="btn btn-primary mt-2">Opslaan</button>
        </div>
    );
}
export default EventDetailsForm;

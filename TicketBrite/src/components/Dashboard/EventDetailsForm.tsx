import React, { useState } from 'react';
import { Event } from '../../Types';

interface EventDetailsFormProps {
    eventDetails: Event;
    onSave: (updatedDetails: Event) => void;
}

function EventDetailsForm({ eventDetails, onSave }: EventDetailsFormProps){
    console.log("Event details", eventDetails);
    const [formValues, setFormValues] = useState<Event>(eventDetails);

    const handleInputChange = <K extends keyof Event>(property: K, value: string | number | boolean) => {    
        setFormValues(formValues => ({
            ...formValues, 
            [property]: value,
        }));
    };

    const handleSave = async () => {
        try {
            // Verzend het formulier naar het endpoint
            const res = await fetch('https://localhost:7150/dashboard/event/save', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(
                    formValues
                ) // Zet de formData om naar JSON
            });
            
            const data = await res.json(); // Ontvang de JSON-response
           
            // validation error
            if(data.statusCode === 400){
                console.log(data);
                //setErrorMsg(data.value);
            }

            // successful registered
            if(data.statusCode === 200){
               
            }           
        } catch (error) {
            console.error('Er is een fout opgetreden:', error);
        }
    };

    return (
        <div className="card p-4 mb-3">
            <h5>Evenement Details</h5>
            <div className="form-group mb-2">
                <label>Naam Evenement</label>
                <input type="text" name="eventName" value={formValues.eventName} onChange={(e) => handleInputChange('eventName', e.target.value)} className="form-control" />
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
            <button onClick={handleSave} className="btn btn-primary mt-2">Opslaan</button>
        </div>
    );
}
export default EventDetailsForm;

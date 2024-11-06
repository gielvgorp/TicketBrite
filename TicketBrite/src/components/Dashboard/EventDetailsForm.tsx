import React, { useState } from 'react';
import { Event } from '../../Types';

interface EventDetailsFormProps {
    eventDetails: Event;
    onSave: (updatedDetails: Event) => void;
}

const EventDetailsForm: React.FC<EventDetailsFormProps> = ({ eventDetails, onSave }) => {
    const [formValues, setFormValues] = useState(eventDetails);

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = e.target;
        setFormValues({ ...formValues, [name]: value });
    };

    const handleSave = () => {
        onSave(formValues);
    };

    return (
        <div className="card p-4 mb-3">
            <h5>Evenement Details</h5>
            <div className="form-group mb-2">
                <label>Naam Evenement</label>
                <input type="text" name="eventName" value={formValues.eventName} onChange={handleInputChange} className="form-control" />
            </div>
            <div className="form-group mb-2">
                <label>Datum en Tijd</label>
                <input type="datetime-local" name="eventDateTime" value={formValues.eventDateTime} onChange={handleInputChange} className="form-control" />
            </div>
            <div className="form-group mb-2">
                <label>Locatie</label>
                <input type="text" name="eventLocation" value={formValues.eventLocation} onChange={handleInputChange} className="form-control" />
            </div>
            <div className="form-group mb-2">
                <label>Minimum Leeftijd</label>
                <input type="number" name="eventAge" value={formValues.eventAge} onChange={handleInputChange} className="form-control" />
            </div>
            <div className="form-group mb-2">
                <label>Categorie</label>
                <input type="text" name="eventCategory" value={formValues.eventCategory} onChange={handleInputChange} className="form-control" />
            </div>
            <div className="form-group mb-2">
                <label>Beschrijving</label>
                <textarea name="eventDescription" value={formValues.eventDescription} onChange={handleInputChange} className="form-control" />
            </div>
            <button onClick={handleSave} className="btn btn-primary mt-2">Opslaan</button>
        </div>
    );
};

export default EventDetailsForm;

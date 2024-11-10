import { useState } from 'react';

const ProfileContent = () => {
    // State voor profielgegevens
    const [profile, setProfile] = useState({
        email: '',
        fullName: '',
        password: ''
    });

    // Functie om input te verwerken
    const handleChange = (e: any) => {
        const { name, value } = e.target;
        setProfile((prevProfile) => ({
            ...prevProfile,
            [name]: value
        }));
    };

    // Functie om gegevens op te slaan (bijv. versturen naar backend)
    const handleSave = () => {
        // Hier zou je de gegevens naar je backend sturen om op te slaan
        console.log('Gegevens opgeslagen:', profile);
        alert('Profielgegevens opgeslagen!');
    };

    return (
        <div className="container p-3 ms-3">
            <h3>Profiel bewerken</h3>
            <form>
                <div className="my-3">
                    <label htmlFor="email" className="form-label">E-mail</label>
                    <input
                        type="email"
                        className="form-control px-2 py-1"
                        id="email"
                        name="email"
                        value={profile.email}
                        onChange={handleChange}
                        placeholder="Voer uw e-mailadres in"
                    />
                </div>
                <div className="mb-3">
                    <label htmlFor="fullName" className="form-label">Volledige naam</label>
                    <input
                        type="text"
                        className="form-control px-2 py-1"
                        id="fullName"
                        name="fullName"
                        value={profile.fullName}
                        onChange={handleChange}
                        placeholder="Voer uw volledige naam in"
                    />
                </div>
                <div className="mb-3">
                    <label htmlFor="password" className="form-label">Wachtwoord</label>
                    <input
                        type="password"
                        className="form-control px-2 py-1"
                        id="password"
                        name="password"
                        value={profile.password}
                        onChange={handleChange}
                        placeholder="Voer een nieuw wachtwoord in"
                    />
                </div>
                <button type="button" className="btn btn-primary px-5 py-2" onClick={handleSave}>Opslaan</button>
            </form>
        </div>
    );
};

export default ProfileContent;

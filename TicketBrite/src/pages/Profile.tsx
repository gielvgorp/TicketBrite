import { useState } from 'react';
import ProfileContent from '../components/Profile/ProfileContent/ProfileContent';
import EventsOverview from '../components/Profile/Organization/EventsOverview';
import { useAuth } from '../AuthContext';
import TicketContent from '../components/Profile/Tickets/TicketsContent';

const Profile = () => {
    // State voor het bijhouden van de huidige content die moet worden weergegeven
    const [activeContent, setActiveContent] = useState('profile');
    const { logout, role } = useAuth();

    // Functie om de content te veranderen op basis van klikken
    const handleContentChange = (content: any) => {
        setActiveContent(content);
    };

    return (
        <div className="container-fluid">
            <div className="row flex-nowrap">
                <div className="col-auto col-md-3 col-xl-2 px-sm-2 px-0 bg-dark">
                    <div className="d-flex flex-column align-items-center align-items-sm-start px-3 pt-2 text-white min-vh-100">
                        <a href="/" className="d-flex align-items-center pb-3 mb-md-0 me-md-auto text-white text-decoration-none">
                            <span className="fs-5 d-none d-sm-inline">Profiel</span>
                        </a>
                        <ul className="nav nav-pills flex-column mb-sm-auto mb-0 align-items-center align-items-sm-start" id="menu">
                            <li className="nav-item">
                                <a href="#" onClick={() => handleContentChange('profile')} className="nav-link align-middle px-0">
                                    <i className="fs-4 bi-house"></i> <span className="ms-1 d-none d-sm-inline"><i className="fa-solid fa-user px-2"></i> Profiel</span>
                                </a>
                            </li>
                            <li className="nav-item">
                                <a href="#" onClick={() => handleContentChange('tickets')} className="nav-link align-middle px-0">
                                    <span className="ms-1 d-none d-sm-inline"><i className="fa-solid fa-ticket px-2"></i> Tickets</span>
                                </a>
                            </li>
                            {
                                role === 'Organization' && (
                                    <li>
                                        <a href="#submenu1" data-bs-toggle="collapse" className="nav-link px-0 align-middle">
                                            <i className="fs-4 bi-speedometer2"></i> <span className="ms-1 d-none d-sm-inline"><i className="fa-solid fa-sitemap px-2"></i> Organisatie</span> 
                                        </a>
                                        <ul className="collapse nav flex-column ms-1" id="submenu1" data-bs-parent="#menu">
                                            <li className="w-100">
                                                <a href="#" onClick={() => handleContentChange('events')} className="nav-link px-0"> 
                                                    <span className="d-none d-sm-inline">Evenementen</span>
                                                </a>
                                            </li>
                                        </ul>
                                    </li>
                                )
                            }
                            <li className="nav-item justify-self-end">
                                <a href="#" onClick={logout} className="nav-link align-middle px-0">
                                    <i className="fs-4 bi-house"></i> <span className="text-danger ms-1 d-none d-sm-inline"><i className="fa-solid fa-right-from-bracket px-2"></i> Uitloggen</span>
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
                <div className="col py-3">
                    {/* Hier wordt de actieve content weergegeven op basis van de state */}
                    {activeContent === 'profile' && <ProfileContent /> }
                    {activeContent === 'tickets' && <TicketContent /> }
                    {activeContent === 'events' && <EventsOverview organizationID='77726785-FA72-4244-A572-AFFEAF20D5F1' /> }
                    {activeContent === 'logout' && <div>Uitloggen Content</div>}
                </div>
            </div>
        </div>
    );
};

export default Profile;

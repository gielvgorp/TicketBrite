import { useEffect, useState } from 'react';
import { Card, Button, ListGroup, Dropdown, Form, Row, Col } from 'react-bootstrap';
import '../ShoppingCart.css';
import { useNavigate } from 'react-router-dom';
import { shoppingCartItem, ApiResponse } from '../Types';
import ShoppingCartItem from '../components/ShoppingCart/ShoppingCartItem';
import { ErrorNotification } from '../components/Notifications/Notifications';

interface ShoppingCart {
    totalPrice: string;
    items: shoppingCartItem[];
}

const banks = [
    "ABN AMRO", "ASN Bank", "Bunq", "ING", "Knab", "Rabobank", "RegioBank", "SNS Bank", "Triodos Bank", "Van Lanschot"
];

function ShoppingCart(){
    const navigate = useNavigate();
    const [shoppingCart, setShoppingCart] = useState<ShoppingCart>();
    const [cartItems, setCartItems] = useState<shoppingCartItem[]>([]);
    const [paymentMethod, setPaymentMethod] = useState<string>("iDeal");
    const [selectedBank, setSelectedBank] = useState<string | null>(null);
    const [totalPrice, setTotalPrice] = useState(0);

    useEffect(() => {
        handleFetchTickets();
    }, []);

    const onTotalPriceChange = (value: number) => {
        setTotalPrice(totalPrice + value);
    }

    const handleFetchTickets = async () => {
        try {
            const token = localStorage.getItem("jwtToken");

            const response = await fetch('http://localhost:7150/api/ShoppingCart/items', {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                }
            });

            if (!response.ok) {
                throw new Error('Fout bij het ophalen van gebruikersgegevens');
            }

             const data = await response.json() as ApiResponse<ShoppingCart>;

            if(data.value){
                setShoppingCart(data.value);
                setCartItems(data.value.items);
                setTotalPrice(parseInt(data.value.totalPrice));
            }
        } catch (error) {
            console.error('Er is een fout opgetreden:', error);
        }
    }

    useEffect(() => {
        if (shoppingCart?.items) {
            setCartItems([...shoppingCart.items]);
        } else {
            setCartItems([]);
        }
    }, [shoppingCart]);

    const handlePurscheTickets = async () => {
        try {
            const token = localStorage.getItem("jwtToken");

            const response = await fetch('http://localhost:7150/api/Ticket/ticket/buy', {
                method: 'POST',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                }
            });

            if (!response.ok) {
                throw new Error('Fout bij het ophalen van gebruikersgegevens');
            }

            const data = await response.json() as ApiResponse<string>;

            if(data.statusCode !== 200){
                ErrorNotification({text: data.value ?? "Er is een onverwachte fout opgetreden!"});
            }

            if(data.statusCode === 200){
                navigate(`/Payment-success/${data.value}`, {replace: true});
            }          
        } catch (error) {
            console.error('Er is een fout opgetreden:', error);
        }
    }

    return (
        <div className="container mt-5">
            <Card className="p-4 shadow-lg shopping-cart-card mx-auto" style={{ maxWidth: '850px' }}>
                <Card.Body>
                    <h2 className="text-center mb-4" style={{ fontWeight: 'bold', fontSize: '2rem' }}>
                        <i className="fas fa-shopping-cart me-3 text-primary"></i>Winkelwagen
                    </h2>
                    {
                        shoppingCart !== undefined && shoppingCart?.items.length > 0 ? <ListGroup variant="flush" className="mb-4">
                        {cartItems.map(ticket => (
                           <ShoppingCartItem onRemoveItem={(value: number) => onTotalPriceChange(value)} ticket={ticket} />
                        ))}
                    </ListGroup> : <h6 className='text-center'>Geen items in de winkelwagen!</h6>
                    }
                   
                    {
                        shoppingCart?.items !== undefined && 
                        shoppingCart.items.length > 0 &&  
                        <>
                         <hr />
                        <Row className="align-items-center mb-4">
                            <Col>
                                <h5>Totaal:</h5>
                            </Col>
                            <Col className="text-end">
                                <h5 className="text-success">€{totalPrice}</h5>
                            </Col>
                        </Row>

                        <h5 className="mb-3">Kies uw betaalmethode:</h5>
                        <div className="d-flex align-items-center mb-4">
                            <Form.Check 
                                inline 
                                label="iDeal" 
                                type="radio" 
                                name="paymentMethod" 
                                checked={paymentMethod === "iDeal"} 
                                onChange={() => setPaymentMethod("iDeal")} 
                            />
                            <Form.Check 
                                inline 
                                label="Mastercard" 
                                type="radio" 
                                name="paymentMethod" 
                                checked={paymentMethod === "Mastercard"} 
                                onChange={() => setPaymentMethod("Mastercard")} 
                            />
                            <Form.Check 
                                inline 
                                label="Apple Pay" 
                                type="radio" 
                                name="paymentMethod" 
                                checked={paymentMethod === "Apple Pay"} 
                                onChange={() => setPaymentMethod("Apple Pay")} 
                            />
                        </div>
                        
                        {paymentMethod === "iDeal" && (
                                <Dropdown onSelect={(value) => setSelectedBank(value || null)} className="mt-3 w-25">
                                    <Dropdown.Toggle variant="secondary" id="dropdown-basic">
                                        {selectedBank || "Selecteer uw bank"}
                                    </Dropdown.Toggle>
                                    <Dropdown.Menu>
                                        {banks.map((bank, index) => (
                                            <Dropdown.Item key={index} eventKey={bank}>{bank}</Dropdown.Item>
                                        ))}
                                    </Dropdown.Menu>
                                </Dropdown>
                            )}
                            <Button onClick={handlePurscheTickets} id="payment-button" variant="success" className="w-100 mt-3">
                                <i className="fas fa-credit-card me-2"></i>
                                Doorgaan naar Betaling
                            </Button>
                        </>
                    }
                </Card.Body>
            </Card>
        </div>
    );
};


export default ShoppingCart;

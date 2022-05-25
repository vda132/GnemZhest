import { useState } from "react";
import { Form, Table, Button } from "react-bootstrap";
import useAuth from "../../hooks/useAuth";
import { toast } from "react-toastify";

import './styles.css'

function Cart() {
    const auth = useAuth();

    const [city, setCity] = useState("");
    const [adress, setAdress] = useState("");
    const [error, setError] = useState("");

    const handleValidation = () => {
        let formIsValid = true;

        if (city.length === 0) {
            setError("Please enter city");
            return false;
        } else {
            setError("");
            formIsValid = true;
        }

        if (adress.length === 0) {
            setError("Please enter adress");
            return false;
        } else {
            setError("");
            formIsValid = true;
        }

        return formIsValid;
    }

    const submitOrder = async () => {
        let response;

        if (handleValidation()) {
            response = await auth.cartItems.forEach(async (cartItem) => {
                console.log(cartItem);
                let res = await fetch(`https://localhost:5001/api/Orders`, {
                    method: "Post",
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({
                        "userId": auth.user.id,
                        "goodId": cartItem.item.id,
                        "city": city,
                        "orderAdress": adress,
                        "ammount": cartItem.ammount,
                        "price": auth.cartPrice
                    })
                });

                console.log(res)
                const data = await res.json();
                
                if (data.status === 200) {
                    toast.success(`${cartItem.item.name} order submitted`, {
                        position: "bottom-left",
                    });
                    auth.removeItems();
                } else {
                    setError(data.message);
                }
            });
            
        }


    }

    return (
        <section className="py-4 container">
            <div className="row justify-content-center">
                <h5>Cart ammount: {auth.cartAmmount}</h5>
                <Table size="sm md xl">
                    <tbody>
                        {auth.cartItems.map((item) => {
                            return (
                                <tr key={item.item.id}>
                                    <td>
                                        <img src={item.item.image1} alt="" style={{ height: '6rem' }} />
                                    </td>
                                    <td>
                                        {item.item.name}
                                    </td>
                                    <td>{item.item.price}</td>
                                    <td>Quantity ({item.ammount})</td>
                                    <td>
                                        <button className="btn btn-info ms-2" onClick={() => auth.addItemToCart(item.item)}>+</button>
                                        <button className="btn btn-info ms-2" disabled={item.ammount === 1} onClick={() => auth.decreaseItemFromCart(item.item)}>-</button>
                                        <button className="btn btn-danger ms-2" onClick={() => auth.removeItemFromCart(item.item)}>Remove</button>
                                    </td>
                                </tr>
                            )
                        })}
                    </tbody>
                </Table>
                <div className="col-auto ms-auto">
                    <h2>Total price: {auth.cartPrice}</h2>
                </div>
                {auth.cartItems.length > 0 ?
                    <Form >
                        <Form.Group className="mb-3" controlId="formBasicCity">
                            <Form.Label>City</Form.Label>
                            <Form.Control placeholder="Enter your city" value={city} onChange={(event) => setCity(event.target.value)} />
                        </Form.Group>
                        <Form.Group className="mb-3" controlId="formBasicStreet">
                            <Form.Label>Street adress</Form.Label>
                            <Form.Control type="street adress" placeholder="Enter your street adress" disabled={city.length === 0} value={adress} onChange={(event) => setAdress(event.target.value)} />
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Text className="text-muted">
                                We'll never share your adress information with anyone else.
                            </Form.Text>
                        </Form.Group>
                        <Button className="btn btn-primary" onClick={() => submitOrder()} >
                            Submit order
                        </Button>
                        <Form.Group className="mb-3">
                            <small id="error" className="text-danger form-text">
                                {error}
                            </small>
                        </Form.Group>
                    </Form> : null}
            </div>
        </section >
    )
}

export default Cart;
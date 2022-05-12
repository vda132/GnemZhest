import React, { useEffect, useState } from "react";
import { Card, Button, Container, Row, Carousel } from "react-bootstrap";
import { Link } from "react-router-dom";
import useAuth from "../../hooks/useAuth";

import './styles.css'


function Home() {
    const [products, setProducts] = useState();
    const [dataIsLoading, setDataIsLoading] = useState(true);

    const auth = useAuth();

    const getData = () => {
        fetch(`https://localhost:5001/api/goods`)
            .then((res) => res.json())
            .then((res) => {
                console.log("here");
                setProducts(res);
                setDataIsLoading(false);
            })
    }

    useEffect(() => {
        getData();
    }, [])

    return (
        <>
            {dataIsLoading ?
                <h3 style={{ marginLeft: "5%", marginTop: "1%" }}><em>Loading...</em></h3>
                : <div>
                    <Carousel className="carousel" variant="dark">
                        {products.map(product =>
                            <Carousel.Item key={product.id} as={Link} to={`/products/${product.id}`} >
                                <img className="d-block carouselImage" style={{ margin: "auto", width: "20%", justifyContent: "center" }}
                                    src={product.image1}
                                    alt={product.name}
                                    xs={12} sm={6} md={3} />
                                <Carousel.Caption >
                                    <h3>{product.name}</h3>
                                    <p>{product.price} грн</p>
                                </Carousel.Caption>
                            </Carousel.Item>
                        )}
                    </Carousel>
                    <div className="mainText" xs={12} sm={8} md={5}>
                        <h1>Все продукты</h1>
                    </div>
                    <Container className="productList">
                        <Row>
                            {products.map(product =>
                                <Card className="productCard" style={{ width: '18rem' }} xs={12} sm={6} md={3} key={product.id} as={Link} to={`/products/${product.id}`}>
                                    <Card.Img variant="top" src={product.image1} />
                                    <Card.Body>
                                        <Card.Title>{product.name}</Card.Title>
                                        <h5>
                                            {product.price} грн
                                        </h5>
                                        {auth.isLoaded && auth.user ?
                                            <Button variant="primary" as={Link} to="/register"> Buy </Button> : null}
                                    </Card.Body>
                                </Card>
                            )}
                        </Row>
                    </Container>
                </div>
            }
        </>
    )
}

export default Home;


import React, { useEffect, useState } from "react";
import { Button } from "react-bootstrap";
import { useParams } from "react-router-dom";
import { Zoom } from 'react-slideshow-image';
import useAuth from "../../hooks/useAuth";

import '..//pages//productPage.css'


function ProductPage() {
  const auth = useAuth();
  const [product, setProduct] = useState();
  const [images, setImages] = useState([]);
  const [dataIsLoading, setDataIsLoading] = useState(true);
  const { id } = useParams();

  const zoomOutProperties = {
    duration: 5000,
    transitionDuration: 500,
    infinite: true,
    indicators: true,
    scale: 0.4,
    arrows: true
  };

  const getProduct = () => {
    fetch(`https://localhost:5001/api/goods/${id}`)
      .then((res) => res.json())
      .then((res) => {
        setProduct(res);
        setImages([res.image1, res.image2, res.image3]);
        setDataIsLoading(false);
      });
  }

  useEffect(() => {
    getProduct();
  }, [])

  const Slideshow = () => {
    return (
      <div className="slide-container">
        <Zoom {...zoomOutProperties}>
          {images.map((each, index) => (
            <img alt="id" key={index} src={each} style={{ margin: "auto", width: "60%" }} />
          ))}
        </Zoom>
      </div>
    );
  };
  return (
    <div>
      {dataIsLoading ?
        <h3 style={{ marginLeft: "5%", marginTop: "1%" }}><em>Loading...</em></h3>
        :
        <div style={{ marginTop: "5%", display: "flex", padding: "0" }}>
          <div style={{ marginLeft: "5%", width: "50%" }}>
            <Slideshow />
          </div>
          <div className="productName">
            <div >
              <h1>{product.name}</h1>
            </div>
            <h2 style={{ marginTop: "5%" }}>Price: {product.price} грн</h2>
            {auth.token ?
              <Button variant="success" style={{ marginTop: "0", marginBottom: "7%", width: "60%" }} onClick={() => auth.addItemToCart(product)}>Buy</Button> : null}
            <br></br>
            <br></br>
            <span>
              <p style={{ marginBottom: "0" }}>Have questions?<br></br>Contact us:</p>
              <a href="tel:+380505652623" style={{ textDecoration: "none", color: "black" }}>+380505652623</a>
            </span>
          </div>
        </div>}
    </div>
  );
}

export default ProductPage;
import React, { useEffect, useState } from "react";
import { Button } from "react-bootstrap";
import { useParams } from "react-router-dom";
import { Zoom } from 'react-slideshow-image';

import '..//pages//productPage.css'


function ProductPage() {
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
        const images = [res.image1, res.image2, res.image3];
        setProduct(res);
        setImages(images);
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
            <img key={index} src={each} style={{ margin: "auto", width: "30%" }} />
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
        <div style={{marginTop:"5%", display:"flex", marginTop:"10px", padding:"0"}}>
          <div style={{marginLeft:"5%", width: "55%"}}>
            <Slideshow />
          </div>
          <div className="productName">
            <div >
              <h1>{product.name}</h1>
            </div>
            <br></br>
            <h2 style={{marginTop:"5%"}}>Price: {product.price} грн</h2>
            <br></br>
            <Button style={{marginTop:"5%", backgroundColor:"green", borderColor:"green"}}>Buy</Button>
          </div>
        </div>}
    </div>
  );
}

export default ProductPage;
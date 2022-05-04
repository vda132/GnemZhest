import React, { Component } from "react";
import { useParams } from "react-router-dom";
import { Zoom } from 'react-slideshow-image';



const images = [
    "/images/IMG_3858-e1505404847545-768x1024-1.jpg",
    "/logo512.png"
];

const zoomOutProperties = {
  duration: 5000,
  transitionDuration: 500,
  infinite: true,
  indicators: true,
  scale: 0.4,
  arrows: true
};

const Slideshow = () => {
  return (
    <div className="slide-container" style={{width:"70%", margin:"auto"}}>
      <Zoom {...zoomOutProperties}>
        {images.map((each, index) => (
          <img key={index} src={each} style={{margin:"auto", width:"30%"}} />
        ))}
      </Zoom>
    </div>
  );
};

function ProductPage() {
    const {id} = useParams();
        return (
            <>
            <h1>{id}</h1>
            <Slideshow/>
            </>
        );
}
export default ProductPage;
import Carousel from "react-bootstrap/Carousel";
import img1 from "../images/photo-1493238792000-8113da705763.jpg";
import img2 from "../images/photo-1597985933882-b4c6a08857a8.jpg";
import img3 from "../images/image-77971099-cb2b-46f1-96c9-b113896d7de9.jpg";
import { motion } from "framer-motion"; 

import { useState } from "react";

function UncontrolledExample() {
  const [index, setIndex] = useState(0);

  const handleSelect = (selectedIndex) => {
    setIndex(selectedIndex);
  };

  const slides = [
    {
      img: img1,
      title: "Welcome to Your Next Great Experience",
      desc: "Discover content tailored just for you. We're glad you're here — let’s make something amazing together.",
    },
    {
      img: img2,
      title: "Empowering Your Digital Journey",
      desc: "From tools to insights, we provide everything you need to move forward with confidence and clarity.",
    },
    {
      img: img3,
      title: "Start Small. Dream Big",
      desc: "Every great story begins with a single click. Yours starts now.",
    },
  ];

  return (
   
    
      <div style={{ height: "100%" }}>
        <Carousel
          activeIndex={index}
          onSelect={handleSelect}
          interval={3000}
          pause={false}
          fade
          style={{ fontFamily: "Playfair Display, serif" }}
        >
          {slides.map((slide, i) => (
            <Carousel.Item key={i}>
              <img width={1600} src={slide.img} alt="" />
              <motion.div
                key={index}
                animate={{ y: -300, opacity: 1 }}
                initial={{ y: 0, opacity: 0 }}
                transition={{ duration: 1 }}
              >
                <Carousel.Caption>
                  <h2 style={{ fontSize: "38px" }}>{slide.title}</h2>
                  <p>{slide.desc}</p>
                </Carousel.Caption>
              </motion.div>
            </Carousel.Item>
          ))}
        </Carousel>
      </div>
  );
}

export default UncontrolledExample;

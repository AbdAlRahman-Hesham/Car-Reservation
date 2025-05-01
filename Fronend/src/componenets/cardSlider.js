import React, { useRef } from "react";
import { useEffect, useContext, useState } from "react";
import { urlContext } from "../contexts/urlContext";
import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import KeyboardDoubleArrowRightIcon from "@mui/icons-material/KeyboardDoubleArrowRight";
import KeyboardDoubleArrowLeftIcon from "@mui/icons-material/KeyboardDoubleArrowLeft";
import CarCard from "./card";
import axios from "axios";
import { motion } from "framer-motion";
import { FadeLoader } from "react-spinners";

function CardsSlider() {
  const [data, setData] = useState([]);
  const { showAlertError, url } = useContext(urlContext);
  useEffect(() => {
    axios
      .get(
        `${url}/api/Car?PageSize=8&PageIndex=${
          Math.floor(Math.random() * 6) + 1
        }`
      )
      .then((res) => {
        setData([...res.data.data]);
      })
      .catch((err) => {
        showAlertError(
          "Something Went Wrong While Trying To Show The Cars Data"
        );
      });
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  const sliderRef = useRef(null);

  const settings = {
    dots: true,
    infinite: true,
    speed: 500,
    slidesToShow: 4,
    slidesToScroll: 1,
    initialSlide: 0,
    responsive: [
      {
        breakpoint: 1024,
        settings: {
          slidesToShow: 3,
          slidesToScroll: 3,
          infinite: true,
          dots: true,
        },
      },
      {
        breakpoint: 600,
        settings: {
          slidesToShow: 2,
          slidesToScroll: 2,
          initialSlide: 2,
        },
      },
      {
        breakpoint: 480,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
        },
      },
    ],
  };

  return (
    <>
      {data.length === 0 ? (
        <FadeLoader />
      ) : (
        <div>
          <div style={{ marginTop: "100px" }}>
            <h2
              style={{
                fontFamily: `Playfair Display,serif`,
                fontSize: "50px",
                wordSpacing: "10px",
                textShadow: "0px 0px 3px black",
              }}
            >
              Explore Our Car Collection
            </h2>
            <p>
              Your perfect car is here — the best, the newest, the one you need!
            </p>
          </div>
          <div
            className="slider-container"
            style={{
              width: "80vw",
              margin: "auto",
              position: "relative",
              marginBottom: "140px",
            }}
          >
            <motion.button
              className="purpleButtton"
              whileTap={{ x: -10 }}
              onClick={() => sliderRef.current.slickPrev()}
              style={{
                position: "absolute",
                top: "50%",
                left: "-50px",
                zIndex: 2,
                borderRadius: "50%",
              }}
            >
              <KeyboardDoubleArrowLeftIcon />
            </motion.button>
            <motion.button
              className="purpleButtton"
              whileTap={{ x: 10 }}
              onClick={() => sliderRef.current.slickNext()}
              style={{
                position: "absolute",
                top: "50%",
                right: "-50px",
                zIndex: 2,
                borderRadius: "50%",
              }}
            >
              <KeyboardDoubleArrowRightIcon />
            </motion.button>

            <Slider ref={sliderRef} {...settings}>
              {data
                .filter((car) => car.isAvailable)
                .map((car) => {
                  return (
                    <div key={car.id}>
                      <CarCard
                        id={car.id}
                        image={car.url}
                        brand={car.brand}
                        model={car.model}
                        price={car.price}
                        rating={car.rating}
                        isAvailable={car.isAvailable}
                      />
                    </div>
                  );
                })}
            </Slider>
          </div>
        </div>
      )}
    </>
  );
}

export default CardsSlider;

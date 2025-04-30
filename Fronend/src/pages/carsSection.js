import { useState, useContext, useEffect } from "react";
import { urlContext } from "../contexts/urlContext";
import { FadeLoader } from "react-spinners";
import CarCard from "../componenets/card";
import MyFilters from "../componenets/filter";
import { Button } from "react-bootstrap";
import axios from "axios";
import StarIcon from "@mui/icons-material/Star";
import { motion } from "framer-motion";

export default function Cars() {
  const { showAlertError, url } = useContext(urlContext);
  const [dataArray, setDataArray] = useState([]);
  const [pageInx, setPageIndx] = useState(1);
  const [loading, setMyLoading] = useState(true);
  const [isFiltered, setFiltred] = useState(false);
  const [onlyAvailable, setOnlyAvailable] = useState(false);
  function getCarsFromAPI() {
    axios
      .get(`${url}/api/Car?PageSize=10&PageIndex=${pageInx}`)
      .then((res) => {
        console.log(res.data.data);
        console.log(pageInx);
        setDataArray((prevdata) => [...prevdata, ...res.data.data]);
      })
      .catch((err) => {
        showAlertError(
          "Something Went Wrong While Trying To Show The Cars Data"
        );
      })
      .finally(() => {
        setMyLoading(false);
      });
  }

  function getCarsByFilteration(brandId, modelId) {
    if (brandId === 0) {
      setFiltred(false);
      setDataArray([]);
      setPageIndx(1);
      getCarsFromAPI();
      setMyLoading(true);
    } else {
      setDataArray([]);
      setFiltred(true);
      setPageIndx(1);
      setMyLoading(true);
      axios
        .get(`${url}/api/Car?BrandId=${brandId}&ModelId=${modelId}`)
        .then((res) => {
          setDataArray((prev) => []);
          setDataArray((prev) => [...res.data.data]);

          res.data.data.map((elem) => {
            console.log(elem.brand);
          });
        })
        .catch((err) => {
          showAlertError(
            "Something Went Wrong While Trying To Show The Cars Data"
          );
        })
        .finally(() => {
          setMyLoading(false);
        });
    }
  }
  function getOnlyAvailable(val) {
    setOnlyAvailable(val);
  }
  useEffect(() => {
    if (!isFiltered) {
      getCarsFromAPI();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [pageInx]);
  useEffect(() => {
    function handelScroll() {
      if (isFiltered) {
        return;
      }
      if (
        window.scrollY + window.innerHeight + 1 >=
          document.documentElement.scrollHeight &&
        pageInx > 1 &&
        pageInx < 8
      ) {
        setPageIndx((prevIndx) => prevIndx + 1);
      }
    }
    window.addEventListener("scroll", handelScroll);
    return () => {
      window.removeEventListener("scroll", handelScroll);
    };

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [pageInx]);
  useEffect(() => {
    window.scrollTo(0, 0);
  }, []);

  return (
    <div style={{ minHeight: "100vh" }}>
      <div style={{ position: "relative", margin: "0px" }}>
        {/*for wrapping */}
        <div className="carsSectionBackground"></div>
        <div
          style={{
            position: "relative",
            zIndex: "2",
            alignContent: "center",
            minHeight: "50vh",
          }}
        >
          <h1
            style={{
              fontSize: "100px",
              color: "white",
              letterSpacing: "8px",
              textShadow: "0px 0px 10px black",
            }}
          >
            Cars
          </h1>
        </div>
      </div>
      <div style={{ backgroundColor: "black" }}>
        <motion.h2
          animate={{ x: ["100%", "-100%"] }}
          transition={{
            repeat: Infinity,
            repeatType: "loop",
            duration: 13,
            ease: "linear",
          }}
          className="message"
        >
          <StarIcon color="gold" /> Find Your Perfect Car By Selecting Brand,
          Model, And Category. <StarIcon color="gold" />
        </motion.h2>
      </div>
      <MyFilters
        getBrand={getCarsByFilteration}
        getOnlyAvailable={getOnlyAvailable}
      />

      {loading ? (
        <div
          style={{
            height: "100vh",
            textAlign: "center",
            alignItems: "center",
            display: "flex",
            justifyContent: "center",
          }}
        >
          <FadeLoader size={50} color="#000" />
        </div>
      ) : (
        <>
          <div
            className="content container col-10"
            style={{
              marginTop: "50px",
              marginBottom: "10px",
            }}
          >
            {dataArray
              .filter((car) => {
                if (onlyAvailable) {
                  return car.isAvailable;
                } else {
                  return true;
                }
              })
              .map((car) => {
                return (
                  <CarCard
                    key={car.id}
                    id={car.id}
                    image={car.url}
                    brand={car.brand}
                    model={car.model}
                    price={car.price}
                    rating={car.rating}
                    isAvailable={car.isAvailable}
                  />
                );
              })}
          </div>
          {pageInx === 1 && dataArray.length !== 0 && !isFiltered && (
            <Button
              variant="light"
              onClick={() => {
                setPageIndx(2);
              }}
              style={{
                border: "solid 3px rgb(48, 47, 47)",
                color: "rgb(48, 47, 47)",
              }}
            >
              <b>Show More...</b>
            </Button>
          )}
        </>
      )}
      {/* here */}
    </div>
  );
}

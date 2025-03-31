import { Button } from "react-bootstrap";
import CarCard from "../componenets/card";
import { useEffect, useState } from "react";
import axios from "axios";
import { useContext } from "react";
import { urlContext } from "../contexts/urlContext";
import { PacmanLoader } from "react-spinners";

export default function MainLayOut() {
  const { showAlertError, url } = useContext(urlContext);
  const [dataArray, setDataArray] = useState([]);
  const [pageInx, setPageIndx] = useState(1);
  const [loading, setMyLoading] = useState(true);

  useEffect(() => {
    axios
      .get(`${url}/api/Car?PageSize=7&PageIndex=${pageInx}`)
      .then((res) => {
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
  }, [pageInx, showAlertError]);
  useEffect(() => {
    function handelScroll() {
      if (
        window.scrollY + window.innerHeight + 1 >=
          document.documentElement.scrollHeight &&
        pageInx > 1 &&
        pageInx < 5
      ) {
        setPageIndx((prevIndx) => prevIndx + 1);
        console.log(pageInx);
      }
    }
    window.addEventListener("scroll", handelScroll);
    return () => {
      window.removeEventListener("scroll", handelScroll);
    };
  }, [pageInx]);
  if (loading) {
    return (
      <div style={{ height: "100vh",textAlign:"center",alignItems:"center",display:"flex",justifyContent:"center" }}>
        <PacmanLoader size={50} color="#000" />
      </div>
    );
  } else {
    return (
      <>
        <div
          className="content container col-10"
          style={{
            marginTop: "50px",
            marginBottom: "10px",
          }}
        >
          {dataArray.map((car) => {
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
        {pageInx === 1 ? (
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
        ) : (
          <></>
        )}
      </>
    );
  }
}

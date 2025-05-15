import { useEffect, useState } from "react";
import { Link } from "react-router";
import { useParams } from "react-router";
import { useContext } from "react";
import { urlContext } from "../contexts/urlContext";
import axios from "axios";
import { motion } from "framer-motion";
import { Button } from "react-bootstrap";
import { BeatLoader } from "react-spinners";
import { useLocation } from "react-router";
import { Container } from "@mui/material";
import Grid from "@mui/material/Grid";
export default function Showdetails() {
  const { url } = useContext(urlContext);
  const { id } = useParams();
  const [selectedCar, setSelectedCar] = useState(null);
  const [loading, setMyLoading] = useState(true);
  useEffect(() => {
    axios
      .get(`${url}/api/Car/${id}`)
      .then((res) => {
        setSelectedCar(res.data);
      })
      .catch((err) => {
        console.log(err);
      })
      .finally(() => {
        setMyLoading(false);
      });
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  return (
    <>
      {!loading ? (
        <div>
          <div className="DetailsBackground"></div>
          <div>
            {" "}
            <Container className="containerForDetails">
              <div>
                <img
                  src={selectedCar.url}
                  alt="Car"
                  className="img-fluid imgContainer"
                />
              </div>
              <hr />
              <motion.div
                initial={{ x: -200, opacity: 0 }}
                animate={{ x: 0, opacity: 1 }}
                transition={{ duration: 2 }}
              >
                <Grid container spacing={2}>
                  <Grid size={4}>
                    <div className="d-flex justify-content-center flex-column align-items-center">
                      <div>
                        <h5>Barnd :</h5>
                        <h3>{selectedCar.brand}</h3>
                      </div>
                      <div>
                        <h5>Rate :</h5>
                        <h3>
                          {selectedCar.rating}
                          <small>/10</small>
                        </h3>
                      </div>
                    </div>
                  </Grid>
                  <Grid size={4}>
                    <div className="d-flex justify-content-center flex-column align-items-center">
                      <div>
                        <h5>Model :</h5>
                        <h3>{selectedCar.model}</h3>
                      </div>
                      <div>
                        <h5>Insurance Cost :</h5>
                        <h3>{selectedCar.insuranceCost}$</h3>
                      </div>
                    </div>
                  </Grid>
                  <Grid size={4}>
                    <div className="d-flex justify-content-center flex-column align-items-center">
                      <div>
                        <h5>Price Per Day:</h5>
                        <h3 style={{ color: "green" }}>{selectedCar.price}$</h3>
                      </div>
                      <div>
                        {selectedCar.isAvailable ? (
                          <Link to={`/reservation/${id}`}>
                            <Button
                              variant="success"
                              style={{ margin: "auto" }}
                            >
                              Rent And Pay Now !!
                            </Button>
                          </Link>
                        ) : (
                          <h4 style={{ color: "rgb(255, 50, 47)" }}>
                            Not Available For Now ...
                          </h4>
                        )}
                      </div>
                    </div>
                  </Grid>
                </Grid>
              </motion.div>
            </Container>
          </div>
        </div>
      ) : (
        <div
          style={{
            height: "100vh",
            textAlign: "center",
            alignContent: "center",
          }}
        >
          <BeatLoader size={30} color="#000" />
        </div>
      )}
    </>
  );
}

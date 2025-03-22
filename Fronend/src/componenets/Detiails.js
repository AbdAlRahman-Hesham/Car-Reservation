import { useEffect, useState } from "react";
import Car from "../R2.jpg";
import { useParams } from "react-router";
import { useContext } from "react";
import { urlContext } from "../contexts/urlContext";
import axios from "axios";
import { Button } from "react-bootstrap";
import { BeatLoader} from "react-spinners";
export default function Showdetails() {
  const { url } = useContext(urlContext);
  const { id } = useParams();
  const [selectedCar, setSelectedCar] = useState(null);
  const [loading, setMyLoading] = useState(true);
  useEffect(() => {
    axios
      .get(`${url}/api/Car/${id}`)
      .then((res) => {
        console.log(res.data);

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
        <div className="container col-10 card shadow p-3 mb-5 bg-body-tertiary">
          <div className="card-body">
            <img src={selectedCar.url} alt="Car" className="img-fluid" />
            {/* <p className="card-text" style={{ fontSize: "17px" }}>{i.body}</p> */}
            <hr />
            <table className="table table-bordered">
              <tbody>
                <tr>
                  <th>Car Liscence</th>
                  <td>{selectedCar.id * selectedCar.id}{selectedCar.id*28}</td>
                </tr>
                {/* <tr>
              <th>Availability</th>
              <td>{i.isAvailable ? "Available" : "Not Available"}</td>
            </tr> */}
                <tr>
                  <th>Model</th>
                  <td>{selectedCar.model}</td>
                </tr>
                <tr>
                  <th>Brand</th>
                  <td>{selectedCar.brand}</td>
                </tr>
                <tr>
                  <th>Rating</th>
                  <td>
                    {selectedCar.rating} <b>/10</b>{" "}
                  </td>
                </tr>
                <tr>
                  <th>Insurance Cost</th>
                  <td> <b>$</b> {selectedCar.insuranceCost}</td>
                </tr>
                <tr>
                  <th>Price</th>
                  <td><b>$</b> {selectedCar.price}</td>
                </tr>
              </tbody>
            </table>
          </div>
          { selectedCar.isAvailable ? 
            <Button variant="success" style={{ width: "75%", margin: "auto" }}>
              Rent And Pay Now !!
            </Button>:<h4 style={{color:"rgb(255, 50, 47)"}}> Sorry !! ..This Car Is Rented Already .. </h4>
          }
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

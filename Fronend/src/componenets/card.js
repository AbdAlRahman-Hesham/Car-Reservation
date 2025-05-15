import React from "react";
import Card from "react-bootstrap/Card";
import Button from "react-bootstrap/Button";
import { FaStar } from "react-icons/fa";
import { Link } from "react-router";
import { useNavigate } from "react-router";

function CarCard({ id, image, brand, model, price, rating, isAvailable }) {
  const navigate = useNavigate();
  function Rent() {
    if (localStorage.getItem("token")) {
      navigate(`/reservation/${id}`);
    } else {
      navigate(`/login?id=${id}`);
    }
  }

  return (
    <Card
      key={id}
      style={{
        width: "18rem",
        flexGrow: 0,
        flexShrink: 0,
        borderRadius: "10px",
        overflow: "hidden",
        backgroundColor: "#FFf2f2",
        border: "1px solid #7d26cd",
      }}
    >
      <Card.Img variant="top" src={image} alt={brand} height={200} />
      {!isAvailable && (
        <div className="info">
          <p style={{ display: "inline", color: "white" }}>
            Not Available For Now
          </p>
        </div>
      )}
      <Card.Body>
        <Card.Title>
          {brand} - {model}
        </Card.Title>
        <Card.Text></Card.Text>
        <Card.Text className="d-flex flex-column">
          <strong>Cost Per Day : ${price}</strong>
          <strong className="d-flex justify-content-center align-items-center">
            Rating: {rating} <b>/10</b>{" "}
            <FaStar style={{ marginLeft: "4px" }} color={"#ffc107"} />
          </strong>
        </Card.Text>
        <hr />
        <div className="d-flex justify-content-center" style={{ gap: "8px" }}>
          <Button
            onClick={Rent}
            disabled={!isAvailable}
            variant="light"
            className="purpleButtton"
          >
            Rent Now
          </Button>

          <Link to={`/details/${id}`}>
            <Button
              variant="light"
              style={{ color: "purple", border: "2px solid purple" }}
            >
              For Details...
            </Button>
          </Link>
        </div>
      </Card.Body>
    </Card>
  );
}

export default CarCard;

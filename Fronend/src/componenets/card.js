import React from "react";
import Card from "react-bootstrap/Card";
import Button from "react-bootstrap/Button";
import { FaStar } from "react-icons/fa";
import { Link } from "react-router";

function CarCard({ id, image, brand, model, price, rating, isAvailable }) {

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
          <strong>Cost: ${price}</strong>
          <strong>
            Rating: {rating} <b>/10</b> <FaStar color={"#ffc107"} />
          </strong>
        </Card.Text>
        <div className="d-flex justify-content-center" style={{ gap: "8px" }}>
          <Link>
            <Button disabled={!isAvailable} variant="success">Rent Now</Button>
          </Link>
          <Link to={`/details/${id}`}>
            <Button variant="info">For Details...</Button>
          </Link>
        </div>
      </Card.Body>
    </Card>
  );
}

export default CarCard;

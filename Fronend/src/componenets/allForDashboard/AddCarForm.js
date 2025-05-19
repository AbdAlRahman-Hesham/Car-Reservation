import React, { useState, useContext } from "react";
import { Form, Button, Row, Col, Card, Alert } from "react-bootstrap";
import { urlContext } from "../../contexts/urlContext";
import axios from "axios";
import "./dashboardStyling.css";

const AddCarForm = ({ onCarAdded }) => {
  const initialFormState = {
    brand: "",
    model: "",
    price: "",
    insuranceCost: "",
    imageUrl: "",
    isAvailable: true
  };

  const [formData, setFormData] = useState(initialFormState);
  const [validated, setValidated] = useState(false);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");

  const { url, token, showAlertSuccess, showAlertError } = useContext(urlContext);

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setFormData({
      ...formData,
      [name]: type === "checkbox" ? checked : value
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    const form = e.currentTarget;
    
    if (form.checkValidity() === false) {
      e.stopPropagation();
      setValidated(true);
      return;
    }

    setIsSubmitting(true);
    setError("");
    setSuccess("");

    // Convert price and insurance cost to numbers
    const carData = {
      ...formData,
      price: parseFloat(formData.price),
      insuranceCost: parseFloat(formData.insuranceCost)
    };

    axios.post(`${url}/api/cars`, carData, {
      headers: { Authorization: `Bearer ${token}` }
    })
      .then(response => {
        setSuccess(`${formData.brand} ${formData.model} has been added successfully.`);
        showAlertSuccess(`Car added successfully: ${formData.brand} ${formData.model}`);
        setFormData(initialFormState);
        setValidated(false);
        
        // Notify parent component that a car was added
        if (onCarAdded) onCarAdded();
      })
      .catch(error => {
        console.error("Error adding car:", error);
        setError("Failed to add car. Please check your input and try again.");
        showAlertError("Failed to add car. Please try again.");
      })
      .finally(() => {
        setIsSubmitting(false);
      });
  };

  return (
    <div className="add-car-form">
      <h2>Add New Car</h2>
      <p className="mb-4">Fill in the details below to add a new car to the inventory.</p>

      {error && <Alert variant="danger">{error}</Alert>}
      {success && <Alert variant="success">{success}</Alert>}

      <Card>
        <Card.Body>
          <Form noValidate validated={validated} onSubmit={handleSubmit}>
            <Row>
              <Col md={6}>
                <Form.Group className="mb-3">
                  <Form.Label>Brand *</Form.Label>
                  <Form.Control
                    type="text"
                    name="brand"
                    value={formData.brand}
                    onChange={handleChange}
                    placeholder="e.g., Toyota"
                    required
                  />
                  <Form.Control.Feedback type="invalid">
                    Please provide a car brand.
                  </Form.Control.Feedback>
                </Form.Group>
              </Col>
              <Col md={6}>
                <Form.Group className="mb-3">
                  <Form.Label>Model *</Form.Label>
                  <Form.Control
                    type="text"
                    name="model"
                    value={formData.model}
                    onChange={handleChange}
                    placeholder="e.g., Corolla"
                    required
                  />
                  <Form.Control.Feedback type="invalid">
                    Please provide a car model.
                  </Form.Control.Feedback>
                </Form.Group>
              </Col>
            </Row>

            <Row>
              <Col md={6}>
                <Form.Group className="mb-3">
                  <Form.Label>Daily Price ($) *</Form.Label>
                  <Form.Control
                    type="number"
                    name="price"
                    value={formData.price}
                    onChange={handleChange}
                    placeholder="e.g., 120.00"
                    min="0"
                    step="0.01"
                    required
                  />
                  <Form.Control.Feedback type="invalid">
                    Please provide a valid price.
                  </Form.Control.Feedback>
                </Form.Group>
              </Col>
              <Col md={6}>
                <Form.Group className="mb-3">
                  <Form.Label>Insurance Cost ($) *</Form.Label>
                  <Form.Control
                    type="number"
                    name="insuranceCost"
                    value={formData.insuranceCost}
                    onChange={handleChange}
                    placeholder="e.g., 25.00"
                    min="0"
                    step="0.01"
                    required
                  />
                  <Form.Control.Feedback type="invalid">
                    Please provide a valid insurance cost.
                  </Form.Control.Feedback>
                </Form.Group>
              </Col>
            </Row>

            <Form.Group className="mb-3">
              <Form.Label>Image URL *</Form.Label>
              <Form.Control
                type="url"
                name="imageUrl"
                value={formData.imageUrl}
                onChange={handleChange}
                placeholder="e.g., https://example.com/car-image.jpg"
                required
              />
              <Form.Control.Feedback type="invalid">
                Please provide a valid image URL.
              </Form.Control.Feedback>
              <Form.Text className="text-muted">
                Provide a link to an image of the car. The image should be in landscape orientation.
              </Form.Text>
            </Form.Group>

            <Form.Group className="mb-4">
              <Form.Check
                type="checkbox"
                name="isAvailable"
                label="Car is available for rent"
                checked={formData.isAvailable}
                onChange={handleChange}
              />
            </Form.Group>

            {formData.imageUrl && (
              <div className="mb-4">
                <p>Image Preview:</p>
                <div className="image-preview">
                  <img
                    src={formData.imageUrl}
                    alt="Car preview"
                    onError={(e) => {
                      e.target.onerror = null;
                      e.target.src = "https://via.placeholder.com/300x200?text=Invalid+Image+URL";
                    }}
                  />
                </div>
              </div>
            )}

            <div className="d-flex justify-content-end">
              <Button 
                variant="secondary" 
                className="me-2"
                onClick={() => {
                  setFormData(initialFormState);
                  setValidated(false);
                }}
              >
                Clear Form
              </Button>
              <Button 
                type="submit" 
                variant="primary" 
                disabled={isSubmitting}
              >
                {isSubmitting ? "Adding Car..." : "Add Car"}
              </Button>
            </div>
          </Form>
        </Card.Body>
      </Card>
    </div>
  );
};

export default AddCarForm;

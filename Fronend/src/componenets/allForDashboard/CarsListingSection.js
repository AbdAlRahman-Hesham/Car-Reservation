import React, { useState, useEffect, useContext } from "react";
import { Row, Col, Button, Card, Modal, Form } from "react-bootstrap";
import { urlContext } from "../../contexts/urlContext";
import axios from "axios";
import { FadeLoader } from "react-spinners";
import Select from "react-select";
import Checkbox from "@mui/material/Checkbox";
import "./dashboardStyling.css";

const checkBoxLabel = { inputProps: { "aria-label": "Checkbox demo" } };

const CarsListingSection = () => {
  const { url, showAlertError, showAlertSuccess } = useContext(urlContext);
  const [cars, setCars] = useState([]);
  const [pageIndex, setPageIndex] = useState(1);
  const [loading, setLoading] = useState(true);
  const [isFiltered, setIsFiltered] = useState(false);
  const [onlyAvailable, setOnlyAvailable] = useState(false);
  
  // For delete confirmation
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [carToDelete, setCarToDelete] = useState(null);
  
  // For edit functionality
  const [showEditModal, setShowEditModal] = useState(false);
  const [carToEdit, setCarToEdit] = useState(null);
  const [editFormData, setEditFormData] = useState({
    brand: "",
    model: "",
    price: "",
    insuranceCost: "",
    imageUrl: "",
    isAvailable: true,
    rating: 10
  });
  const [validated, setValidated] = useState(false);
  
  // For filtering
  const [brandsArray, setBrandsArray] = useState([{ value: 0, label: "All" }]);
  const [modelsArray, setModelsArray] = useState([]);
  const [categoriesArray, setCategoriesArray] = useState([]);
  const [selectedBrand, setSelectedBrand] = useState({ value: 0, label: "All" });
  const [selectedModel, setSelectedModel] = useState(null);
  const [selectedCategory, setSelectedCategory] = useState(null);
  const [check, setCheck] = useState(false);

  // Fetch brands for filter on component mount
  useEffect(() => {
    const token = localStorage.getItem('token');
    
    axios.get(`${url}/api/Brands`, {
      headers: {
        'Authorization': `Bearer ${token}`
      }
    })
      .then((res) => {
        let brands = res.data;
        let brandsData = brands.map((brand) => {
          return { value: brand.id, label: brand.name };
        });
        setBrandsArray([{ value: 0, label: "All" }, ...brandsData]);
      })
      .catch(error => {
        console.error("Error fetching brands:", error);
        showAlertError("Failed to load brands. Please try again.");
      });
  }, [url, showAlertError]);

  // Fetch models when brand is selected
  useEffect(() => {
    if (selectedBrand.value !== 0) {
      const token = localStorage.getItem('token');
      
      axios.get(`${url}/api/Models?brandid=${selectedBrand.value}`, {
        headers: {
          'Authorization': `Bearer ${token}`
        }
      })
        .then((res) => {
          let models = res.data;
          let modelsData = models.map((model) => {
            return { value: model.id, label: model.name };
          });
          let categoriesData = models.map((model) => {
            return { value: model.id, label: model.category };
          });
          setCategoriesArray(categoriesData);
          setModelsArray(modelsData);
        })
        .catch(error => {
          console.error("Error fetching models:", error);
          showAlertError("Failed to load models. Please try again.");
        });
    }
  }, [selectedBrand, url, showAlertError]);

  // Fetch cars on initial load and page change
  useEffect(() => {
    if (!isFiltered) {
      fetchCars();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [pageIndex]);

  // Add scroll listener for infinite scrolling
  useEffect(() => {
    function handleScroll() {
      if (isFiltered) {
        return;
      }
      
      if (
        window.scrollY + window.innerHeight + 1 >= 
        document.documentElement.scrollHeight &&
        !loading
      ) {
        setPageIndex((prevIndex) => prevIndex + 1);
      }
    }
    
    window.addEventListener("scroll", handleScroll);
    return () => {
      window.removeEventListener("scroll", handleScroll);
    };
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [pageIndex, isFiltered, loading]);

  const fetchCars = () => {
    setLoading(true);
    const token = localStorage.getItem('token');
    
    axios.get(`${url}/api/Car?PageSize=10&PageIndex=${pageIndex}`, {
      headers: {
        'Authorization': `Bearer ${token}`
      }
    })
      .then((res) => {
        console.log("Cars data:", res.data.data);
        setCars((prevCars) => [...prevCars, ...res.data.data]);
      })
      .catch((error) => {
        console.error("Error fetching cars:", error);
        showAlertError("Failed to load cars. Please try again.");
      })
      .finally(() => {
        setLoading(false);
      });
  };

  const getCarsByFilteration = (brandId, modelId) => {
    if (brandId === 0) {
      setIsFiltered(false);
      setCars([]);
      setPageIndex(1);
      fetchCars();
      setLoading(true);
    } else {
      setCars([]);
      setIsFiltered(true);
      setPageIndex(1);
      setLoading(true);
      
      const token = localStorage.getItem('token');
      
      axios.get(`${url}/api/Car?BrandId=${brandId}&ModelId=${modelId}`, {
        headers: {
          'Authorization': `Bearer ${token}`
        }
      })
        .then((res) => {
          setCars(res.data.data);
        })
        .catch((error) => {
          console.error("Error fetching filtered cars:", error);
          showAlertError("Failed to load filtered cars. Please try again.");
        })
        .finally(() => {
          setLoading(false);
        });
    }
  };

  const handleDelete = (car) => {
    setCarToDelete(car);
    setShowDeleteModal(true);
  };
  
  const handleEdit = (car) => {
    setCarToEdit(car);
    setEditFormData({
      brand: car.brand,
      model: car.model,
      price: car.price,
      insuranceCost: car.insuranceCost,
      imageUrl: car.url,
      isAvailable: car.isAvailable,
      rating: car.rating
    });
    setShowEditModal(true);
  };
  
  const handleEditFormChange = (e) => {
    const { name, value, type, checked } = e.target;
    setEditFormData({
      ...editFormData,
      [name]: type === "checkbox" ? checked : value
    });
  };
  
  const handleEditSubmit = (e) => {
    e.preventDefault();
    const form = e.currentTarget;
    
    if (form.checkValidity() === false) {
      e.stopPropagation();
      setValidated(true);
      return;
    }
    
    const updatedCar = {
      ...carToEdit,
      brand: editFormData.brand,
      model: editFormData.model,
      price: parseFloat(editFormData.price),
      insuranceCost: parseFloat(editFormData.insuranceCost),
      // Keep original image URL
      url: carToEdit.url,
      isAvailable: editFormData.isAvailable,
      rating: parseInt(editFormData.rating)
    };
    
    const token = localStorage.getItem('token');
    
    axios.put(`${url}/api/cars/${carToEdit.id}`, updatedCar, {
      headers: {
        'Authorization': `Bearer ${token}`
      }
    })
      .then(() => {
        showAlertSuccess(`${updatedCar.brand} ${updatedCar.model} has been updated.`);
        
        // Refresh cars list after update
        if (isFiltered) {
          getCarsByFilteration(selectedBrand.value, selectedModel ? selectedModel.value : '');
        } else {
          setCars([]);
          setPageIndex(1);
          fetchCars();
        }
      })
      .catch(error => {
        console.error("Error updating car:", error);
        showAlertError("Failed to update car. Please try again.");
      })
      .finally(() => {
        setShowEditModal(false);
        setCarToEdit(null);
        setValidated(false);
      });
  };

  const confirmDelete = () => {
    if (!carToDelete) return;
    console.log(carToDelete.id);
    const token = localStorage.getItem('token');
    
    axios.delete(`${url}/api/Car/${carToDelete.id}`, {
      headers: {
        'Authorization': `Bearer ${token}`
      }
    })
      .then((res) => {
        showAlertSuccess(`${carToDelete.brand} ${carToDelete.model} has been deleted.`);
        // Refresh cars list after deletion
        if (isFiltered) {
          getCarsByFilteration(selectedBrand.value, selectedModel ? selectedModel.value : '');
        } else {
          setCars([]);
          setPageIndex(1);
          fetchCars();
        }
      })
      .catch(error => {
        console.error("Error deleting car:", error);
        if (error.response && error.response.status === 400) {
          showAlertError("This car cannot be deleted because it is currently rented or has active reservations.");
        } else {
          showAlertError("Failed to delete car. Please try again.");
        }
      })
      .finally(() => {
        setShowDeleteModal(false);
        setCarToDelete(null);
      });
  };

  const handleOnlyAvailableChange = (value) => {
    setOnlyAvailable(value);
    setCheck(value);
  };

  return (
    <div className="cars-listing-section">
      <h2>All Cars</h2>
      
      {/* Filters */}
      <div
        style={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          margin: "20px",
          flexWrap: "wrap",
          gap: "20px"
        }}
      >
        {/* Brand Filter */}
        <div
          style={{
            display: "flex",
            alignItems: "center",
            gap: "10px",
            width: "20%",
          }}
        >
          <h5 style={{ margin: 0 }}>Brand:</h5>
          <div className="forSelect"> 
            <Select
              value={selectedBrand}
              onChange={(e) => {
                setSelectedBrand(e);
                setSelectedModel(null);
                setSelectedCategory(null);
                getCarsByFilteration(e.value, "");
              }}
              options={brandsArray}
              classNamePrefix="select"
              placeholder="Select a Brand"
              styles={{
                control: (provided) => ({
                  ...provided,
                  minHeight: "35px",
                }),
              }}
            />
          </div>
        </div>

        {/* Model Filter */}
        <div
          style={{
            display: "flex",
            alignItems: "center",
            gap: "10px",
            width: "20%",
          }}
        >
          <h5 style={{ margin: 0 }}>Model:</h5>
          <div className="forSelect"> 
            <Select
              value={selectedModel}
              onChange={(e) => {
                setSelectedModel(e);
                setSelectedCategory(
                  categoriesArray.find((c) => c.value === e.value)
                );
                getCarsByFilteration(selectedBrand.value, e.value);
              }}
              options={modelsArray}
              classNamePrefix="select"
              placeholder="Select a Model"
              isDisabled={selectedBrand.value === 0}
              styles={{
                control: (provided) => ({
                  ...provided,
                  minHeight: "35px",
                }),
              }}
            />
          </div>
        </div>

        {/* Category Filter */}
        <div
          style={{
            display: "flex",
            alignItems: "center",
            gap: "10px",
            width: "20%",
          }}
        >
          <h5 style={{ margin: 0 }}>Category:</h5>
          <div className="forSelect"> 
            <Select
              value={selectedCategory}
              onChange={(e) => {
                setSelectedCategory(e);
                setSelectedModel(modelsArray.find((m) => m.value === e.value));
                getCarsByFilteration(selectedBrand.value, e.value);
              }}
              options={categoriesArray}
              classNamePrefix="select"
              placeholder="Select a Category"
              isDisabled={selectedBrand.value === 0}
              styles={{
                control: (provided) => ({
                  ...provided,
                  minHeight: "35px",
                }),
              }}
            />
          </div>
        </div>

        {/* Checkbox */}
        <div
          style={{
            display: "flex",
            alignItems: "center",
            gap: "10px",
            width: "20%",
            justifyContent: "center",
          }}
        >
          <Checkbox
            {...checkBoxLabel}
            checked={check}
            onChange={(e) => {
              setCheck(e.target.checked);
              handleOnlyAvailableChange(e.target.checked);
            }}
          />
          <h6 style={{ margin: 0 }}>Only Available Cars</h6>
        </div>
      </div>
      <hr />
      
      {/* Cars Display */}
      {loading && cars.length === 0 ? (
        <div
          style={{
            height: "50vh",
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
          <Row className="mt-4">
            {cars
              .filter((car) => {
                if (onlyAvailable) {
                  return car.isAvailable;
                } else {
                  return true;
                }
              })
              .map((car) => (
                <Col key={car.id} lg={4} md={6} className="mb-4">
                  <Card className="car-card h-100">
                    <div className="car-image-container">
                      <Card.Img 
                        variant="top" 
                        src={car.url} 
                        alt={`${car.brand} ${car.model}`} 
                        className="car-image" 
                      />
                      {!car.isAvailable && (
                        <span className="rented-badge">Unavailable</span>
                      )}
                    </div>
                    <Card.Body>
                      <Card.Title>{car.brand} {car.model}</Card.Title>
                      <div className="car-details">
                        <div className="car-stat-item">
                          <span className="stat-label">Daily Price:</span>
                          <span className="stat-value">${car.price.toFixed(2)}</span>
                        </div>
                        <div className="car-stat-item">
                          <span className="stat-label">Insurance:</span>
                          <span className="stat-value">${car.insuranceCost.toFixed(2)}</span>
                        </div>
                        <div className="car-stat-item">
                          <span className="stat-label">Rating:</span>
                          <span className="stat-value">
                            {Array(Math.floor(car.rating)).fill().map((_, i) => (
                              <i key={i} className="fas fa-star"></i>
                            ))}
                            {Array(10 - Math.floor(car.rating)).fill().map((_, i) => (
                              <i key={i} className="far fa-star"></i>
                            ))}
                          </span>
                        </div>
                        <div className="car-stat-item">
                          <span className="stat-label">Status:</span>
                          <span className={`status-badge ${car.isAvailable ? 'available' : 'unavailable'}`}>
                            {car.isAvailable ? 'Available' : 'In Maintenance'}
                          </span>
                        </div>
                      </div>
                    </Card.Body>
                    <Card.Footer className="d-flex justify-content-between">
                      <Button 
                        variant="outline-primary" 
                        size="sm"
                        onClick={() => handleEdit(car)}
                      >
                        Edit
                      </Button>
                      <Button 
                        variant="outline-danger" 
                        size="sm"
                        onClick={() => handleDelete(car)}
                      >
                        Delete
                      </Button>
                    </Card.Footer>
                  </Card>
                </Col>
              ))}
          </Row>
          
          {!isFiltered && cars.length > 0 && pageIndex === 1 && (
            <div className="text-center mt-4 mb-4">
              <Button
                variant="light"
                onClick={() => setPageIndex(2)}
                style={{
                  border: "solid 3px rgb(48, 47, 47)",
                  color: "rgb(48, 47, 47)",
                }}
              >
                Load More
              </Button>
            </div>
          )}
          
          {loading && cars.length > 0 && (
            <div className="d-flex justify-content-center align-items-center w-100 my-4">
              <FadeLoader size={30} color="#2c3e50" />
            </div>
          )}
          
          {cars.length === 0 && !loading && (
            <div className="text-center mt-5">
              <h4>No cars found</h4>
              <p>Try adjusting your filters or add some cars to the inventory.</p>
            </div>
          )}
        </>
      )}
      
      {/* Delete Confirmation Modal */}
      <Modal show={showDeleteModal} onHide={() => setShowDeleteModal(false)}>
        <Modal.Header closeButton>
          <Modal.Title>Confirm Deletion</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          {carToDelete && (
            <p>Are you sure you want to delete the {carToDelete.brand} {carToDelete.model}? This action cannot be undone.</p>
          )}
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={() => setShowDeleteModal(false)}>
            Cancel
          </Button>
          <Button variant="danger" onClick={confirmDelete}>
            Delete
          </Button>
        </Modal.Footer>
      </Modal>
      
      {/* Edit Car Modal */}
      <Modal show={showEditModal} onHide={() => setShowEditModal(false)} size="lg">
        <Modal.Header closeButton>
          <Modal.Title>Edit Car</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          {carToEdit && (
            <Form noValidate validated={validated} onSubmit={handleEditSubmit}>
              <Row>
                <Col md={6}>
                  <Form.Group className="mb-3">
                    <Form.Label>Brand *</Form.Label>
                    <Form.Control
                      type="text"
                      name="brand"
                      value={editFormData.brand}
                      onChange={handleEditFormChange}
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
                      value={editFormData.model}
                      onChange={handleEditFormChange}
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
                      value={editFormData.price}
                      onChange={handleEditFormChange}
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
                      value={editFormData.insuranceCost}
                      onChange={handleEditFormChange}
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
              
              <Row>
                <Col md={6}>
                  <Form.Group className="mb-3">
                    <Form.Label>Rating (1-10) *</Form.Label>
                    <Form.Control
                      type="number"
                      name="rating"
                      value={editFormData.rating}
                      onChange={handleEditFormChange}
                      min="1"
                      max="10"
                      required
                    />
                    <Form.Control.Feedback type="invalid">
                      Please provide a rating between 1 and 10.
                    </Form.Control.Feedback>
                  </Form.Group>
                </Col>
                <Col md={6} className="d-flex align-items-end mb-3">
                  <Form.Group>
                    <Form.Check
                      type="checkbox"
                      name="isAvailable"
                      label="Car is available for rent (uncheck if in maintenance)"
                      checked={editFormData.isAvailable}
                      onChange={handleEditFormChange}
                    />
                  </Form.Group>
                </Col>
              </Row>

              {editFormData.imageUrl && (
                <div className="mb-4">
                  <p>Image Preview:</p>
                  <div className="image-preview" style={{ 
                    height: '200px', 
                    width: '80%',
                    margin: '0 auto',
                    display: 'flex',
                    justifyContent: 'center',
                    alignItems: 'center',
                    borderRadius: '8px', 
                    border: '1px solid #dee2e6',
                    backgroundColor: '#f8f9fa'
                  }}>
                    <img
                      src={editFormData.imageUrl}
                      alt="Car preview"
                      style={{ 
                        minWidth: '300px',
                        maxWidth: '100%', 
                        maxHeight: '180px', 
                        objectFit: 'contain',
                        display: 'block'
                      }}
                      onError={(e) => {
                        e.target.onerror = null;
                        e.target.src = "https://via.placeholder.com/300x200?text=Invalid+Image+URL";
                      }}
                    />
                  </div>
                </div>
              )}
            </Form>
          )}
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={() => setShowEditModal(false)}>
            Cancel
          </Button>
          <Button variant="primary" onClick={handleEditSubmit}>
            Save Changes
          </Button>
        </Modal.Footer>
      </Modal>
    </div>
  );
};

export default CarsListingSection;

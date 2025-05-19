import React, { useState, useEffect, useContext } from "react";
import { Row, Col, Card, Nav, Button } from "react-bootstrap";
import axios from "axios";
import { urlContext } from "../../contexts/urlContext";
import "./dashboardStyling.css";

// Import custom components
import CarsListingSection from "./CarsListingSection";
import AddCarForm from "./AddCarForm";

const CarsSection = () => {
  const [activeTab, setActiveTab] = useState("overview");
  const [carStats, setCarStats] = useState({
    totalCars: 0,
    currentlyRentedCars: 0,
    availableCars: 0,
    overallUtilizationRate: 0,
    mostUtilizedCars: [],
    leastUtilizedCars: [],
    utilizationByBrand: [],
    averageIdleDays: 0,
  });
  const [loading, setLoading] = useState(true);
  const { url, token, showAlertError } = useContext(urlContext);

  useEffect(() => {
    setLoading(true);
    // Fetch car statistics from the backend
    axios
      .get(`${url}/api/DachBoard/CarUtilization?startDate&endDate`)
      .then((res) => {
        console.log("Car stats response:", res.data);
        setCarStats(res.data);
      })
      .catch((error) => {
        console.error("Error fetching car statistics:", error);
        showAlertError("Failed to load car statistics. Please try again.");
      })
      .finally(() => {
        setLoading(false);
      });
  }, [url, token, showAlertError]);

  // Add event listener for tab changes to URL hash
  useEffect(() => {
    const handleHashChange = () => {
      const hash = window.location.hash.slice(1); // Remove the '#' character
      if (hash === "cars") {
        setActiveTab("all-cars");
      } else if (hash === "add-car") {
        setActiveTab("add-car");
      } else if (hash === "overview") {
        setActiveTab("overview");
      }
    };

    // Check hash on component mount
    handleHashChange();

    // Add event listener
    window.addEventListener("hashchange", handleHashChange);

    // Clean up event listener
    return () => {
      window.removeEventListener("hashchange", handleHashChange);
    };
  }, []);

  // Update URL hash when tab changes
  useEffect(() => {
    window.location.hash = activeTab;
  }, [activeTab]);

  // Function to render the statistics overview
  const renderOverview = () => (
    <>
      <h2>Cars Overview</h2>

      {/* Top Stats Cards */}
      <Row className="mt-3">
        <Col md={3} sm={6} className="mb-4">
          <Card className="stats-card">
            <Card.Body>
              <div className="stats-icon cars-icon">🚗</div>
              <h3>{carStats.totalCars}</h3>
              <p>Total Cars</p>
            </Card.Body>
          </Card>
        </Col>
        <Col md={3} sm={6} className="mb-4">
          <Card className="stats-card">
            <Card.Body>
              <div
                className="stats-icon available-icon"
                style={{ backgroundColor: "#36A2EB" }}
              >
                ✓
              </div>
              <h3>{carStats.availableCars}</h3>
              <p>Available Cars</p>
            </Card.Body>
          </Card>
        </Col>
        <Col md={3} sm={6} className="mb-4">
          <Card className="stats-card">
            <Card.Body>
              <div
                className="stats-icon rented-icon"
                style={{ backgroundColor: "#FF6384" }}
              >
                ⚡
              </div>
              <h3>{carStats.currentlyRentedCars}</h3>
              <p>Rented Cars</p>
            </Card.Body>
          </Card>
        </Col>
        <Col md={3} sm={6} className="mb-4">
          <Card className="stats-card">
            <Card.Body>
              <div className="stats-icon utilization-icon">📊</div>
              <h3>{carStats.overallUtilizationRate.toFixed(1)}%</h3>
              <p>Utilization Rate</p>
            </Card.Body>
          </Card>
        </Col>
      </Row>

      {/* Most Utilized Cars */}
      <h3 className="mt-4">Most Utilized Cars</h3>
      <Row className="mt-2">
        {carStats.mostUtilizedCars &&
          carStats.mostUtilizedCars.map((car) => (
            <Col md={4} sm={6} key={car.carId} className="mb-4">
              <Card className="car-card">
                <div className="car-image-container">
                  <img
                    src={car.imageUrl}
                    alt={`${car.brand} ${car.model}`}
                    className="car-image"
                  />
                  {car.isCurrentlyRented && (
                    <span className="rented-badge">Rented</span>
                  )}
                </div>
                <Card.Body>
                  <Card.Title>
                    {car.brand} {car.model}
                  </Card.Title>
                  <div className="car-stats">
                    <div className="car-stat-item">
                      <span className="stat-label">Utilization:</span>
                      <span className="stat-value">
                        {car.utilizationRate.toFixed(1)}%
                      </span>
                    </div>
                    <div className="car-stat-item">
                      <span className="stat-label">Reservations:</span>
                      <span className="stat-value">{car.reservationCount}</span>
                    </div>
                    <div className="car-stat-item">
                      <span className="stat-label">Revenue:</span>
                      <span className="stat-value">
                        ${car.revenue.toLocaleString()}
                      </span>
                    </div>
                  </div>
                </Card.Body>
              </Card>
            </Col>
          ))}
      </Row>

      {/* Least Utilized Cars - I'll only show the first three to save space */}
      <h3 className="mt-4">Least Utilized Cars</h3>
      <Row className="mt-2">
        {carStats.leastUtilizedCars &&
          carStats.leastUtilizedCars.slice(0, 3).map((car) => (
            <Col md={4} sm={6} key={car.carId} className="mb-4">
              <Card className="car-card">
                <div className="car-image-container">
                  <img
                    src={car.imageUrl}
                    alt={`${car.brand} ${car.model}`}
                    className="car-image"
                  />
                  {car.isCurrentlyRented && (
                    <span className="rented-badge">Rented</span>
                  )}
                </div>
                <Card.Body>
                  <Card.Title>
                    {car.brand} {car.model}
                  </Card.Title>
                  <div className="car-stats">
                    <div className="car-stat-item">
                      <span className="stat-label">Utilization:</span>
                      <span className="stat-value">
                        {car.utilizationRate.toFixed(1)}%
                      </span>
                    </div>
                    <div className="car-stat-item">
                      <span className="stat-label">Reservations:</span>
                      <span className="stat-value">{car.reservationCount}</span>
                    </div>
                    <div className="car-stat-item">
                      <span className="stat-label">Revenue:</span>
                      <span className="stat-value">
                        ${car.revenue.toLocaleString()}
                      </span>
                    </div>
                  </div>
                </Card.Body>
              </Card>
            </Col>
          ))}
      </Row>

      {/* Additional Quick Stats */}
      <Card className="mt-4">
        <Card.Body>
          <h3>Additional Insights</h3>
          <Row>
            <Col md={6}>
              <div className="insight-item">
                <span className="insight-label">Average Idle Days:</span>
                <span className="insight-value">
                  {carStats.averageIdleDays.toFixed(1)} days
                </span>
              </div>
            </Col>
            <Col md={6}>
              <div className="insight-item">
                <span className="insight-label">Top Brand:</span>
                <span className="insight-value">
                  {carStats.utilizationByBrand &&
                  carStats.utilizationByBrand.length > 0
                    ? carStats.utilizationByBrand.sort(
                        (a, b) =>
                          b.averageUtilizationRate - a.averageUtilizationRate
                      )[0].brandName
                    : "-"}
                </span>
              </div>
            </Col>
          </Row>
        </Card.Body>
      </Card>
    </>
  );

  const renderAllCars = () => {
    return <CarsListingSection />;
  };

  const renderAddCar = () => {
    return (
      <AddCarForm
        onCarAdded={() => {
          // Refresh the stats when a new car is added
          axios
            .get(`${url}/api/DachBoard/CarStatistics`, {
              headers: { Authorization: `Bearer ${token}` },
            })
            .then((res) => {
              setCarStats(res.data);
            })
            .catch((error) => {
              console.error("Error refreshing car statistics:", error);
            });
        }}
      />
    );
  };

  return (
    <div className="dashboard-content cars-section">
      <h1 className="section-title">Cars Management</h1>
      <p className="section-description">
        Manage your car inventory and view utilization metrics.
      </p>

      <Nav variant="tabs" className="mt-4 mb-4">
        <Nav.Item>
          <Nav.Link
            className={activeTab === "overview" ? "active" : ""}
            onClick={() => setActiveTab("overview")}
          >
            Overview
          </Nav.Link>
        </Nav.Item>
        <Nav.Item>
          <Nav.Link
            className={activeTab === "all-cars" ? "active" : ""}
            onClick={() => setActiveTab("all-cars")}
          >
            All Cars
          </Nav.Link>
        </Nav.Item>
        <Nav.Item>
          <Nav.Link
            className={activeTab === "add-car" ? "active" : ""}
            onClick={() => setActiveTab("add-car")}
          >
            Add New Car
          </Nav.Link>
        </Nav.Item>
      </Nav>

      {loading ? (
        <div className="text-center mt-5">
          <p>Loading car data...</p>
        </div>
      ) : (
        <div className="tab-content">
          {activeTab === "overview" && renderOverview()}
          {activeTab === "all-cars" && renderAllCars()}
          {activeTab === "add-car" && renderAddCar()}
        </div>
      )}
    </div>
  );
};

export default CarsSection;

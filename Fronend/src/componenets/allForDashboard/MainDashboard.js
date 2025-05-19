import React from "react";
import { Row, Col, Card } from "react-bootstrap";
import { Doughnut, Bar, Line } from "react-chartjs-2";
import "./dashboardStyling.css";

const MainDashboard = ({ stats, carStatusData, revenueData, monthlyTrendData }) => {
  return (
    <div className="dashboard-content">
      <h1 className="section-title">Dashboard Overview</h1>
      <p className="section-description">
        Welcome to the admin dashboard. Here you can monitor car rental
        statistics.
      </p>

      {/* Top Stats Cards */}
      <Row className="stats-cards-container">
        <Col md={3} sm={6} className="mb-4">
          <Card className="stats-card">
            <Card.Body>
              <div className="stats-icon cars-icon">🚗</div>
              <h3>{stats.totalCars}</h3>
              <p>Total Cars</p>
            </Card.Body>
          </Card>
        </Col>
        <Col md={3} sm={6} className="mb-4">
          <Card className="stats-card">
            <Card.Body>
              <div className="stats-icon customers-icon">👥</div>
              <h3>{stats.totalCustomers}</h3>
              <p>Total Customers</p>
            </Card.Body>
          </Card>
        </Col>
        <Col md={3} sm={6} className="mb-4">
          <Card className="stats-card">
            <Card.Body>
              <div className="stats-icon revenue-icon">💵</div>
              <h3>${stats.monthlyRevenue.toLocaleString()}</h3>
              <p>Monthly Revenue</p>
            </Card.Body>
          </Card>
        </Col>
        <Col md={3} sm={6} className="mb-4">
          <Card className="stats-card">
            <Card.Body>
              <div className="stats-icon duration-icon">⏱️</div>
              <h3>{stats.averageReservationDuration.toFixed(1)} days</h3>
              <p>Avg. Reservation</p>
            </Card.Body>
          </Card>
        </Col>
      </Row>

      {/* Charts Section */}
      <Row className="charts-container">
        <Col lg={4} md={6} className="mb-4">
          <Card className="chart-card">
            <Card.Body>
              <h4 className="chart-title">Car Status</h4>
              <div className="chart-container">
                <Doughnut
                  data={carStatusData}
                  options={{ responsive: true, maintainAspectRatio: false }}
                />
              </div>
              <div className="chart-legend">
                <div className="legend-item">
                  <span
                    className="legend-color"
                    style={{ backgroundColor: "#36A2EB" }}
                  ></span>
                  <span>Available: {stats.availableCars}</span>
                </div>
                <div className="legend-item">
                  <span
                    className="legend-color"
                    style={{ backgroundColor: "#FF6384" }}
                  ></span>
                  <span>Rented: {stats.rentedCars}</span>
                </div>
              </div>
            </Card.Body>
          </Card>
        </Col>
        <Col lg={4} md={6} className="mb-4">
          <Card className="chart-card">
            <Card.Body>
              <h4 className="chart-title">Revenue Comparison</h4>
              <div className="chart-container">
                <Bar
                  data={revenueData}
                  options={{ responsive: true, maintainAspectRatio: false }}
                />
              </div>
            </Card.Body>
          </Card>
        </Col>
        <Col lg={4} md={12} className="mb-4">
          <Card className="chart-card">
            <Card.Body>
              <h4 className="chart-title">Monthly Trends</h4>
              <div className="chart-container">
                <Line
                  data={monthlyTrendData}
                  options={{ responsive: true, maintainAspectRatio: false }}
                />
              </div>
            </Card.Body>
          </Card>
        </Col>
      </Row>

      {/* Summary Section */}
      <Row className="summary-section">
        <Col md={12}>
          <Card className="summary-card">
            <Card.Body>
              <h4>Business Summary</h4>
              <Row>
                <Col md={6}>
                  <div className="summary-item">
                    <span className="summary-label">Total Revenue:</span>
                    <span className="summary-value">
                      ${stats.totalRevenue.toLocaleString()}
                    </span>
                  </div>
                  <div className="summary-item">
                    <span className="summary-label">Total Cars:</span>
                    <span className="summary-value">{stats.totalCars}</span>
                  </div>
                </Col>
                <Col md={6}>
                  <div className="summary-item">
                    <span className="summary-label">Utilization Rate:</span>
                    <span className="summary-value">
                      {((stats.rentedCars / stats.totalCars) * 100).toFixed(1)}%
                    </span>
                  </div>
                  <div className="summary-item">
                    <span className="summary-label">Customers per Car:</span>
                    <span className="summary-value">
                      {(stats.totalCustomers / stats.totalCars).toFixed(2)}
                    </span>
                  </div>
                </Col>
              </Row>
            </Card.Body>
          </Card>
        </Col>
      </Row>
    </div>
  );
};

export default MainDashboard;

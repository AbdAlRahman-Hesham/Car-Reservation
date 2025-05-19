import React, { useState, useEffect, useContext } from "react";
import { urlContext } from "../contexts/urlContext";
import "../componenets/allForDashboard/dashboardStyling.css";
import {
  Chart as ChartJS,
  ArcElement,
  Tooltip,
  Legend,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  BarElement,
  Title,
} from "chart.js";
import axios from "axios";
import { useNavigate } from "react-router";

// Import Components
import DashboardSidebar from "../componenets/allForDashboard/DashboardSidebar";
import MainDashboard from "../componenets/allForDashboard/MainDashboard";
import CarsSection from "../componenets/allForDashboard/CarsSection";
import ReservationsSection from "../componenets/allForDashboard/ReservationsSection";

// Register ChartJS components
ChartJS.register(
  ArcElement,
  Tooltip,
  Legend,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  BarElement,
  Title
);

export default function Dashboard() {
  const [activeSection, setActiveSection] = useState("main");
  const [stats, setStats] = useState({
    totalCars: 0,
    availableCars: 0,
    rentedCars: 0,
    totalCustomers: 0,
    monthlyRevenue: 0,
    totalRevenue: 0,
    averageReservationDuration: 0,
  });
  const [loading, setLoading] = useState(true);
  const { url, token, showAlertError } = useContext(urlContext);
  const navigate = useNavigate();

  // Check authentication on mount
  useEffect(() => {
    // Check if token exists and user is admin
    const isAuthenticated = !!token || !!localStorage.getItem('token');
    const isAdmin = localStorage.getItem('isAdmin') === 'true';
    
    if (!isAuthenticated || !isAdmin) {
      // Redirect to admin login page if not authenticated or not admin
      navigate('/admin-login');
    }
  }, [token, navigate]);

  useEffect(() => {
      setLoading(true);
      // Fetch statistics from the backend
    axios.get(`${url}/api/DachBoard/DetailedStatistics`).then((res) => {
        setStats(res.data)
    }).catch((error)=>{
        console.error("Error fetching statistics:", error);
        showAlertError("Failed to load statistics. Please try again.");
    }).finally(()=>{
        setLoading(false);
    })
  }, [url, token, showAlertError]);

  // Data for donut chart - Car Status
  const carStatusData = {
    labels: ["Available Cars", "Rented Cars"],
    datasets: [
      {
        data: [stats.availableCars, stats.rentedCars],
        backgroundColor: ["#36A2EB", "#FF6384"],
        hoverBackgroundColor: ["#36A2EB", "#FF6384"],
      },
    ],
  };

  // Data for bar chart - Revenue
  const revenueData = {
    labels: ["Monthly Revenue", "Total Revenue"],
    datasets: [
      {
        label: "Revenue (USD)",
        data: [stats.monthlyRevenue, stats.totalRevenue],
        backgroundColor: ["rgba(54, 162, 235, 0.5)", "rgba(255, 99, 132, 0.5)"],
        borderColor: ["rgba(54, 162, 235, 1)", "rgba(255, 99, 132, 1)"],
        borderWidth: 1,
      },
    ],
  };

  // Demo data for line chart - Monthly trend (simulated data)
  const monthlyTrendData = {
    labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun"],
    datasets: [
      {
        label: "Reservations",
        data: [12, 19, 15, 28, 22, 30],
        borderColor: "rgb(75, 192, 192)",
        tension: 0.1,
        fill: false,
      },
      {
        label: "Revenue ($K)",
        data: [15, 20, 18, 40, 35, 41],
        borderColor: "rgb(255, 159, 64)",
        tension: 0.1,
        fill: false,
      },
    ],
  };

  // Render the active section based on user selection
  const renderActiveSection = () => {
    switch (activeSection) {
      case "cars":
        return <CarsSection />;
      case "reservations":
        return <ReservationsSection />;
      default:
        return <MainDashboard 
          stats={stats} 
          carStatusData={carStatusData} 
          revenueData={revenueData} 
          monthlyTrendData={monthlyTrendData} 
        />;
    }
  };

  return (
    <div className="dashboard-container" >
      {/* Sidebar Component */}
      <DashboardSidebar activeSection={activeSection} setActiveSection={setActiveSection} />

      {/* Main Content */}
      <div className="dashboard-main">{renderActiveSection()}</div>
    </div>
  );
}

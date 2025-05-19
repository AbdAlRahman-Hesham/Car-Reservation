import React, { useState, useEffect, useContext } from "react";
import { Button } from "react-bootstrap";
import adminImage from "../../images/user.jpg";
import { useNavigate } from "react-router";
import { urlContext } from "../../contexts/urlContext";
import Swal from "sweetalert2";
import "./dashboardStyling.css";

const DashboardSidebar = ({ activeSection, setActiveSection }) => {
  const navigate = useNavigate();
  const [userName, setUserName] = useState("Admin User");
  const [userImage, setUserImage] = useState(adminImage);
  const { showAlertSuccess } = useContext(urlContext);
  
  useEffect(() => {
    // Get user data from localStorage
    const storedName = localStorage.getItem("fName");
    const storedImage = localStorage.getItem("picUrl");
    
    if (storedName) {
      setUserName(storedName);
    }
    
    if (storedImage && storedImage !== "null" && storedImage !== "undefined") {
      setUserImage(storedImage);
    }
  }, []);
  
  const handleLogout = () => {
    // Show confirmation dialog
    Swal.fire({
      title: "Logout Confirmation",
      text: "Are you sure you want to logout?",
      icon: "question",
      showCancelButton: true,
      confirmButtonColor: "#e74c3c",
      cancelButtonColor: "#7f8c8d",
      confirmButtonText: "Yes, Logout",
      cancelButtonText: "Cancel"
    }).then((result) => {
      if (result.isConfirmed) {
        // Show goodbye message
        Swal.fire({
          title: `Goodbye, ${userName}!`,
          text: "See you soon! Thanks for managing the car reservation system.",
          icon: "success",
          confirmButtonColor: "#3498db",
          timer: 2000,
          timerProgressBar: true,
          showConfirmButton: false
        }).then(() => {
          // Clear localStorage
          localStorage.removeItem("token");
          localStorage.removeItem("isAdmin");
          localStorage.removeItem("fName");
          localStorage.removeItem("picUrl");
          
          // Redirect to admin login
          navigate("/admin-login");
        });
      }
    });
  };
  return (
    <div className="dashboard-sidebar">
      <div className="admin-profile">
        <img 
          src={userImage} 
          alt="Admin" 
          className="admin-image" 
          onError={(e) => {
            e.target.onerror = null;
            e.target.src = adminImage;
          }}
        />
        <h3 className="admin-name">{userName}</h3>
        <p className="admin-role">System Administrator</p>
      </div>

      <div className="sidebar-menu">
        <Button
          className={`sidebar-menu-item ${
            activeSection === "main" ? "active" : ""
          }`}
          onClick={() => setActiveSection("main")}
        >
          📊 Dashboard
        </Button>
        <Button
          className={`sidebar-menu-item ${
            activeSection === "cars" ? "active" : ""
          }`}
          onClick={() => setActiveSection("cars")}
        >
          🚗 Cars
        </Button>
        <Button
          className={`sidebar-menu-item ${
            activeSection === "reservations" ? "active" : ""
          }`}
          onClick={() => setActiveSection("reservations")}
        >
          📅 Reservations
        </Button>
      </div>
      
      <div className="sidebar-footer">
        <Button
          className="sidebar-menu-item logout-btn"
          onClick={handleLogout}
        >
          <span className="d-flex align-items-center justify-content-center w-100">
            <i className="fas fa-sign-out-alt mr-2" style={{ marginRight: '8px' }}></i> Logout
          </span>
        </Button>
      </div>
    </div>
  );
};

export default DashboardSidebar;

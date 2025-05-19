import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import { Route, Routes, Navigate, useLocation } from "react-router";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import { urlContext } from "./contexts/urlContext";
import { useState } from "react";
import Swal from "sweetalert2";
import Registration from "./pages/ValForm";
import LoginForm from "./pages/loginForm";
import AdminLogin from "./pages/adminLogin";

import MainLayOut from "./pages/main";
import Cars from "./pages/carsSection";
import Footer from "./componenets/footer";
import Showdetails from "./componenets/Detiails";
import NavBar from "./componenets/myNav";
import AboutUsPage from "./pages/aboutUS";
import Reservation from "./pages/completeReservation";
import Profile from "./pages/profile";
import Dashboard from "./pages/dashboard";

function App() {
  const [token, setToken] = useState(localStorage.getItem("token"));
  const [loading, setLoading] = useState(false);
  const [selected, setSelected] = useState(useLocation().pathname);

  const showAlertError = (message = "Please try again later.") => {
    Swal.fire({
      title: "⚠️ Oops! Something went wrong!",
      text: message,
      icon: "error",
      confirmButtonColor: "#dc3545",
      confirmButtonText: "Try Again 🔄",
    });
  };
  const showAlertSuccess = (message = "Operation completed successfully!") => {
  Swal.fire({
    title: "✅ Success!",
    text: message,
    icon: "success",
    confirmButtonColor: "#28a745", // لون أخضر
    confirmButtonText: "Great! 🎉",
  });
};
  return (
    <urlContext.Provider
      value={{
        url: "https://car-reservation.runasp.net",
        setMyToken: (t) => {
          setToken(t);
        },
        showAlertError: showAlertError,
        showAlertSuccess:showAlertSuccess,
        token: token,
        loading: loading,
        setMyLoading: (L) => {
          setLoading(L);
        },
        selected: selected,
        setSelected: (S) => {
          setSelected(S);
        },
      }}
    >
      <div className="App">
        {["/login", "/register", "/reservation","/dashboard", "/admin-login"].includes(
          useLocation().pathname.replace(/\/\d+/g, "")
        ) ? (
          <></>
        ) : (
          <NavBar />
        )}

        <Routes>
          <Route path="/" element={<Navigate to="/home" />} />
          <Route path="/home" element={<MainLayOut />} />
          <Route path="/login" element={<LoginForm />} />
          <Route path="/register" element={<Registration />} />
          <Route path="/admin-login" element={<AdminLogin />} />
          <Route path="/cars" element={<Cars />} />
          <Route path="/details/:id" element={<Showdetails />} />
          <Route path="/reservation/:id" element={<Reservation />} />
          <Route path="/aboutUs" element={<AboutUsPage />} />
          <Route path="/profile" element={<Profile />} />
          <Route path="/dashboard" element={<Dashboard />} />
        </Routes>

        {["/login", "/register", "/reservation","/dashboard", "/admin-login"].includes(
          useLocation().pathname.replace(/\/\d+/g, "")
        ) ? (
          <></>
        ) : (
          <Footer />
        )}
      </div>
    </urlContext.Provider>
  );
}

export default App;

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

import MainLayOut from "./pages/main";
import Cars from "./pages/carsSection";
import Footer from "./componenets/footer";
import Showdetails from "./componenets/Detiails";
import NavBar from "./componenets/myNav";
import AboutUsPage from "./pages/aboutUS";
import Reservation from "./pages/completeReservation";

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
  return (
    <urlContext.Provider
      value={{
        url: "https://car-reservation.runasp.net",
        setMyToken: (t) => {
          setToken(t);
        },
        showAlertError: showAlertError,
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
        {["/login", "/register", "/reservation"].includes(
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
          <Route path="/cars" element={<Cars />} />
          <Route path="/details/:id" element={<Showdetails />} />
          <Route path="/reservation/:id" element={<Reservation />} />
          <Route path="/aboutUs" element={<AboutUsPage />} />
        </Routes>

        {["/login", "/register", "/reservation"].includes(
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

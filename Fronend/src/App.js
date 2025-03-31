import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import { Route, Routes, Navigate } from "react-router";
import { urlContext } from "./contexts/urlContext";
import { useState } from "react";
import Swal from "sweetalert2";
import Registration from "./pages/ValForm";
import LoginForm from "./pages/loginForm";
import NavBar from "./componenets/myNav";
import MainLayOut from "./pages/main";
import ProfileLayout from "./pages/profile";
import Footer from "./componenets/footer";
import Showdetails from "./componenets/Detiails";

function App() {
  const [token, setToken] = useState(localStorage.getItem("token"));
  const [loading, setLoading] = useState(false);
  const [selected, setSelected] = useState("H");

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
        url: "http://car-reservation.runasp.net",
        setMyToken: (t) => {
          setToken(t);
        },
        showAlertError: showAlertError,
        token: token,
        loading: loading,
        setMyLoading: (L) => {
          setLoading(L);
        },
        selected:selected,
        setSelected:(S)=>{setSelected(S)}
      }}
    >
      <div className="App ">
        <NavBar />
    
        <Routes>
          <Route path="/" element={<Navigate to="/home" />} />
          <Route path="/home" element={<MainLayOut />} />
          <Route path="/login" element={<LoginForm />} />
          <Route path="/register" element={<Registration />} />
          <Route path="/profile" element={<ProfileLayout />} />
          <Route path="/details/:id" element={<Showdetails/>} />
        </Routes>
        {/* <LoginForm />
        <LoginForm /> */}
        <Footer />
      </div>
    </urlContext.Provider>
  );
}

export default App;

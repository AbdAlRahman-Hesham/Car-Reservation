import { Link, NavLink } from "react-router";

import Container from "react-bootstrap/Container";
import { Button } from "react-bootstrap";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import Logo from "../images/logo.jpg";
import { useContext, useState } from "react";
import { urlContext } from "../contexts/urlContext";
import { useLocation } from "react-router";
import Swal from "sweetalert2";
import { useEffect } from "react";
import { useNavigate } from "react-router";
export default function NavBar() {
  const { setMyToken, token, selected, setSelected } = useContext(urlContext);
  const [fixedNavBar, setNavBar] = useState(false);

  const location = useLocation();
  const navigate = useNavigate();

  useEffect(() => {
    setSelected(location.pathname);
    function hanelScroll() {
      console.log("scrolllllllll");
      if (window.scrollY > 0) {
        setNavBar(true);
      } else {
        setNavBar(false);
      }
    }
    window.addEventListener("scroll", hanelScroll);
    return () => {
      window.removeEventListener("scroll", hanelScroll);
    };
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  const showAlertLoggedOutSuccess = () => {
    Swal.fire({
      title: "👋 See you soon, safe driver!",
      text: "You've logged out successfully.You Are Running As Guest Now!",
      icon: "info",
      confirmButtonColor: "#dc3545",
      confirmButtonText: "Ok!",
    });
  };

  return (
    <>
      <Navbar
        className={`${fixedNavBar ? "fixed" : "absolute"}`}
        style={{ transition: " all 0.8s" }}
      >
        <Container className="d-flex p-0 ">
          <Navbar.Brand
            className="p-0 d-flex flex-column align-items-center"
            as={NavLink}
            to="/home"
          >
            <img
              src={Logo}
              alt=""
              width={250}
              height={70}
              style={{ margin: "0px" }}
            />
          </Navbar.Brand>
          <div style={{ marginTop: "20px" }}>
            <Nav className="me-aut d-flex align-items-center">
              <Nav.Link as={NavLink} to="/home">
                <h5
                  className={`${
                    location.pathname === "/home" ? "selected" : ""
                  } `}
                >
                  Home
                </h5>
              </Nav.Link>
              <Nav.Link as={NavLink} to="/cars">
                <h5
                  className={`${
                    location.pathname === "/cars" ? "selected" : ""
                  } `}
                >
                  Cars
                </h5>
              </Nav.Link>
              {/* <Nav.Link
                as={NavLink}
                to="/"
                onClick={() => {
                  setSelected("/home");
                }}
              >
                <h5 className={`${selected === "/home" ? "selected" : ""} `}>
                  Contact Us
                </h5>
              </Nav.Link> */}
              <Nav.Link as={NavLink} to="/aboutUs">
                <h5
                  className={`${
                    location.pathname === "/aboutUs" ? "selected" : ""
                  } `}
                >
                  About US
                </h5>
              </Nav.Link>

              {!token ? (
                <Nav.Link
                  as={NavLink}
                  to="/login"
                  style={{ marginLeft: "45px" }}
                >
                  <div
                    aria-label="User Login Button"
                    tabIndex="0"
                    role="button"
                    className="user-profile"
                  >
                    <div className="user-profile-inner">
                      <svg
                        aria-hidden="true"
                        xmlns="http://www.w3.org/2000/svg"
                        viewBox="0 0 24 24"
                      >
                        <g data-name="Layer 2" id="Layer_2">
                          <path d="m15.626 11.769a6 6 0 1 0 -7.252 0 9.008 9.008 0 0 0 -5.374 8.231 3 3 0 0 0 3 3h12a3 3 0 0 0 3-3 9.008 9.008 0 0 0 -5.374-8.231zm-7.626-4.769a4 4 0 1 1 4 4 4 4 0 0 1 -4-4zm10 14h-12a1 1 0 0 1 -1-1 7 7 0 0 1 14 0 1 1 0 0 1 -1 1z"></path>
                        </g>
                      </svg>
                      <p style={{ marginTop: "10px" }}>Log In</p>
                    </div>
                  </div>
                </Nav.Link>
              ) : (
                <>
                  <Link
                    to="/profile"
                    style={{ all: "unset", display: "flex", cursor: "pointer" }}
                  >
                    <div
                      className="text-center mb-1"
                      style={{ marginLeft: "5px" }}
                    >
                      <img
                        src={localStorage.getItem("picUrl")}
                        alt="Logo"
                        className="roundedImg"
                        style={{
                          width: "60px",
                          height: "60px",
                          marginLeft: "30px",
                          boxShadow: "0px 0px 50px #7D26CD",
                        }}
                      />
                    </div>
                    <span style={{ alignContent: "end" }}>
                      <small
                        style={{
                          color: fixedNavBar ? "#1a1a1a" : "white",
                          marginLeft: "5px",
                          marginTop: "10px",
                          fontSize: "20px",
                        }}
                      >
                        {localStorage.getItem("fName")}
                      </small>
                    </span>
                  </Link>
                  <Button
                    style={{ marginLeft: "10px" }}
                    variant="danger"
                    onClick={() => {
                      setMyToken(null);
                      localStorage.removeItem("token");
                      localStorage.removeItem("fName");
                      localStorage.removeItem("picUrl");
                      showAlertLoggedOutSuccess();
                      navigate("/home");
                    }}
                  >
                    Log Out
                  </Button>
                </>
              )}
            </Nav>
          </div>
        </Container>
      </Navbar>
    </>
  );
}

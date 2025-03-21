import { Link, NavLink } from "react-router";
import Container from "react-bootstrap/Container";
import { Button } from "react-bootstrap";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import Logo from "../logo.jpg";
import { useContext } from "react";
import { urlContext } from "../contexts/urlContext";
import Swal from "sweetalert2";
export default function NavBar() {
  const { setMyToken, token,selected,setSelected } = useContext(urlContext);
 
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
    <Navbar  style={{ backgroundColor: "#111111" }}>
      <Container className="d-flex p-0 ">
        <Navbar.Brand
          className="p-0 d-flex flex-column align-items-center"
          as={NavLink}
          to="/home"
        >
          <img
            src={Logo}
            alt=""
            width={200}
            height={100}
            style={{ margin: "0px" }}
          />
          <small className="characters">Drive Freely ..Rent Easly</small>
        </Navbar.Brand>
        <div style={{ marginTop: "20px" }}>
          <Nav className="me-aut d-flex align-items-center">
            <Nav.Link as={NavLink} to="/home" onClick={()=>{setSelected("H")}}>
              <h4
                style={{ color: "#ffd" }}
                className={`${selected==="H" ? "selected" : ""} `}
              >
                Home
              </h4>
            </Nav.Link>
            <Nav.Link as={NavLink} to="/profile" onClick={()=>{setSelected("P")}}>
              <h4
                style={{ color: "#ffd" }}
                className={`${selected==="P" ? "selected" : ""} `}
              >
                My Profile
              </h4>
            </Nav.Link>
            <Nav.Link as={NavLink} to="/" onClick={()=>{setSelected("L1")}}>
              <h4
                style={{ color: "#ffd" }}
                className={`${selected==="L1" ? "selected" : ""} `}
              >
                Link
              </h4>
            </Nav.Link>
            <Nav.Link as={NavLink} to="/" onClick={()=>{setSelected("L2")}}>
              <h4
                style={{ color: "#ffd" }}
                className={`${selected==="L2" ? "selected" : ""} `}
              >
                Link
              </h4>
            </Nav.Link>

            {!token ? (
              <Nav.Link as={NavLink} to="/login" style={{marginLeft:"45px"}} onClick={()=>{setSelected("")}}>
                <h6
                  style={{ color: "#ffd" }}
                >
                  Log In / Sign Up
                </h6>
              </Nav.Link>
            ) : (
              <>
                <Link to="/profile">
                  <div
                    className="text-center mb-1"
                    style={{ marginLeft: "5px" }}
                  >
                    <img
                      src={localStorage.getItem("picUrl")}
                      alt="Logo"
                      className="roundedImg"
                      style={{ width: "60px", height: "60px",marginLeft:"30px" }}
                    />
                  </div>
                </Link>
                <small style={{color:"white",marginLeft:"5px",marginTop:"10px"}}>{localStorage.getItem("fName")}</small>
                <Button
                  style={{ marginLeft: "10px" }}
                  variant="danger"
                  onClick={() => {
                    setMyToken(null);
                    localStorage.removeItem("token");
                    localStorage.removeItem("fName");
                    localStorage.removeItem("picUrl");
                    showAlertLoggedOutSuccess();
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
  );
}

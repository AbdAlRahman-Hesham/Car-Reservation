import { Form, Button, Container, Row, Col } from "react-bootstrap";
import { useContext, useState } from "react";
import { urlContext } from "../contexts/urlContext";
import { useNavigate } from "react-router";
import LogoIcon from "../images/logo2.png"; // Create or use an appropriate admin icon
import { useFormik } from "formik";
import * as Yup from "yup";
import axios from "axios";
import Swal from "sweetalert2";
import { ClipLoader } from "react-spinners";

const AdminLogin = () => {
  const {
    url,
    setMyToken,
    showAlertError,
    loading,
    setMyLoading,
  } = useContext(urlContext);
  const navigate = useNavigate();

  const showWelcomeAlert = (name) => {
    Swal.fire({
      title: `Welcome, Admin ${name}!`,
      text: "You've successfully logged into the admin dashboard.",
      icon: "success",
      confirmButtonColor: "#28a745",
      confirmButtonText: "Continue to Dashboard",
    });
  };

  const myform = useFormik({
    validateOnMount: true,
    initialValues: {
      email: "",
      password: ""
    },
    validationSchema: Yup.object().shape({
      email: Yup.string().required(`Email is required`),
      password: Yup.string()
        .required(`Password is required`)
        .min(7, `Password must be at least 7 characters long`),
    }),
    onSubmit: (values) => {
      loginRequest(values);
      myform.resetForm();
    },
  });

  async function loginRequest(values) {
    console.log(values);
    setMyLoading(true);
    await axios
      .post(`${url}/api/accounts/login`, values)
      .then((res) => {
        // Check if the user has admin role (you might need to adjust this based on your API response)
          localStorage.setItem("token", res.data.token);
          localStorage.setItem("isAdmin", "true");
          setMyToken(res.data.token);
          localStorage.setItem("fName", res.data.fName);
          localStorage.setItem("picUrl", res.data.picUrl);
          showWelcomeAlert(res.data.fName);
          navigate("/dashboard");
      })
      .catch((res) => {
        console.log("soort");
        console.log(res);
        switch (res.response?.status) {
          case 401:
            showAlertError("Wrong Password or Email");
            break;
          case 403:
            showAlertError("You don't have permission to access the admin dashboard");
            break;
          default:
            showAlertError("An error occurred. Please try again.");
            break;
        }
      })
      .finally(() => {
        setMyLoading(false);
      });
  }

  return (
    <Container
      className="d-flex justify-content-center align-items-center"
      style={{ minHeight: "100vh", alignContent: "center", padding: "20px" }}
    >
      <Row>
        <Col md={12}>
          <div
            className="login-box p-4 rounded shadow"
            style={{
              backgroundColor: "#f4f4f4",
              width: "45vw",
              border: "#2c3e50 solid 2px",
            }}
          >
            <h2 className="text-center fw-bold mb-3">Admin Dashboard Login</h2>
            <div className="text-center mb-1">
              <img 
                src={LogoIcon} 
                alt="Admin Logo" 
                className="roundedImg" 
                onError={(e) => {
                  // Fallback in case the admin icon is missing
                  e.target.onerror = null;
                  e.target.src = "https://via.placeholder.com/150?text=Admin";
                }}
              />
            </div>

            <Form onSubmit={myform.handleSubmit}>
              <Form.Group className="mb-3">
                <Form.Label className="fw-bold">Email</Form.Label>
                <Form.Control
                  type="email"
                  name="email"
                  placeholder="Enter admin email"
                  value={myform.values.email}
                  onBlur={myform.handleBlur}
                  onChange={(e) => {
                    myform.handleChange(e);
                    myform.setFieldTouched("email", true, false);
                  }}
                  className={
                    myform.errors.email && myform.touched.email
                      ? `invalid-input`
                      : ``
                  }
                />
                <small
                  style={{
                    color: "red",
                    display: "block",
                    minHeight: "22px",
                    visibility:
                      myform.errors.email && myform.touched.email
                        ? "visible"
                        : "hidden",
                  }}
                >
                  {myform.errors.email}
                </small>
              </Form.Group>

              <Form.Group className="mb-3">
                <Form.Label className="fw-bold">Password</Form.Label>
                <Form.Control
                  type="password"
                  placeholder="Enter admin password"
                  name="password"
                  value={myform.values.password}
                  onBlur={myform.handleBlur}
                  onChange={(e) => {
                    myform.handleChange(e);
                    myform.setFieldTouched("password", true, false);
                  }}
                  className={
                    myform.errors.password && myform.touched.password
                      ? `invalid-input`
                      : ``
                  }
                />
                <small
                  style={{
                    color: "red",
                    display: "block",
                    minHeight: "22px",
                    visibility:
                      myform.errors.password && myform.touched.password
                        ? "visible"
                        : "hidden",
                  }}
                >
                  {myform.errors.password}
                </small>
              </Form.Group>

              <Button
                disabled={!myform.isValid}
                type="submit"
                variant="primary"
                className="w-100 mb-3"
                style={{ backgroundColor: "#2c3e50", borderColor: "#2c3e50" }}
              >
                {loading ? <ClipLoader size={20} color="#fff" /> : "Login as Admin"}
              </Button>
            </Form>

            <div className="text-center mt-3">
              <Button 
                variant="link" 
                onClick={() => navigate("/")} 
                className="text-decoration-none"
              >
                Return to main site
              </Button>
            </div>
          </div>
        </Col>
      </Row>
    </Container>
  );
};

export default AdminLogin;

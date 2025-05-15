import { Form, Button, Container, Row, Col } from "react-bootstrap";
import { useContext, useEffect, useState } from "react";
import { urlContext } from "../contexts/urlContext";
import { Link, useNavigate } from "react-router";
import User from "../images/user.jpg";
import { useFormik } from "formik";
import * as Yup from "yup";
import axios from "axios";
import Swal from "sweetalert2";
import { ClipLoader } from "react-spinners";
const LoginForm = () => {
  const {
    url,
    setMyToken,
    showAlertError,
    loading,
    setMyLoading,
    setSelected,
  } = useContext(urlContext);
  const navigate = useNavigate();
  const [to, setTO] = useState("");
  const showAlert = () => {
    Swal.fire({
      title: `🚀 Welcome back ${localStorage.getItem("fName")} !`,
      text: "Your ride is ready! Let's hit the road!",
      icon: "success",
      confirmButtonColor: "#28a745",
      confirmButtonText: "Let's Go! 🚗",
    });
  };
  const myform = useFormik({
    validateOnMount: true,
    initialValues: {
      email: "",
      password: "",
    },
    validationSchema: Yup.object().shape({
      email: Yup.string().required(`Email Is Required`),
      password: Yup.string()
        .required(`Password IS Rrequired`)
        .min(7, `Password must be at least 7 characters long`),
    }),
    onSubmit: (values) => {
      loginRequest(values);
      myform.resetForm();
    },
  });

  async function loginRequest(values) {
    setMyLoading(true);
    await axios
      .post(`${url}/api/accounts/login`, values)
      .then((res) => {
        console.log(res);
        localStorage.setItem("token", res.data.token);
        setMyToken(res.data.token);
        localStorage.setItem("fName", res.data.fName);
        localStorage.setItem("picUrl", res.data.picUrl);
        showAlert();
        navigate(to);
        setSelected("H");
      })
      .catch((res) => {
        switch (res.response.status) {
          case 401:
            showAlertError("Wrong Password Or Email");
            break;
          case 409:
            showAlertError("The Email Is Already Used");
            break;
          default:
            break;
        }
      })
      .finally(() => {
        setMyLoading(false);
      });
  }
  useEffect(() => {
    if (new URLSearchParams(window.location.search).get("id")) {
      setTO(
        `/reservation/${new URLSearchParams(window.location.search).get("id")}`
      );
    }else{
      setTO("/home")
    }
  }, []);

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
              border: "#5e9dfa solid 2px",
            }}
          >
            <h2 className="text-center fw-bold mb-3">Login</h2>
            <div className="text-center mb-1">
              <img src={User} alt="Logo" className="roundedImg" />
            </div>

            <Form onSubmit={myform.handleSubmit}>
              <Form.Group className="mb-3">
                <Form.Label className="fw-bold">Email</Form.Label>
                <Form.Control
                  type="email"
                  name="email"
                  placeholder="Enter your email"
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
                  placeholder="Enter your password"
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
              >
                {loading ? <ClipLoader size={20} color="#fff" /> : "Login"}
              </Button>
            </Form>

            <div className="text-center mt-3">
              You Don't Have An Account?!
              <Link
                to="/register"
                className="text-decoration-none text-primary"
              >
                <i style={{ marginLeft: "10px", fontSize: "20px" }}>
                  Sign Up →
                </i>
              </Link>
            </div>
            <div className="text-center mt-3">
              OR You could Run As A Guest
              <Link to="/home" className="text-decoration-none text-primary">
                <i style={{ marginLeft: "10px", fontSize: "20px" }}>Home →</i>
              </Link>
            </div>
          </div>
        </Col>
      </Row>
    </Container>
  );
};

export default LoginForm;

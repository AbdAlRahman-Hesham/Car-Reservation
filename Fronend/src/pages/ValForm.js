import React from "react";
import { Container, Form, Button, Row, Col } from "react-bootstrap";
import { Link, useNavigate } from "react-router";
import Logo from "../logo2.jpg";
import { useFormik } from "formik";
import * as Yup from "yup";
import axios from "axios";
import { urlContext } from "../contexts/urlContext";
import { useContext } from "react";
import Swal from "sweetalert2";
import { ClipLoader } from "react-spinners";

export default function Registration() {
  let formData=new FormData()
  const navigate = useNavigate();
  const { url, setMyToken, showAlertError, loading, setMyLoading,setSelected } =
    useContext(urlContext);
  const showAlertRegisterationSuccess = () => {
    Swal.fire({
      title: `🎉 Welcome aboard, ${localStorage.getItem("fName")}!`,
      text: "Your account is ready. Start exploring amazing car rental deals now!",
      icon: "success",
      confirmButtonColor: "#007bff",
      confirmButtonText: "Start Now 🚘",
    });
  };
  const myForm = useFormik({
    validateOnMount: true,
    initialValues: {
      fname: ``,
      lname: ``,
      email: ``,
      phone: ``,
      id: ``,
      pic: ``,
      pass: null,
      pass2: ``,
      street: ``,
      city: ``,
      country: ``,
    },
    validationSchema: Yup.object().shape({
      fname: Yup.string()
        .required("First name is required")
        .min(4, "First name must be lmore than 3 chrs."),
      lname: Yup.string()
        .required(`Last name is required`)
        .min(4, "username must be lmore than 3 chrs."),
      email: Yup.string().required(`Email is required`).email(`Invalid Email`),
      phone: Yup.string()
        .required("Phone number is required")
        .matches(/^01[0-9]{9}$/, "Invalid phone number"),
      id: Yup.string()
        .required("National ID is required")
        .matches(/^\d{14}$/, "National ID must be exactly 14 digits"),
      pic: Yup.mixed()
        .required("Profile picture URL is required"),
      pass: Yup.string()
        .required("Password is required")
        .min(7, "Password must be at least 7 characters long")
        .matches(/[A-Z]/, "Password must contain at least one uppercase letter")
        .matches(/[a-z]/, "Password must contain at least one lowercase letter")
        .matches(/\d/, "Password must contain at least one digit")
        .matches(
          /[@$!%*?&]/,
          "Password must contain at least one special character"
        ),
      pass2: Yup.string()
        .oneOf([Yup.ref("pass"), null], "Passwords must match")
        .required("Confirm password is required"),
      street: Yup.string().required("Street is required"),

      city: Yup.string().required("City is required"),

      country: Yup.string().required("Country is required"),
    }),
    onSubmit: (values) => {
      registerRequest(values);
      myForm.resetForm();
    },
  });

  async function registerRequest(values) {
    formData.append("FName",values.fname)
    formData.append("LName",values.lname)
    formData.append("Picture",values.pic)
    formData.append("NationalId",values.id)
    formData.append("Email",values.email)
    formData.append("PhoneNumber",values.phone)
    formData.append("Password",values.pass)
    formData.append("Address.Street",values.street)
    formData.append("Address.City",values.city)
    formData.append("Address.Country",values.country)
    setMyLoading(true);
    await axios
      .post(`${url}/api/accounts/Register`, formData)
      .then((res) => {
        localStorage.setItem("token", res.data.token);
        setMyToken(res.data.token);
        localStorage.setItem("fName", res.data.fName);
        localStorage.setItem("picUrl", res.data.picUrl);
        navigate("/home");
        setSelected("H")
        showAlertRegisterationSuccess();
      })
      .catch((res) => {
        console.log(res.response);
        switch (res.response.status) {
          case 409:
            showAlertError("The Email Is Already Used");
            break;
            case 400 :
              showAlertError("Some Thing Wrong from Server")
            break;
          default:
            break;
        }
      })
      .finally(() => {
        setMyLoading(false);
      });
  }
  return (
    <div
      className="d-flex justify-content-center align-items-center container col-6"
      style={{ minHeight: "65vh",marginTop:"15px" }}
    >
      <Container
        className="p-3 rounded mainContainer "
        style={{ border: "#5e9dfa solid 2px" }}
      >
        <h4 className="text-center mb-3 text-primary">Creating An Account</h4>
        <div className="text-center mb-1">
          <img src={Logo} alt="Logo" className="roundedImg" />
        </div>

        <Form onSubmit={myForm.handleSubmit}>
          <Row>
            <Col md={6}>
              <Form.Group className="mb-1">
                <Form.Label className="fw-bold">First Name</Form.Label>
                <Form.Control
                  name="fname"
                  value={myForm.values.fname}
                  onChange={(e) => {
                    myForm.handleChange(e);
                    myForm.setFieldTouched("fname", true, false);
                  }}
                  onBlur={myForm.handleBlur}
                  type="text"
                  placeholder="First name"
                  className={`h-35 ${
                    myForm.errors.fname && myForm.touched.fname
                      ? `invalid-input`
                      : ``
                  }`}
                />
                <small
                  style={{
                    color: "red",
                    display: "block",
                    minHeight: "22px",
                    visibility:
                      myForm.errors.fname && myForm.touched.fname
                        ? "visible"
                        : "hidden",
                  }}
                >
                  {myForm.errors.fname}
                </small>
              </Form.Group>
            </Col>
            <Col md={6}>
              <Form.Group className="mb-1">
                <Form.Label className="fw-bold">Last Name</Form.Label>
                <Form.Control
                  name="lname"
                  value={myForm.values.lname}
                  onChange={(e) => {
                    myForm.handleChange(e);
                    myForm.setFieldTouched("lname", true, false);
                  }}
                  onBlur={myForm.handleBlur}
                  type="text"
                  placeholder="Last name"
                  className={`h-35 ${
                    myForm.errors.lname && myForm.touched.lname
                      ? `invalid-input`
                      : ``
                  }`}
                />
                <small
                  style={{
                    color: "red",
                    display: "block",
                    minHeight: "22px",
                    visibility:
                      myForm.errors.lname && myForm.touched.lname
                        ? "visible"
                        : "hidden",
                  }}
                >
                  {myForm.errors.lname}
                </small>
              </Form.Group>
            </Col>
          </Row>

          <Row>
            <Col md={6}>
              <Form.Group className="mb-1">
                <Form.Label className="fw-bold">Email</Form.Label>
                <Form.Control
                  name="email"
                  value={myForm.values.email}
                  onChange={(e) => {
                    myForm.handleChange(e);
                    myForm.setFieldTouched("email", true, false);
                  }}
                  onBlur={myForm.handleBlur}
                  type="email"
                  placeholder="Email"
                  className={`h-35 ${
                    myForm.errors.email && myForm.touched.email
                      ? `invalid-input`
                      : ``
                  }`}
                />
                <small
                  style={{
                    color: "red",
                    display: "block",
                    minHeight: "22px",
                    visibility:
                      myForm.errors.email && myForm.touched.email
                        ? "visible"
                        : "hidden",
                  }}
                >
                  {myForm.errors.email}
                </small>
              </Form.Group>
            </Col>
            <Col md={6}>
              <Form.Group className="mb-1">
                <Form.Label className="fw-bold">Phone Number</Form.Label>
                <Form.Control
                  name="phone"
                  value={myForm.values.phone}
                  onChange={(e) => {
                    myForm.handleChange(e);
                    myForm.setFieldTouched("phone", true, false);
                  }}
                  onBlur={myForm.handleBlur}
                  type="text"
                  placeholder="Phone"
                  className={`h-35 ${
                    myForm.errors.phone && myForm.touched.phone
                      ? `invalid-input`
                      : ``
                  }`}
                />
                <small
                  style={{
                    color: "red",
                    display: "block",
                    minHeight: "22px",
                    visibility:
                      myForm.errors.phone && myForm.touched.phone
                        ? "visible"
                        : "hidden",
                  }}
                >
                  {myForm.errors.phone}
                </small>
              </Form.Group>
            </Col>
          </Row>

          <Row>
            <Col md={6}>
              <Form.Group className="mb-1">
                <Form.Label className="fw-bold">National ID</Form.Label>
                <Form.Control
                  name="id"
                  value={myForm.values.id}
                  onChange={(e) => {
                    myForm.handleChange(e);
                    myForm.setFieldTouched("id", true, false);
                  }}
                  onBlur={myForm.handleBlur}
                  type="text"
                  placeholder="National ID"
                  className={`h-35 ${
                    myForm.errors.id && myForm.touched.id ? `invalid-input` : ``
                  }`}
                />
                <small
                  style={{
                    color: "red",
                    display: "block",
                    minHeight: "22px",
                    visibility:
                      myForm.errors.id && myForm.touched.id
                        ? "visible"
                        : "hidden",
                  }}
                >
                  {myForm.errors.id}
                </small>
              </Form.Group>
            </Col>
            <Col md={6}>
              <Form.Group className="mb-1">
                <Form.Label className="fw-bold">Picture url</Form.Label>
                <Form.Control
                  name="pic"
                  accept="images/*"
                  onChange={(e) => {
                    if (e.target.files[0]) {
                      myForm.setFieldValue("pic",e.target.files[0])
                    }
                    myForm.setFieldTouched("pic", true, false);
                  }}
                  onBlur={myForm.handleBlur}
                  type="file"
                  className={`h-35 ${
                    myForm.errors.pic && myForm.touched.pic
                      ? `invalid-input`
                      : ``
                  }`}
                  placeholder="Profile Picture"
                />
                <small
                  style={{
                    color: "red",
                    display: "block",
                    minHeight: "22px",
                    visibility:
                      myForm.errors.pic && myForm.touched.pic
                        ? "visible"
                        : "hidden",
                  }}
                >
                  {myForm.errors.pic}
                </small>
              </Form.Group>
            </Col>
          </Row>

          <Row>
            <Col md={6}>
              <Form.Group className="mb-1">
                <Form.Label className="fw-bold">Password</Form.Label>
                <Form.Control
                  name="pass"
                  value={myForm.values.pass}
                  onChange={(e) => {
                    myForm.handleChange(e);
                    myForm.setFieldTouched("pass", true, false);
                  }}
                  onBlur={myForm.handleBlur}
                  type="text"
                  placeholder="Password"
                  className={`h-35 ${
                    myForm.errors.pass && myForm.touched.pass
                      ? `invalid-input`
                      : ``
                  }`}
                />
                <small
                  style={{
                    color: "red",
                    display: "block",
                    minHeight: "22px",
                    visibility:
                      myForm.errors.pass && myForm.touched.pass
                        ? "visible"
                        : "hidden",
                  }}
                >
                  {myForm.errors.pass}
                </small>
              </Form.Group>
            </Col>
            <Col md={6}>
              <Form.Group className="mb-1">
                <Form.Label className="fw-bold">Confirm Password</Form.Label>
                <Form.Control
                  name="pass2"
                  value={myForm.values.pass2}
                  onChange={(e) => {
                    myForm.handleChange(e);
                    myForm.setFieldTouched("pass2", true, false);
                  }}
                  onBlur={myForm.handleBlur}
                  type="ptext"
                  placeholder="Confirm"
                  className={`h-35 ${
                    myForm.errors.pass2 && myForm.touched.pass2
                      ? `invalid-input`
                      : ``
                  }`}
                />
                <small
                  style={{
                    color: "red",
                    display: "block",
                    minHeight: "22px",
                    visibility:
                      myForm.errors.pass2 && myForm.touched.pass2
                        ? "visible"
                        : "hidden",
                  }}
                >
                  {myForm.errors.pass2}
                </small>
              </Form.Group>
            </Col>
          </Row>

          <Row>
            <Col md={4}>
              <Form.Group className="mb-1">
                <Form.Label className="fw-bold">Street</Form.Label>
                <Form.Control
                  name="street"
                  value={myForm.values.street}
                  onChange={(e) => {
                    myForm.handleChange(e);
                    myForm.setFieldTouched("street", true, false);
                  }}
                  onBlur={myForm.handleBlur}
                  type="text"
                  placeholder="Street"
                  className={`h-35 ${
                    myForm.errors.street && myForm.touched.street
                      ? `invalid-input`
                      : ``
                  }`}
                />
                <small
                  style={{
                    color: "red",
                    display: "block",
                    minHeight: "22px",
                    visibility:
                      myForm.errors.street && myForm.touched.street
                        ? "visible"
                        : "hidden",
                  }}
                >
                  {myForm.errors.street}
                </small>
              </Form.Group>
            </Col>
            <Col md={4}>
              <Form.Group className="mb-1">
                <Form.Label className="fw-bold">City</Form.Label>
                <Form.Control
                  name="city"
                  value={myForm.values.city}
                  onChange={(e) => {
                    myForm.handleChange(e);
                    myForm.setFieldTouched("city", true, false);
                  }}
                  onBlur={myForm.handleBlur}
                  type="text"
                  placeholder="City"
                  className={`h-35 ${
                    myForm.errors.city && myForm.touched.city
                      ? `invalid-input`
                      : ``
                  }`}
                />
                <small
                  style={{
                    color: "red",
                    display: "block",
                    minHeight: "22px",
                    visibility:
                      myForm.errors.city && myForm.touched.city
                        ? "visible"
                        : "hidden",
                  }}
                >
                  {myForm.errors.city}
                </small>
              </Form.Group>
            </Col>
            <Col md={4}>
              <Form.Group className="mb-1">
                <Form.Label className="fw-bold">Country</Form.Label>
                <Form.Control
                  name="country"
                  value={myForm.values.country}
                  onChange={(e) => {
                    myForm.handleChange(e);
                    myForm.setFieldTouched("country", true, false);
                  }}
                  onBlur={myForm.handleBlur}
                  type="text"
                  placeholder="Country"
                  className={`h-35 ${
                    myForm.errors.country && myForm.touched.country
                      ? `invalid-input`
                      : ``
                  }`}
                />
                <small
                  style={{
                    color: "red",
                    display: "block",
                    minHeight: "22px",
                    visibility:
                      myForm.errors.country && myForm.touched.country
                        ? "visible"
                        : "hidden",
                  }}
                >
                  {myForm.errors.country}
                </small>
              </Form.Group>
            </Col>
          </Row>

          <Button
            variant="primary"
            type="submit"
            className={`w-100 mt-2 ${!myForm.isValid ? "btn-disabled" : ""}`}
            disabled={!myForm.isValid}
          >
            {loading ? <ClipLoader size={20} color="#fff" /> : "Submit"}
          </Button>

          <div className="text-center mt-3">
            Do you have an account?!
            <Link to="/login" className="text-decoration-none text-primary">
              <i style={{ marginLeft: "10px", fontSize: "20px" }}>Log in →</i>
            </Link>
          </div>
        </Form>
      </Container>
    </div>
  );
}

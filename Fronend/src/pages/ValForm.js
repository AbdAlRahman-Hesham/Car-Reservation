import React from "react";
import { Container, Form, Button, Row } from "react-bootstrap";
import { Link, useNavigate } from "react-router";
import Logo from "../images/logo2.png";
import { useFormik } from "formik";
import axios from "axios";
import { urlContext } from "../contexts/urlContext";
import { useContext } from "react";
import Swal from "sweetalert2";
import { ClipLoader } from "react-spinners";
import Inputfield from "../componenets/InputField";
import {Myschema} from "../ValSchema"

export default function Registration() {
  
  let formData = new FormData();
  const navigate = useNavigate();
  const {
    url,
    setMyToken,
    showAlertError,
    loading,
    setMyLoading,
    setSelected,
  } = useContext(urlContext);
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
    validationSchema: Myschema,
    onSubmit: (values) => {
      registerRequest(values);
      myForm.resetForm();
    },
  });

  async function registerRequest(values) {
    formData.append("FName", values.fname);
    formData.append("LName", values.lname);
    formData.append("Picture", values.pic);
    formData.append("NationalId", values.id);
    formData.append("Email", values.email);
    formData.append("PhoneNumber", values.phone);
    formData.append("Password", values.pass);
    formData.append("Address.Street", values.street);
    formData.append("Address.City", values.city);
    formData.append("Address.Country", values.country);
    setMyLoading(true);
    await axios
      .post(`${url}/api/accounts/Register`, formData)
      .then((res) => {
        localStorage.setItem("token", res.data.token);
        setMyToken(res.data.token);
        localStorage.setItem("fName", res.data.fName);
        localStorage.setItem("picUrl", res.data.picUrl);
        navigate("/home");
        setSelected("H");
        showAlertRegisterationSuccess();
      })
      .catch((res) => {
        console.log(res.response);
        switch (res.response.status) {
          case 409:
            showAlertError("The Email Is Already Used");
            break;
          case 400:
            showAlertError("Some Thing Wrong from Server");
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
      style={{ minHeight: "65vh", padding: "20px",height:"100vh" }}
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
            <Inputfield
              label={"First Name"}
              placeholder={"First name"}
              formik={myForm}
              name="fname"
            />
            <Inputfield
              label={"Last Name"}
              placeholder={"Last name"}
              formik={myForm}
              name="lname"
            />
          </Row>

          <Row>
            <Inputfield
              label={"Email"}
              placeholder={"email"}
              formik={myForm}
              name="email"
            />
            <Inputfield
              label={"Phone Number"}
              placeholder={"Phone number"}
              formik={myForm}
              name="phone"
            />
          </Row>

          <Row>
            <Inputfield
              label={"National ID"}
              placeholder={"Id number"}
              formik={myForm}
              name="id"
            />
             <Inputfield
              label={"Profile Picture"}
              placeholder={"Picture"}
              formik={myForm}
              name="pic"
            />
          </Row>

          <Row>
            <Inputfield
              label={"Password"}
              placeholder={"password"}
              formik={myForm}
              name="pass"
            />
            <Inputfield
              label={"Confirm Password"}
              placeholder={"password"}
              formik={myForm}
              name="pass2"
            />
          </Row>
          <Row>
            <Inputfield
              label={"Street"}
              placeholder={"Street"}
              formik={myForm}
              name="street"
            />

            <Inputfield
              label={"City"}
              placeholder={"city"}
              formik={myForm}
              name="city"
            />

            <Inputfield
              label={"Country"}
              placeholder={"country"}
              formik={myForm}
              name="country"
            />
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

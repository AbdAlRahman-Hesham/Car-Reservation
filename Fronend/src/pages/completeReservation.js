import { useParams } from "react-router";
import { Container } from "@mui/material";
import Stepper from "@mui/material/Stepper";
import Step from "@mui/material/Step";
import StepLabel from "@mui/material/StepLabel";
import Logo from "../images/logo2.png";
import DateSelectionContent from "../componenets/ReservationStepsComponenets/SelectDateStep";
import DateConfirmation from "../componenets/ReservationStepsComponenets/ConfirmBookingStep";
import PayemtContent from "../componenets/ReservationStepsComponenets/payment";
import Success from "../componenets/ReservationStepsComponenets/SuccessfullyPayed";

import { useEffect, useState } from "react";
import { useContext } from "react";
import { urlContext } from "../contexts/urlContext";
import axios from "axios";
const steps = [
  "Select Date",
  "Confirm Your Booking",
  "Payment",
  "Booking Completed Successfully",
];
export default function Reservation() {
  const { showAlertError, url } = useContext(urlContext);
  const [carDetails, setCarDetails] = useState({});
  const [activeStep, setActiveStep] = useState(0);
  const [chossenDates, setChoosenDates] = useState({});
  const [resID, setResID] = useState();
  const [loading, setLoading] = useState(false);
  const { id } = useParams();
  const baseUrl = process.env.REACT_APP_BASE_URL;
  const finalUrl = `${baseUrl}/reservation/18?step=3`;

  useEffect(() => {
    if (new URLSearchParams(window.location.search).get("step")) {
      setActiveStep(3);
      setChoosenDates(JSON.parse(localStorage.getItem("dates")));
      setCarDetails(JSON.parse(localStorage.getItem("car")));
      setResID(localStorage.getItem("resId"));
    } else {
      axios
        .get(`${url}/api/Car/${id}`)
        .then((res) => {
          setCarDetails(res.data);
        })
        .catch((err) => {
          showAlertError(
            "Something Went Wrong While trying To Compelte Your Reservation"
          );
        });
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  function confirmDate(dates, ResID) {
    setActiveStep(1);
    setChoosenDates(dates);
    setResID(ResID);
  }
  function confirmBooking() {
    setActiveStep(2);
  }
  function stepBack() {
    setActiveStep(activeStep - 1);
  }
  function toProceedClicked() {
    setLoading(true)
    const headers = {
      authorization: `Bearer ${localStorage.getItem("token")}`,
    };
    axios
      .post(
        `${url}/api/Payment/create-checkout-session`,
        {
          reservationId: resID,
          successUrl: finalUrl,
          cancelUrl: "http://example.com/cancel",
        },
        { headers: headers }
      )
      .then((res) => {
        //   console.log(res.data.checkoutSessionUrl);
        window.location.assign(res.data.checkoutSessionUrl);
        localStorage.setItem("dates", JSON.stringify(chossenDates));
        localStorage.setItem("car", JSON.stringify(carDetails));
        localStorage.setItem("resId", resID);
      })
      .catch(() => {
        showAlertError(
          "Something Went Wrong While Trying To Redirect You To Strip Gateway"
        );
      })
  }
  return (
    <div style={{ backgroundColor: "rgba(179, 175, 175, 0.36)" }}>
      <Container
        maxWidth="lg"
        style={{ alignContent: "center", minHeight: "100vh" }}
      >
        <div className="text-center mb-1">
          <img src={Logo} alt="Logo" className="roundedImg" />
        </div>
        <h2>Complete Reservation</h2>
        <div className="reservationCard">
          <Stepper style={{ margin: "20px" }} activeStep={activeStep}>
            {steps.map((label) => (
              <Step key={label}>
                <StepLabel>{label}</StepLabel>
              </Step>
            ))}
          </Stepper>

          {activeStep === 0 && (
            <DateSelectionContent
              step={activeStep}
              confirmDate={confirmDate}
              refilldates={chossenDates}
              id={id}
            />
          )}
          {activeStep === 1 && (
            <DateConfirmation
              dates={chossenDates}
              car={carDetails}
              stepBack={stepBack}
              confirmBooking={confirmBooking}
            />
          )}
          {activeStep === 2 && (
            <PayemtContent
              resId={resID}
              loading={loading}
              toProceedClicked={toProceedClicked}
            />
          )}
          {activeStep === 3 && (
            <Success car={carDetails} ResID={resID} dates={chossenDates} />
          )}
        </div>
      </Container>
    </div>
  );
}

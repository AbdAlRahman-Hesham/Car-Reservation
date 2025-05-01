import Alert from "@mui/material/Alert";
import ArrowForwardIcon from "@mui/icons-material/ArrowForward";
import PaymentIcon from "@mui/icons-material/Payment";
// import { useEffect } from "react";
import axios from "axios";
import { urlContext } from "../../contexts/urlContext";
import { useContext, useState } from "react";
import { ClipLoader } from "react-spinners";
export default function PayemtContent({ resId, toProceedClicked,loading }) {
  return (
    <div style={{ margin: "20px" }}>
      <h2>Complete Your Payment</h2>
      <hr />
      <div
        style={{
          display: "flex",
          flexFlow: "column",
          justifyContent: "space-between",
          alignItems: "center",
        }}
      >
        <Alert severity="error">
          Warning: Your reservation will be automatically canceled in 1 hour if
          you do not confirm and complete the payment. Please take action to
          secure your booking.
        </Alert>
        <PaymentIcon style={{ fontSize: "150px", color: "#0d6efd " }} />
        <div className="proceed">
          {loading ? (
            <ClipLoader />
          ) : (
            <h3 onClick={toProceedClicked}>
              Proceed To Payment
              <ArrowForwardIcon style={{ fontSize: "40px" }} />
            </h3>
          )}
        </div>
        <p>You Will Be Redirected To Stripe Payment Gateway</p>
        <Alert
          severity="warning"
          style={{ width: "100%", textAlign: "center" }}
        >
          Please Check Your Email After Payment Completion For Confirmation
        </Alert>
      </div>
    </div>
  );
}

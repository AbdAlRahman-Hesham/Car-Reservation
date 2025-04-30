import { motion } from "framer-motion";
import { Button } from "@mui/material";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";
import dayjs from "dayjs";
export default function DateConfirmation({
  dates,
  car,
  confirmBooking,
  stepBack,
}) {
  const numberOfDays = Math.ceil(
    dayjs(dates.end).diff(dayjs(dates.start), "hour") / 24
  );
  return (
    <motion.div
      initial={{ x: -50, opacity: 0 }}
      animate={{ x: 0, opacity: 1 }}
      transition={{ duration: 1 }}
      style={{ padding: "40px", marginTop: "20px" }}
    >
      <h4>Booking Confirmation</h4>
      <hr />
      <div style={{ backgroundColor: "rgba(102, 99, 99, 0.11)",borderRadius:"20px",padding:"10px" }}>
        <div
          className="d-flex"
          style={{
            textAlign: "start",
            justifyContent: "space-between",
            alignItems: "center",
            padding: "10px",
          }}
        >
          <h5 style={{ fontWeight: "bold" }}>Car</h5>
          <h3>
            {car.brand}-{car.model}
          </h3>
        </div>
        <hr style={{ margin: "0px" }} />
        <div
          className="d-flex"
          style={{
            textAlign: "start",
            justifyContent: "space-between",
            alignItems: "center",
            padding: "10px",
          }}
        >
          <h5 style={{ fontWeight: "bold" }}>Pick-Up Date</h5>
          <h6>{dayjs(dates.start).format("dddd, D MMMM YYYY hh:mm A")}</h6>
        </div>
        <hr style={{ margin: "0px" }} />
        <div
          className="d-flex"
          style={{
            textAlign: "start",
            justifyContent: "space-between",
            alignItems: "center",
            padding: "10px",
          }}
        >
          <h5 style={{ fontWeight: "bold" }}>Drop-Off Date</h5>
          <h6>{dayjs(dates.end).format("dddd, D MMMM YYYY hh:mm A")}</h6>
        </div>
        <hr style={{ margin: "0px" }} />
        <div
          className="d-flex"
          style={{
            textAlign: "start",
            justifyContent: "space-between",
            alignItems: "center",
            padding: "10px",
          }}
        >
          <h5 style={{ fontWeight: "bold" }}>Total Cost</h5>
          <h4 style={{ color: "green" }}>
            {numberOfDays * Number(car.insuranceCost) + Number(car.price)}$
          </h4>
        </div>
      </div>
      <div className="actions" style={{ textAlign: "end" }}>
        <Button
          onClick={() => {
            stepBack();
          }}
          color="primary"
        >
          {" "}
          <ArrowBackIcon /> Back
        </Button>
        <Button
          variant="contained"
          color="success"
          onClick={() => {
            confirmBooking();
          }}
        >
          To Payment
        </Button>
      </div>
    </motion.div>
  );
}

import { Container } from "@mui/material";
import { Button } from "@mui/material";
import CheckCircleIcon from "@mui/icons-material/CheckCircle";
import MarkEmailReadIcon from "@mui/icons-material/MarkEmailRead";
import dayjs from "dayjs";
import { motion } from "framer-motion";
import { useNavigate } from "react-router";
export default function Success({ car, ResID, dates }) {
  const navigate = useNavigate();
  console.log(car.brand);
  function clean() {
    navigate("/cars");
    localStorage.removeItem("resId");
    localStorage.removeItem("car");
    localStorage.removeItem("dates");
    localStorage.removeItem("step");
  }
  return (
    <motion.div
      initial={{ scale: 0.3, opacity: 0 }}
      animate={{ scale: 1, opacity: 1 }}
      transition={{ duration: 1 }}
    >
      <Container
        maxWidth={"sm"}
        style={{
          borderRadius: "20px",
          backgroundColor: "rgba(158, 158, 158, 0.39)",
          minHeight: "100%",
          padding: "10px",
        }}
      >
        <CheckCircleIcon style={{ fontSize: "150px", color: "green" }} />
        <h2 style={{ marginBottom: "30px" }}>Completed Successfully</h2>
        <h6>
          Check Your Email <MarkEmailReadIcon />{" "}
        </h6>
        <p>We've Sent A Confirmation To Your Email Address</p>
        <hr style={{ margin: "10px" }} />
        <div
          className="d-flex"
          style={{
            textAlign: "start",
            justifyContent: "space-between",
            alignItems: "center",
            padding: "10px",
          }}
        >
          <h5 style={{ fontWeight: "bold" }}>Reservation ID </h5>
          <h5>{ResID}</h5>
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
          <h5 style={{ fontWeight: "bold" }}>Car</h5>
          <h4>
            {car.brand}-{car.model}
          </h4>
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
          <h5>{dayjs(dates.start).format("dddd, D MMMM YYYY hh:mm A")}</h5>
        </div>
      </Container>
      <Button style={{ marginTop: "20px" }} onClick={clean}>
        {" "}
        Back To Car Section And Browse More Cars
      </Button>
    </motion.div>
  );
}

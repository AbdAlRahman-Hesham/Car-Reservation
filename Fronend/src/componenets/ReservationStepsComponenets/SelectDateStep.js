import DateRangeIcon from "@mui/icons-material/DateRange";
import { motion } from "framer-motion";
import { useEffect, useState } from "react";
import { ClipLoader } from "react-spinners";
import { Button } from "@mui/material";
import dayjs from "dayjs";
import { useContext } from "react";
import { urlContext } from "../../contexts/urlContext";
import { useNavigate } from "react-router";
import Swal from "sweetalert2";
import axios from "axios";

export default function DateSelectionContent({ confirmDate, refilldates, id }) {
  const { showAlertError, url } = useContext(urlContext);
  const navigate = useNavigate();
  const [valid, setValid] = useState(true);
  const [loading, setLoading] = useState(false);
  const [dates, setDates] = useState({
    start: dayjs().format("YYYY-MM-DDTHH:mm"),
    end: dayjs().add(1, "day").format("YYYY-MM-DDTHH:mm"),
  });
  useEffect(() => {
    if (refilldates.start && refilldates.end) {
      setDates(refilldates);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  const carIsBusy = () => {
    Swal.fire({
      title: "⚠️ Reservation Failed",
      text: "Sorry, the car is already reserved at the selected time. Please choose a different time slot.",
      icon: "error",
      confirmButtonColor: "#dc3545",
      confirmButtonText: "Select Another Date",
    });
  };
  function createReservation() {
    const headers = {
      authorization: `Bearer ${localStorage.getItem("token")}`,
    };
    console.log(headers);
    setLoading(true);
    console.log(dayjs(dates.start).format("YYYY-MM-DD HH:mm"));
    axios
      .post(
        `${url}/api/Reservation?CarId=${id}&StartDate=${dayjs(
          dates.start
        ).format("YYYY-MM-DD HH:mm")}&EndDate=${dayjs(dates.end).format(
          "YYYY-MM-DD HH:mm"
        )}`,
        {},
        { headers: headers }
      )
      .then((res) => {
        console.log(res.data.id);
        confirmDate(dates, res.data.id);
      })
      .catch((err) => {
        console.log(err);
        if (err.status === 400) {
          carIsBusy();
        } else {
          showAlertError("Something Went Wrong Please Try Again");
        }
      })
      .finally(() => {
        setLoading(false);
      });
  }
  function DropOffDateandcheckValidation(D) {
    setDates({
      ...dates,
      end: dayjs(D).format("YYYY-MM-DDTHH:mm"),
    });
    setValid(dayjs(dates.start).add(23, "hour").isBefore(dayjs(D)));
  }
  return (
    <motion.div
      initial={{ x: -50, opacity: 0 }}
      animate={{ x: 0, opacity: 1 }}
      transition={{ duration: 1 }}
      style={{ padding: "10px", marginTop: "20px" }}
    >
      <h4> Pick The Dates For Your Rental</h4>
      <hr />
      <div
        style={{
          textAlign: "start",
          marginTop: "20px",
          alignContent: "center",
          height: "100%",
        }}
      >
        <label
          style={{
            fontWeight: "bold",
            fontSize: "20px",
            marginBottom: "5px",
            display: "block",
          }}
        >
          Pick-Up Date <DateRangeIcon />
        </label>
        <input
          className="reservationInput"
          type="datetime-local"
          value={dates.start}
          onChange={(e) => {
            setDates({
              start: dayjs(e.target.value).format("YYYY-MM-DDTHH:mm"),
              end: dayjs(e.target.value)
                .add(1, "day")
                .format("YYYY-MM-DDTHH:mm"),
            });
          }}
          min={dates.start}
        />
        <label
          style={{
            fontWeight: "bold",
            fontSize: "20px",
            marginBottom: "5px",
            display: "block",
          }}
        >
          Drop-Off Date <DateRangeIcon />
        </label>
        <input
          style={{
            border: `${
              !valid ? "2px red solid" : "3px solid rgba(14, 20, 72, 0.61)"
            } `,
            marginBottom: "0px",
          }}
          className="reservationInput"
          type="datetime-local"
          min={dayjs(dates.start).format("YYYY-MM-DDTHH:mm")}
          value={dates.end}
          onChange={(e) => {
            DropOffDateandcheckValidation(e.target.value);
          }}
        />

        <p style={{ color: "red", opacity: `${!valid ? "1" : "0"}` }}>
          Drop-off Date Should Be At Least 24 Hours from The Pick Up Date
        </p>

        <div className="actions">
          <Button
            color="error"
            onClick={() => {
              navigate("/cars");
            }}
          >
            Cancel operation
          </Button>

          <Button
            disabled={!valid}
            style={{ width: "150px" }}
            variant="contained"
            color="success"
            onClick={() => {
              createReservation();
              setLoading(true);
            }}
          >
            {loading ? <ClipLoader color="white" size={20} /> : "Confirm Date"}
          </Button>
        </div>
      </div>
    </motion.div>
  );
}

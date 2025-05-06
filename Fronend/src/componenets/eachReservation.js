import Grid from "@mui/material/Grid";
import { useEffect, useState } from "react";
export default function Operation({ info }) {
  const [className, setClassName] = useState("");
  useEffect(() => {
    if (info) {
      switch (info.status) {
        case "Pending":
          setClassName("warning");
          break;
        case "Confirmed":
          setClassName("success");
          break;
        case "Cancelled":
          setClassName("danger");
          break;
        default:
            setClassName("failed");
          break;
      }
    }
  }, [info]);

  return (
    <div
      className={className}
      style={{ padding: "20px", margin: "15px", borderRadius: "18px" }}
    >
      <Grid container spacing={2}>
        <Grid size={3}>
          <b>Res ID : {info.id}</b>
        </Grid>
        <Grid size={3}>
          <b>Start Date :</b> {info.startDate}
        </Grid>
        <Grid size={3}>
          <b>End Date :</b> {info.endDate}
        </Grid>
        <Grid size={3}>
          <b>State : {info.status}</b>{" "}
        </Grid>
      </Grid>
      {className === "warning" && (
        <span>
          <b>⚠️Warning</b> :BE Aware That This Reservation Will BE Canceled If
          You Did Not Confirm Reservation
        </span>
      )}
    </div>
  );
}

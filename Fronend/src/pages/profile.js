import { Container } from "@mui/material";
import img from "../images/logo_with_padding-removebg-preview.png";
import Checkbox from "@mui/material";
import { useEffect, useState } from "react";
import { urlContext } from "../contexts/urlContext";
import { useContext } from "react";
import Operation from "../componenets/eachReservation";
import { FadeLoader } from "react-spinners";
import axios from "axios";

const checkBoxLabel = { inputProps: { "aria-label": "Checkbox demo" } };

export default function Profile() {
  const [userInfo, setUserInfo] = useState({});
  const [loading, setLoading] = useState(true);
  const [reservations, setReservations] = useState([]);
  const { url } = useContext(urlContext);
  useEffect(() => {
    const headers = {
      authorization: `Bearer ${localStorage.getItem("token")}`,
    };

    axios
      .get(`${url}/api/accounts`, {
        headers: headers,
      })
      .then((res) => {
        setUserInfo(res.data);
        // console.log(res.data);
      });
    axios
      .get(`${url}/api/Reservation`, {
        headers: headers,
      })
      .then((res) => {
        // console.log(res.data);
        setReservations(res.data);
      });
  }, []);
  return (
    <div style={{ minHeight: "100vh", margin: "0px !important" }}>
      <Container style={{ marginTop: "0px !important" }}>
        <div>
          <img
            src={img}
            alt=""
            style={{
              width: "100%",
              height: "300px",
              borderBottomLeftRadius: "50px",
              borderBottomRightRadius: "50px",
            }}
          />
          <div style={{ position: "relative", bottom: "80px" }}>
            <img
              src={userInfo.picUrl}
              alt="UserImage"
              className="profilePageImage"
            />
            <h3>
              {userInfo.fName} {userInfo.lName}
            </h3>
          </div>
        </div>
        <h1 style={{ textAlign: "start" }}>Your Reservations</h1>
        <hr />{" "}
        {!loading ? (
          <div
            className="d-flex justify-content-center"
            style={{ minHeight: "400px", alignItems: "center" }}
          >
            <FadeLoader />
          </div>
        ) : (
          <div className="classification">
            {reservations.map((op) => {
              return <Operation info={op} />;
            })}

            {/* <Checkbox
            {...checkBoxLabel}
            checked={checkList.all}
            onChange={(e) => {
                setCheckList({all:true,canceled:false,pending:false});
            //   getOnlyAvailable(e.target.checked);
            }}
          />
          <h6 style={{ margin: 0 }}>Only Available Cars</h6> */}
          </div>
        )}
      </Container>
    </div>
  );
}

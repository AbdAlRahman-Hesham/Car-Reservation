import AboutUsComponent from "../componenets/aboutUsComponent";
import Marquee from "react-fast-marquee";
import { useEffect } from "react";
import StarIcon from "@mui/icons-material/Star";
import { motion } from "framer-motion";
export default function AboutUsPage() {
  useEffect(() => {
    window.scrollTo(0, 0);
  }, []);
  return (
    <>
      <div style={{ position: "relative", margin: "0px" }}>
        {/*for wrapping */}
        <div className="AbooutUsBackground"></div>
        <div
          style={{
            position: "relative",
            zIndex: "2",
            alignContent: "center",
            minHeight: "50vh",
          }}
        >
          <h1
            style={{
              fontSize: "100px",
              color: "white",
              letterSpacing: "8px",
              textShadow: "0px 0px 10px black",
            }}
          >
            About US
          </h1>
        </div>
      </div>
      <AboutUsComponent />
      <hr />
      <motion.div
        initial={{ y: "30px" }}
        animate={{ y: "-30px" }}
        transition={{
          repeat: Infinity,
          repeatType: "reverse",
          duration: 2,
          ease: "linear",
        }}
        style={{
          backgroundColor: "#7d26cd",
          color: "white",
          fontFamily: "Playfair Display",
          fontSize: "30px",
          width: "30%",
          margin: "50px auto",
          padding: "20px",
          borderRadius: "30px",
          boxShadow: "0px 0px 30px #7d26cd",
        }}
      >
        <StarIcon /> Our Humble Team <StarIcon />
      </motion.div>
      <Marquee speed={50} gradient={false} direction="left">
        <div
          style={{
            display: "flex",
            flexFlow: "column",
            justifyContent: "center",
            alignItems: "center",
            marginRight: "30px",
          }}
        >
          <h3>Abdallah Nasser</h3>{" "}
        </div>
        <div
          style={{
            display: "flex",
            flexFlow: "column",
            justifyContent: "center",
            alignItems: "center",
            marginRight: "30px",
          }}
        >
          <h3>Abd-ElRahman Hesham</h3>{" "}
        </div>
        <div
          style={{
            display: "flex",
            flexFlow: "column",
            justifyContent: "center",
            alignItems: "center",
            marginRight: "30px",
          }}
        >
          <h3>Ahmad Sanad</h3>{" "}
        </div>
        <h3 style={{ marginRight: "30px" }}>Omar Dawood</h3>
        <h3 style={{ marginRight: "30px" }}>Omar Wagih</h3>
        <h3 style={{ marginRight: "30px" }}>Ahmad Asal</h3>
        <h3 style={{ marginRight: "30px" }}>Ali Abd-ElReheem</h3>
      </Marquee>
    </>
  );
}

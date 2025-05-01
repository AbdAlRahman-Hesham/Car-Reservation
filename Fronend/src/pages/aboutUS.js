import AboutUsComponent from "../componenets/aboutUsComponent";
import { useEffect } from "react";
export default function AboutUsPage(params) {
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
      <AboutUsComponent/>
 
    </>
  );
}

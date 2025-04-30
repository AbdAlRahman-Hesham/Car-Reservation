import * as React from "react";
import Accordion from "@mui/material/Accordion";
import AccordionActions from "@mui/material/AccordionActions";
import AccordionSummary from "@mui/material/AccordionSummary";
import AccordionDetails from "@mui/material/AccordionDetails";
import Typography from "@mui/material/Typography";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import Button from "@mui/material/Button";
import { motion } from "framer-motion";

export default function Questions() {
  return (
    <motion.div
      initial={{ x: -200, opacity: 0 }}
      transition={{ duration: 1 }}
      whileInView={{ x: 0, opacity: 1 }}
      viewport={{ once: false }}
      style={{ margin: "100px", minHeight: "380px",marginBottom:"10px"}}
    >
      <h2
        style={{
          fontFamily: `Playfair Display,serif`,
          fontSize: "40px",
          wordSpacing: "10px",
          marginBottom: "40px",
        }}
      >
        Have Any Questions?
      </h2>
      <Accordion
        sx={{ backgroundColor: "transparent", boxShadow: "none" }}
        slotProps={{ transition: { timeout: 700 } }}
      >
        <AccordionSummary
          sx={{ borderBottom: "1px solid rgb(63, 31, 94)" }}
          expandIcon={
            <ExpandMoreIcon
              sx={{ borderRadius: "25%" }}
              className="purpleButtton"
            />
          }
          aria-controls="panel1-content"
          id="panel1-header"
        >
          <Typography sx={{ fontSize: "25px" }} component="span">
            What types of cars are available for selection?
          </Typography>
        </AccordionSummary>
        <AccordionDetails
          sx={{ textAlign: "start", color: "rgb(89, 41, 134)" }}
        >
          We offer a wide range of vehicles to meet your needs — whether you're
          looking for an economical car, a family vehicle, or a luxury model.
          You can easily choose the right car through our website or by
          contacting our support team.
        </AccordionDetails>
      </Accordion>
      <Accordion
        sx={{ backgroundColor: "transparent", boxShadow: "none" }}
        slotProps={{ transition: { timeout: 700 } }}
      >
        <AccordionSummary
          sx={{ borderBottom: "1px solid black" }}
          expandIcon={
            <ExpandMoreIcon
              sx={{ borderRadius: "25%" }}
              className="purpleButtton"
            />
          }
          aria-controls="panel1-content"
          id="panel1-header"
        >
          <Typography sx={{ fontSize: "25px" }} component="span">
            Is full insurance included with the rental car?
          </Typography>
        </AccordionSummary>
        <AccordionDetails
          sx={{ textAlign: "start", color: "rgb(89, 41, 134)" }}
        >
          Yes, all our rental cars come with comprehensive insurance coverage,
          and you also have the option to add extra insurance packages based on
          your needs..
        </AccordionDetails>
      </Accordion>
    </motion.div>
  );
}

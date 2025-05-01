import UncontrolledExample from "../componenets/slider";
import Brands from "../componenets/ScrollingBrands";
import { useEffect, useState } from "react";
import CardsSlider from "../componenets/cardSlider";
import { useContext } from "react";
import { urlContext } from "../contexts/urlContext";
import AboutUsComponent from "../componenets/aboutUsComponent";
import { motion } from "framer-motion";
import Questions from "../componenets/accordion";
import { Container } from "@mui/material";

export default function MainLayOut() {
  useEffect(() => {
    window.scrollTo(0, 0);
  }, []);
  return (
    <>
      <UncontrolledExample />
      <motion.div
        initial={{ x: -200, opacity: 0 }}
        animate={{ x: 0, opacity: 1 }}
        transition={{ duration: 1 }}
      >
        <CardsSlider />
        <AboutUsComponent />
        <Container maxWidth="xl">
          <Questions />
        </Container>
      </motion.div>
      <Brands />
    </>
  );
}

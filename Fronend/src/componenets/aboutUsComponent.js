import { motion } from "framer-motion";
import { useState } from "react";
import img from "../images/forTopics.jpg";
const topics = [
  `Speedo is your ultimate destination for all your car rental needs.
Whether you're planning a road trip, need a car for a special occasion, or simply want to cruise around the city in style, we have everything you need.
With our wide range of vehicles, competitive prices, and high-quality customer service, Speedo strives to make your car rental experience easy and hassle-free.
Our team of friendly experts is here to assist you every step of the way.`,
  `Always striving for Speedo to be among the distinguished and leading companies in the field of car rental in all of EGYPT. We also aspire in the future to reach the peak, achieving the highest levels of quality and transparency for our clients from individuals, private sector, and government entities.`,
  `Speedo is committed to providing the best car rental services within EGYPT.
We believe that traveling should be an unforgettable experience, and renting a car is a key element in that journey.
Therefore, the company offers top-quality services relying on the latest models, ensuring the highest levels of comfort and safety.`,
];
export default function AboutUsComponent() {
  const [selectedButton, setSelectedButton] = useState("topic1");
  const [selectedTopic, setSelectedTopic] = useState(0);
  return (
    <>
      <motion.div
        initial={{ x: -200, opacity: 0 }}
        transition={{ duration: 1 }}
        whileInView={{ x: 0, opacity: 1 }}
        viewport={{ once: false }}
        className="tabs"
      >
        <div className="contentOfTopics">
          <div>
            <h2>SPEEDO</h2>
            <p style={{ color: "black" }}>For Car Reservation</p>
          </div>
          <div className="Buttons">
            <motion.button
              whileHover={{ scale: 1.2 }}
              whileTap={{ scale: 0.8 }}
              className={selectedButton === "topic1" ? "choosen" : "unChoosen"}
              onClick={() => {
                setSelectedButton("topic1");
                setSelectedTopic(0);
              }}
            >
              Who Are We
            </motion.button>
            <motion.button
              whileHover={{ scale: 1.2 }}
              whileTap={{ scale: 0.8 }}
              className={selectedButton === "topic2" ? "choosen" : "unChoosen"}
              onClick={() => {
                setSelectedButton("topic2");
                setSelectedTopic(1);
              }}
            >
              Our Vision
            </motion.button>
            <motion.button
              whileHover={{ scale: 1.2 }}
              whileTap={{ scale: 0.8 }}
              className={selectedButton === "topic3" ? "choosen" : "unChoosen"}
              onClick={() => {
                setSelectedButton("topic3");
                setSelectedTopic(2);
              }}
            >
              Our Mission
            </motion.button>
          </div>
          <motion.p
            key={selectedTopic}
            initial={{ x: -200, opacity: 0 }}
            animate={{ x: 0, opacity: 1 }}
            transition={{ duration: 2 }}
          >
            {topics[selectedTopic]}
          </motion.p>
        </div>
        <img height={"350px"} src={img} alt="" />
      </motion.div>
    </>
  );
}

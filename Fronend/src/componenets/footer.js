import React from "react";

export default function Footer() {
  return (
    <footer style={styles.footer}>
      <div style={styles.text}>
        <p>Car Rental @2025 | All Rights Not Reserved |</p>
        <h3 style={styles.name}>Team 7</h3>
      </div>
    </footer>
  );
}
const styles = {
  footer: {
    backgroundColor: "#111111",
    height: "auto",
    color: "white",
    borderTopLeftRadius:"50px",
    borderTopRightRadius:"50px",
    textAlign: "center",
    padding: "10px",
    marginTop: "50px",
    fontSize: "16px",
    display: "flex",
    justifyContent: "space-between",
    alignItems: "center",
    borderTop: "2px solid #444",
 
  },
  text: {
    margin: 0,
    flex: 1,
  },
  name: {
    fontWeight: "bold",
    color: "#36A2EB",
  },
};

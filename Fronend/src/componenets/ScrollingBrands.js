import Marquee from "react-fast-marquee";
import img1 from "../CarLogos/BMW-Logo.wine.png";
import img2 from "../CarLogos/Dodge-Logo.wine.png";
import img3 from "../CarLogos/Ford_Motor_Company-Logo.wine.png";
import img4 from "../CarLogos/Honda-Logo.wine.png";
import img5 from "../CarLogos/Jeep-Logo.wine.png";
import img6 from "../CarLogos/Kia_Motors-Logo.wine.png";
import img7 from "../CarLogos/Mazda-Logo.wine.png";
import img8 from "../CarLogos/Mercedes-Benz-Logo.wine.png";
import img9 from "../CarLogos/Nissan_Motor_India_Private_Limited-Logo.wine.png";
import img10 from "../CarLogos/Porsche-Logo.wine.png";
import img11 from "../CarLogos/Tesla,_Inc.-Logo.wine.png";
import img12 from "../CarLogos/Toyota_Canada_Inc.-Logo.wine.png";
import img13 from "../CarLogos/Volkswagen_Group-Logo.wine.png";
import img14 from "../CarLogos/Volvo-Logo.wine.png";
import img15 from "../CarLogos/chevrolet_logo1.png";
export default function Brands() {
  return (
    <>
      <div
        style={{
          backgroundColor: "transparent",
          margin: "0px auto",
          overflow: "hidden",
          padding: "20px 0",
        }}
      >
        <Marquee
          speed={50} // سرعة الحركة
          gradient={true} // ما نستخدمش تدرج لوني (نخليه كلام صافي)
          direction="left" // اتجاه الحركة (ممكن تخليه "right" لو عايز)
        >
          <img alt="carLogo" width={250} height={200} src={img1} />
          <img alt="carLogo" width={200} height={200} src={img2} />
          <img alt="carLogo" width={200} height={200} src={img3} />
          <img alt="carLogo" width={200} height={200} src={img4} />
          <img alt="carLogo" width={200} height={200} src={img5} />
          <img alt="carLogo" width={200} height={200} src={img6} />
          <img alt="carLogo" width={200} height={200} src={img7} />
          <img alt="carLogo" width={200} height={200} src={img8} />
          <img alt="carLogo" width={200} height={200} src={img9} />
          <img alt="carLogo" width={200} height={200} src={img10} />
          <img alt="carLogo" width={200} height={200} src={img11} />
          <img alt="carLogo" width={200} height={200} src={img12} />
          <img alt="carLogo" width={200} height={200} src={img13} />
          <img alt="carLogo" width={200} height={200} src={img14} />
          <img alt="carLogo" width={200} height={200} src={img15} />
        </Marquee>
      </div>
    </>
  );
}

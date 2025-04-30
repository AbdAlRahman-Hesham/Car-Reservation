import axios from "axios";
import { label } from "motion/react-client";
import { urlContext } from "../contexts/urlContext";
import { useEffect, useState, useContext } from "react";
import Select from "react-select";
import Checkbox from "@mui/material/Checkbox";

const checkBoxLabel = { inputProps: { "aria-label": "Checkbox demo" } };

export default function MyFilters({ getBrand, getOnlyAvailable }) {
  const { url } = useContext(urlContext);
  const [brandsArray, setBrandsArray] = useState([{ value: 0, label: "All" }]);
  const [modelsArray, setModelsArray] = useState([]);
  const [categoriesArray, setCategoriesArray] = useState([]);
  const [selectedBrand, setSelectedBrand] = useState({
    value: 0,
    label: "All",
  });
  const [selectedModel, setSelectedModel] = useState(modelsArray[0]);
  const [selectedCategory, setSelectedCategory] = useState(categoriesArray[0]);
  const [check, setCheck] = useState(false);
  useEffect(() => {
    axios.get(`${url}/api/Brands`).then((res) => {
      let brands = res.data;
      let brandsData = brands.map((brand) => {
        return { value: brand.id, label: brand.name };
      });
      setBrandsArray([{ value: 0, label: "All" }, ...brandsData]);
    });
  }, [url]);
  useEffect(() => {
    if (selectedBrand.value !== 0) {
      console.log(selectedBrand);
      axios
        .get(`${url}/api/Models?brandid=${selectedBrand.value}`)
        .then((res) => {
          let models = res.data;
          let modelsData = models.map((model) => {
            return { value: model.id, label: model.name };
          });
          let categoriesData = models.map((model) => {
            return { value: model.id, label: model.category };
          });
          setCategoriesArray(categoriesData);
          setModelsArray(modelsData);
        })
        .catch((err) => {
          console.log(err);
        });
    }
  }, [selectedBrand, url]);
  return (
    <>
      <div
        style={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          margin: "20px",
          flexWrap: "wrap",
          gap:"20px"
        }}
      >
        {/* Brand Filter */}
        <div
          style={{
            display: "flex",
            alignItems: "center",
            gap: "10px",
            width: "20%",
          }}
        >
          <h5 style={{ margin: 0 }}>Brand:</h5>
          <div className="forSelect"> 
            {" "}
            <Select
              value={selectedBrand}
              onChange={(e) => {
                setSelectedBrand(e);
                setSelectedModel("");
                setSelectedCategory("");
                getBrand(e.value, "");
              }}
              options={brandsArray}
              classNamePrefix="select"
              placeholder="Select a Brand"
              styles={{
                control: (provided) => ({
                  ...provided,
                  minHeight: "35px",
                }),
              }}
            />
          </div>
        </div>

        {/* Model Filter */}
        <div
          style={{
            display: "flex",
            alignItems: "center",
            gap: "10px",
            width: "20%",
          }}
        >
          <h5 style={{ margin: 0 }}>Model:</h5>
          <div className="forSelect"> 
            <Select
              value={selectedModel}
              onChange={(e) => {
                setSelectedModel(e);
                setSelectedCategory(
                  categoriesArray.find((c) => c.value === e.value)
                );
                getBrand(selectedBrand.value, e.value);
              }}
              options={modelsArray}
              classNamePrefix="select"
              placeholder="Select a Model"
              isDisabled={selectedBrand.value === 0}
              styles={{
                control: (provided) => ({
                  ...provided,
                  minHeight: "35px",
                }),
              }}
            />
          </div>
        </div>

        {/* Category Filter */}
        <div
          style={{
            display: "flex",
            alignItems: "center",
            gap: "10px",
            width: "20%",
          }}
        >
          <h5 style={{ margin: 0 }}>Category:</h5>
          <div className="forSelect"> 
            <Select
              value={selectedCategory}
              onChange={(e) => {
                setSelectedCategory(e);
                setSelectedModel(modelsArray.find((m) => m.value === e.value));
                getBrand(selectedBrand.value, e.value);
              }}
              options={categoriesArray}
              classNamePrefix="select"
              placeholder="Select a Category"
              isDisabled={selectedBrand.value === 0}
              styles={{
                control: (provided) => ({
                  ...provided,
                  minHeight: "35px",
                }),
              }}
            />
          </div>
        </div>

        {/* Checkbox */}
        <div
          style={{
            display: "flex",
            alignItems: "center",
            gap: "10px",
            width: "20%",
            justifyContent: "center",
          }}
        >
          <Checkbox
            {...checkBoxLabel}
            checked={check}
            onChange={(e) => {
              setCheck(e.target.checked);
              getOnlyAvailable(e.target.checked);
            }}
          />
          <h6 style={{ margin: 0 }}>Only Available Cars</h6>
        </div>
      </div>
      <hr />
    </>
  );
}

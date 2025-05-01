import { Form, Col } from "react-bootstrap";
export default function Inputfield({ label, placeholder, formik, name }) {
  console.log(formik.values[name]);
  let type = "text";
  let col = 6;
  if (["pass", "pass2"].includes(name)) {
    type = "password";
  } else if (name === "pic") {
    type = "file";
  }
  if (["street", "city", "country"].includes(name)) {
    col = 4;
  }
  function onChangeForPicture(e) {
    if (e.target.files[0]) {
      formik.setFieldValue("pic", e.target.files[0]);
    }
    formik.setFieldTouched("pic", true, false);
  }
  function onChangeForAll(e) {
    formik.handleChange(e);
    formik.setFieldTouched([name], true, false);
  }
  return (
    <>
      <Col md={col}>
        <Form.Group className="mb-1">
          <Form.Label className="fw-bold">{label}</Form.Label>
          <Form.Control
            accept={name === "pic" ? "images/*" : ""}
            name={name}
            {...(name !== "pic" && { value: formik.values[name] })}
            onChange={(e) => {
              name === "pic" ? onChangeForPicture(e) : onChangeForAll(e);
            }}
            onBlur={formik.handleBlur}
            type={type}
            placeholder={placeholder}
            className={`h-35 ${
              formik.errors[name] && formik.touched[name] ? `invalid-input` : ``
            }`}
          />
          <small
            style={{
              color: "red",
              display: "block",
              minHeight: "22px",
              visibility:
                formik.errors[name] && formik.touched[name]
                  ? "visible"
                  : "hidden",
            }}
          >
            {formik.errors[name]}
          </small>
        </Form.Group>
      </Col>
    </>
  );
}

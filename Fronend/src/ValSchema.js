import * as Yup from "yup";
 export const Myschema =  Yup.object({
    fname: Yup.string()
      .required("First name is required")
      .min(4, "First name must be lmore than 3 chrs."),
    lname: Yup.string()
      .required(`Last name is required`)
      .min(4, "username must be lmore than 3 chrs."),
    email: Yup.string().required(`Email is required`).email(`Invalid Email`),
    phone: Yup.string()
      .required("Phone number is required")
      .matches(/^01[0-9]{9}$/, "Invalid phone number"),
    id: Yup.string()
      .required("National ID is required")
      .matches(/^\d{14}$/, "National ID must be exactly 14 digits"),
    pic: Yup.mixed().required("Profile picture URL is required"),
    pass: Yup.string()
      .required("Password is required")
      .min(7, "Password must be at least 7 characters long")
      .matches(/[A-Z]/, "Password must contain at least one uppercase letter")
      .matches(/[a-z]/, "Password must contain at least one lowercase letter")
      .matches(/\d/, "Password must contain at least one digit")
      .matches(
        /[@$!%*?&]/,
        "Password must contain at least one special character"
      ),
    pass2: Yup.string()
      .oneOf([Yup.ref("pass"), null], "Passwords must match")
      .required("Confirm password is required"),
    street: Yup.string().required("Street is required"),

    city: Yup.string().required("City is required"),

    country: Yup.string().required("Country is required"),
  });



  


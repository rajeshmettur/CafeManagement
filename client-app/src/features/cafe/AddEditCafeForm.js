import React, { useEffect } from "react";
import { Field, reduxForm } from "redux-form";
import { TextField, Button } from "@mui/material";
import { useNavigate, useLocation } from "react-router-dom";
import axios from "../../api/axios";

// Custom Input Component
const renderTextField = ({ input, label, meta: { touched, error } }) => (
  <div>
    <TextField
      {...input}
      label={label}
      variant="outlined"
      fullWidth
      error={touched && !!error}
      helperText={touched && error}
    />
  </div>
);

const AddEditCafeForm = ({ handleSubmit, initialize }) => {
  const navigate = useNavigate();
  const location = useLocation();
  //const [logo, setLogo] = useState(null);

  useEffect(() => {
    if (location.state) {
      // Pre-fill the form in edit mode
      initialize(location.state);
    }
  }, [location.state, initialize]);

  const onSubmit = async (values) => {
    try {
      const method = location.state ? "put" : "post";
      const url = location.state ? `/cafes/${location.state.id}` : "/cafes";

      // Handle logo upload if a new file is selected
      const formData = new FormData();
      if (location.state) formData.append("id", values.id);

      formData.append("name", values.name);
      formData.append("description", values.description);
      formData.append("location", values.location);
      if (values.logo) {
        formData.append("logo", values.logo);
      }

      await axios[method](url, formData);

      navigate("/");
    } catch (error) {
      console.error("Error submitting form:", error);
    }
  };

  /* const handleFileChange = (event) => {
    const file = event.target.files[0];
    if (file && file.size <= 2 * 1024 * 1024) {
      // Validate file size <= 2MB
      setLogo(file);
    } else {
      alert("Logo file size must be less than 2MB.");
    }
  }; */

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <Field name="name" component={renderTextField} label="Name" />
      <Field
        name="description"
        component={renderTextField}
        label="Description"
      />
      <Field name="location" component={renderTextField} label="Location" />
      <Field name="logo" component={renderTextField} label="Logo" />
      {/*<div style={{ margin: "10px 0" }}>
        <input type="file" accept="image/*" onChange={handleFileChange} />
      </div> */}
      <Button
        type="submit"
        variant="contained"
        color="primary"
        style={{ marginRight: 10 }}
      >
        Submit
      </Button>
      <Button variant="outlined" onClick={() => navigate("/")}>
        Cancel
      </Button>
    </form>
  );
};

export default reduxForm({
  form: "cafeForm",
})(AddEditCafeForm);

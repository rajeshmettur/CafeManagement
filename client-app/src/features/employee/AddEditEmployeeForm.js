import React, { useEffect } from "react";
import { Field, reduxForm } from "redux-form";
import {
  TextField,
  RadioGroup,
  FormControlLabel,
  Radio,
  Select,
  MenuItem,
  Button,
} from "@mui/material";
import { useNavigate, useLocation } from "react-router-dom";
import axios from "../../api/axios";

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

const renderRadioGroup = ({ input }) => (
  <RadioGroup {...input} row>
    <FormControlLabel value="M" control={<Radio />} label="Male" />
    <FormControlLabel value="F" control={<Radio />} label="Female" />
  </RadioGroup>
);

const renderSelectField = ({ input, label, children }) => (
  <div>
    <Select {...input} label={label} variant="outlined" fullWidth>
      {children}
    </Select>
  </div>
);

const AddEditEmployeeForm = ({ handleSubmit, initialize }) => {
  const navigate = useNavigate();
  const location = useLocation();

  useEffect(() => {
    if (location.state) {
      // Pre-fill the form in edit mode
      initialize(location.state);
    }
  }, [location.state, initialize]);

  const onSubmit = async (values) => {
    const method = location.state ? "put" : "post";
    const url = location.state
      ? `/employees/${location.state.id}`
      : "/employees";

    try {
      await axios[method](url, values);
      navigate("/employees");
    } catch (error) {
      console.error("Error submitting form:", error);
    }
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <Field name="name" component={renderTextField} label="Name" />
      <Field name="email" component={renderTextField} label="Email" />
      <Field name="phone" component={renderTextField} label="Phone" />
      <Field name="gender" component={renderRadioGroup} />
      <Field
        name="cafeName"
        component={renderSelectField}
        label="Assigned Café"
      >
        {/* Options for cafes would be fetched dynamically */}
        <MenuItem value="Cafe 1">Cafe 1</MenuItem>
        <MenuItem value="Cafe 2">Cafe 2</MenuItem>
        <MenuItem value="Cafe 3">Cafe 3</MenuItem>
      </Field>
      <Button
        type="submit"
        variant="contained"
        color="primary"
        style={{ marginRight: 10 }}
      >
        Submit
      </Button>
      <Button
        variant="outlined"
        onClick={() => navigate(`/employees/${location.state.id}`)}
      >
        Cancel
      </Button>
    </form>
  );
};

export default reduxForm({
  form: "employeeForm",
})(AddEditEmployeeForm);

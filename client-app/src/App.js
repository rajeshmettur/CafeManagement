import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import CafePage from "./features/cafe/CaféPage";
import EmployeePage from "./features/employee/EmployeePage";
import AddEditCafeForm from "./features/cafe/AddEditCafeForm";
import AddEditEmployeeForm from "./features/employee/AddEditEmployeeForm";

const App = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<CafePage />} />
        <Route path="/employees/:cafeId" element={<EmployeePage />} />
        <Route path="/add-cafe" element={<AddEditCafeForm />} />
        <Route path="/edit-cafe/:id" element={<AddEditCafeForm />} />
        <Route path="/add-employee" element={<AddEditEmployeeForm />} />
        <Route path="/edit-employee/:id" element={<AddEditEmployeeForm />} />
      </Routes>
    </Router>
  );
};

export default App;

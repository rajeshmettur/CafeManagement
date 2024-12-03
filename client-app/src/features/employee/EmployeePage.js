import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import axios from "axios";
import { Button } from "@mui/material";
import { AgGridReact } from "ag-grid-react";
import "ag-grid-community/styles/ag-grid.css";
import "ag-grid-community/styles/ag-theme-alpine.css";

const EmployeePage = () => {
  const { cafeId } = useParams(); // Extract CafeId from params
  const [employees, setEmployees] = useState([]);
  const [loading, setLoading] = useState(false);
  const [open, setOpen] = useState(false);
  const [itemToDelete, setItemToDelete] = useState(null);
  const navigate = useNavigate();

  // Fetch employees based on CafeId
  const fetchEmployeesByCafeId = async (id) => {
    try {
      //const response = await axios.get("/cafes");
      const response = await axios.get("http://localhost:5122/api/employees", {
        params: {
          cafeId: id,
        },
      });
      return response.data;
    } catch (error) {
      console.error("Error fetching employees:", error);
    }
  };

  const fetchEmployees = async () => {
    try {
      if (!cafeId) {
        console.warn("CafeId is not provided.");
        return;
      }
      setLoading(true);
      const data = await fetchEmployeesByCafeId(cafeId);
      setEmployees(data);
    } catch (error) {
      console.error("Failed to fetch employees:", error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchEmployees();
  }, []);

  const handleOpen = (data) => {
    setItemToDelete(data);
    setOpen(true);
  };

  const handleClose = () => setOpen(false);

  const confirmDelete = async () => {
    try {
      await axios.delete(`/cafes/${itemToDelete.id}`); // Update endpoint
      alert("Deleted successfully!");
      fetchEmployees(); // Refresh data
    } catch (error) {
      console.error("Error deleting item:", error);
      alert("An error occurred while deleting the item.");
    } finally {
      setOpen(false);
    }
  };

  // Delete employee
  // const handleDelete = async (employeeId) => {
  //   if (window.confirm("Are you sure you want to delete this employee?")) {
  //     try {
  //       await axios.delete(`/api/employees/${employeeId}`);
  //       setEmployees((prev) => prev.filter((emp) => emp.id !== employeeId));
  //     } catch (error) {
  //       console.error("Error deleting employee:", error);
  //     }
  //   }
  //};

  const actionComp = (params) => {
    return (
      <div className="action-buttons">
        <Button
          onClick={() =>
            navigate(`/edit-employee/${params.data.id}`, { state: params.data })
          }
        >
          Edit
        </Button>
        <Button color="error" onClick={() => handleOpen(params.data)}>
          Delete
        </Button>
      </div>
    );
  };

  const columns = [
    { headerName: "ID", field: "id" },
    { headerName: "Name", field: "name" },
    { headerName: "Email", field: "emailAddress" },
    { headerName: "Phone", field: "phoneNumber" },
    { headerName: "Days Worked", field: "daysWorked" },
    { headerName: "Cafe Name", field: "cafe" },
    {
      headerName: "Actions",
      cellRenderer: actionComp,
    },
  ];

  return (
    <div className="ag-theme-alpine" style={{ height: "600px", width: "100%" }}>
      <h1>Employees</h1>
      <Button
        variant="contained"
        color="primary"
        onClick={() => navigate("/add-employee")}
      >
        Add New Employee
      </Button>
      {loading ? (
        <p>Loading...</p>
      ) : (
        <AgGridReact rowData={employees} columnDefs={columns} />
      )}
    </div>
  );
};

export default EmployeePage;

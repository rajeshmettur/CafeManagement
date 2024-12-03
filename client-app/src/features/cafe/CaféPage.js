import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AgGridReact } from "ag-grid-react";
import { Button, TextField, Typography } from "@mui/material";
import axios from "../../api/axios";
import {
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
} from "@mui/material";
import "ag-grid-community/styles/ag-grid.css";
import "ag-grid-community/styles/ag-theme-alpine.css";

const CafePage = () => {
  const [cafes, setCafes] = useState([]);
  const [filterLocation, setFilterLocation] = useState("");
  const navigate = useNavigate();
  const [open, setOpen] = useState(false);
  const [itemToDelete, setItemToDelete] = useState(null);

  const fetchData = async () => {
    try {
      const response = await axios.get("/cafes"); // Update with your endpoint
      setCafes(response.data); // Update the table data
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  };

  useEffect(() => {
    // Fetch cafes from the backend API
    fetchData();
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
      fetchData(); // Refresh data
    } catch (error) {
      console.error("Error deleting item:", error);
      alert("An error occurred while deleting the item.");
    } finally {
      setOpen(false);
    }
  };

  // const handleDelete = async (cafe) => {
  //   try {
  //     await axios.delete(`/cafes/${cafe.id}`);
  //     setCafes(cafes.filter((c) => c.id !== cafe.id));
  //   } catch (error) {
  //     console.error("Error deleting cafe:", error);
  //   }
  // };

  const filteredCafes = cafes.filter((cafe) =>
    cafe.location.toLowerCase().includes(filterLocation.toLowerCase())
  );

  const actionComp = (params) => {
    return (
      <div className="action-buttons">
        <Button
          onClick={() =>
            navigate(`/edit-cafe/${params.data.id}`, { state: params.data })
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

  const employeeComp = (params) => {
    return (
      <div>
        <Button
          onClick={() =>
            navigate(`/employees/${params.data.id}`, { state: params.data })
          }
        >
          {params.value}
        </Button>
      </div>
    );
  };

  const columns = [
    {
      headerName: "Logo",
      field: "logo",
      cellRendererParams: (params) => {
        return <img src={params.value} alt="logo" style={{ width: 50 }} />;
      },
    },
    { headerName: "Name", field: "name" },
    { headerName: "Description", field: "description" },
    { headerName: "Location", field: "location" },
    {
      headerName: "Employees",
      field: "employees",
      cellRenderer: employeeComp,
    },
    {
      headerName: "Actions",
      cellRenderer: actionComp,
    },
  ];

  return (
    <div style={{ margin: 20 }}>
      <Typography variant="h4" gutterBottom>
        Cafés
      </Typography>
      <TextField
        label="Filter by Location"
        variant="outlined"
        value={filterLocation}
        onChange={(e) => setFilterLocation(e.target.value)}
        style={{ marginBottom: 20 }}
      />
      <Button
        variant="contained"
        color="primary"
        onClick={() => navigate("/add-cafe")}
        style={{ marginBottom: 10 }}
      >
        Add New Café
      </Button>
      <div className="ag-theme-alpine" style={{ height: 400, width: "100%" }}>
        <AgGridReact
          rowData={filteredCafes}
          columnDefs={columns}
          defaultColDef={{
            flex: 1,
            minWidth: 100,
            filter: true,
            sortable: true,
            resizable: true,
          }}
          domLayout="autoHeight"
        />
        <Dialog open={open} onClose={handleClose}>
          <DialogTitle>Confirm Deletion</DialogTitle>
          <DialogContent>
            <DialogContentText>
              Are you sure you want to delete{" "}
              {itemToDelete?.name || "this item"}?
            </DialogContentText>
          </DialogContent>
          <DialogActions>
            <Button onClick={handleClose} color="primary">
              Cancel
            </Button>
            <Button onClick={confirmDelete} color="error">
              Confirm
            </Button>
          </DialogActions>
        </Dialog>
      </div>
    </div>
  );
};

export default CafePage;

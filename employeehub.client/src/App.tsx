import { useEffect, useState } from 'react';
import Swal from 'sweetalert2';
import './App.css';

import { NewEmployeeForm } from "./Components/NewEmployeeForm.tsx";
import { EmployeeList } from "./Components/EmployeeList.tsx";
import { EmployeeView, EmployeeResponse, EmployeeUpdate } from './Models/models.ts';
import apiRequest from './api/apiRequest.ts';
import { EmployeeCreate } from './Models/EmployeeCreate.ts';

function App() {
  const API_URL = "http://localhost:5075/api/Employees";

  const [error, setError] = useState<string>();
  const [isLoading, setIsLoading] = useState(false);
  const [employees, setEmployees] = useState<EmployeeView[]>([]);
  const [editingEmployee, setEditingEmployee] = useState<EmployeeResponse | null>(null);


  const fetchEmployees = async () => {
    setIsLoading(true);

    try {
      const data = await apiRequest(API_URL);
      const employees = data.items as EmployeeResponse[];
      setEmployees(employees);
    } catch (e: any) {
      setError(e);
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    fetchEmployees();
  }, []);

  const handleCreate = async (employee: EmployeeCreate) => {
    if(employee.age == '') {
      employee.age = undefined;
    }
    const postOptions = {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(employee)
    };

    try {
      await apiRequest(API_URL, postOptions);
      fetchEmployees();
    } catch (err) {
      console.error("Error creating employee:", err);
    }
  };

  const handleUpdate = async (employee: EmployeeUpdate) => {
    if(employee.age == '') {
      employee.age = undefined;
    }
    const putOptions = {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(employee)
    };

    try {
      await apiRequest(`${API_URL}/${employee.id}`, putOptions);
      fetchEmployees();
      setEditingEmployee(null);
    } catch (err) {
      console.error("Error updating employee:", err);
    }
  };

  const handleDelete = async (id: string) => {
    const result = await Swal.fire({
      title: "Do you really want to delete this user?",
      showCancelButton: true,
      confirmButtonText: "Yes",
      cancelButtonText: "No",
    });
  
    if (result.isConfirmed) {
      try {
        await apiRequest(`${API_URL}/${id}`, { method: "DELETE" });
        fetchEmployees();
      } catch (err) {
        console.error("Error deleting employee:", err);
        Swal.fire("Error", "There was an issue deleting the user.", "error");
      }
    } else {
      
    }
  };

  const handleBulkDelete = async (ids: string[]) => {
    const result = await Swal.fire({
      title: "Do you really want to delete selected users?",
      showCancelButton: true,
      confirmButtonText: "Yes",
      cancelButtonText: "No",
    });
  
    if (result.isConfirmed) {
      try {
        const deleteOptions = {
          method: "DELETE",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({ ids })
        };
  
        await apiRequest(API_URL, deleteOptions);
        fetchEmployees();
      } catch (err) {
        console.error("Bulk delete error:", err);
        Swal.fire("Error", "There was an issue deleting users.", "error");
      }
    }
  };
  if (isLoading) {
    return <div className="loading">Loading...</div>;
  }

  if (error) {
    return <div>Something went wrong! Please try again</div>;
  }

  return (
    <>
      <NewEmployeeForm
        onCreate={handleCreate}
        onUpdate={handleUpdate}
        editingEmployee={editingEmployee}
        cancelEdit={() => setEditingEmployee(null)}
      />
      <h3 className="list-header mt-10">
        <span>
          Employee List
        </span>
      </h3>
      <EmployeeList
        employees={employees}
        deleteEmployee={handleDelete}
        editEmployee={setEditingEmployee}
        onBulkDelete={handleBulkDelete}
      />
    </>
  );
}

export default App;

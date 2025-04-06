import { useState } from "react";
import { EmployeeView } from "../Models/models.ts";
import { EmployeeItem } from "./EmployeeItem"

interface Props {
  employees: EmployeeView[];
  deleteEmployee: (id: string) => void;
  editEmployee: (employee: EmployeeView) => void;
  onBulkDelete: (ids: string[]) => void;
}

export function EmployeeList({ employees, deleteEmployee, editEmployee, onBulkDelete }: Props) {
  const [selectedIds, setSelectedIds] = useState<string[]>([]);

  const toggleSelect = (id: string) => {
    setSelectedIds(prev =>
      prev.includes(id) ? prev.filter(x => x !== id) : [...prev, id]
    );
  };
  
  const sortedEmployeesByName = [...employees].sort((a, b) => a.firstName.localeCompare(b.firstName));
  return (
  <>
  {sortedEmployeesByName.length > 0 && (
    <div className="flex justify-end">
        <button
          onClick={() => onBulkDelete(selectedIds)}
          disabled={selectedIds.length === 0}
          className="relative inline-flex items-center justify-center p-0.5 mb-2 me-2 overflow-hidden text-sm font-medium text-gray-900 rounded-lg group bg-gradient-to-br from-pink-500 to-orange-400 group-hover:from-pink-500 group-hover:to-orange-400 hover:text-white dark:text-white focus:ring-4 focus:outline-none focus:ring-pink-200 dark:focus:ring-pink-800 disabled:opacity-50">
          <span className="relative px-5 py-2.5 transition-all ease-in duration-75 bg-white dark:bg-gray-900 rounded-md group-hover:bg-transparent group-hover:dark:bg-transparent">
            Delete Selected
          </span>
        </button>
      </div>
    )}
     
    <ul className="width-full flex flex-col mt-5 overflow-y-auto max-h-110">
      {sortedEmployeesByName.length == 0 && "List is empty"}
      {sortedEmployeesByName.map((employee, index) => {
        return <EmployeeItem 
          key={employee.id}
          employee={employee}
          deleteEmployee={() => deleteEmployee(employee.id)}
          editEmployee={() => editEmployee(employee)} 
          isSelected={selectedIds.includes(employee.id)}
            toggleSelect={toggleSelect}
          index={index}/>
      })}
    </ul>
  </>

  )
}

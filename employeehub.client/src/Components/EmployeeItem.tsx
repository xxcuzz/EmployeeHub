import { EmployeeResponse } from "../Models/EmployeeResponse";
import { calculateAge } from "../utils/AgeCalculator";

interface EmployeeItemProps {
  employee: EmployeeResponse;
  key: string;
  deleteEmployee: (id: string) => void;
  editEmployee: (id: string) => void;
  isSelected: boolean;
  toggleSelect: (id: string) => void;
  index: number;
}

export function EmployeeItem({ employee, deleteEmployee, editEmployee, isSelected, toggleSelect, index }: EmployeeItemProps) {
  const bgColor = index % 2 === 0 ? "bg-neutral-900" : "bg-neutral-800";

  return (
    <li className={`flex items-center ${bgColor}`} key={employee.id}>
      <input
        type="checkbox"
        checked={isSelected}
        onChange={() => toggleSelect(employee.id)}
        className="mx-2"
      />
      <p className="flex-auto basis-lg ">{employee.firstName} {employee.lastName}</p>
      <p className="flex-auto basis-md">
        {employee.age ? calculateAge(employee.age) + ' years' : '-'}
      </p>
      <p className="flex-auto basis-md">{employee.gender}</p>
      <button className="m-2 items-center justify-center inline-flex bg-transparent hover:bg-emerald-600 text-green-700 font-semibold hover:text-white py-2 px-4 border border-emerald-500 hover:border-transparent rounded-lg"
        onClick={() => editEmployee(employee.id)}>
        Edit
      </button>

      <button onClick={() => deleteEmployee(employee.id)}
        className="m-2 items-center justify-center inline-flex bg-transparent hover:bg-rose-800 text-rose-700 font-semibold hover:text-white py-2 px-4 border border-rose-500 hover:border-transparent rounded-lg">
        Delete
      </button>
    </li>
  )
}
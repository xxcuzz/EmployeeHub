import React, { useState, useEffect } from 'react'
import { EmployeeCreate, EmployeeUpdate } from '../Models/models.ts';
import { Gender } from '../Models/Gender';
import { validateEmployeeForm } from '../utils/EmployeeFormValidator.ts';
import { FormErrors } from '../Models/models.ts';

interface Props {
  onCreate: (employee: EmployeeCreate) => void;
  onUpdate: (employee: EmployeeUpdate) => void;
  editingEmployee: EmployeeUpdate | null;
  cancelEdit: () => void;
}

export function NewEmployeeForm({ onCreate, onUpdate, editingEmployee, cancelEdit }: Props) {
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [dateOfBirth, setDateOfBirth] = useState<string|undefined>('');
  const [gender, setGender] = useState('');

  const [errors, setErrors] = useState<FormErrors>({
    firstName: '',
    lastName: '',
    age: '',
    gender: '',
  });

  useEffect(() => {
    if (editingEmployee) {
      setFirstName(editingEmployee.firstName);
      setLastName(editingEmployee.lastName);

      const formattedDate = editingEmployee.age
      ? new Date(editingEmployee.age).toISOString().split('T')[0]
      : '';
      setDateOfBirth(formattedDate);
      setGender(editingEmployee.gender);
    } else {
      setFirstName('');
      setLastName('');
      setDateOfBirth('');
      setGender('');
    }
  }, [editingEmployee]);

  function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    const validationErrors = validateEmployeeForm(firstName, lastName, dateOfBirth, gender);

    if (Object.keys(validationErrors).length > 0) {
      setErrors(validationErrors);
      return;
    }
    const payload: EmployeeCreate = { firstName, lastName, age: dateOfBirth, gender };
    if (editingEmployee) {
      onUpdate({
        ...editingEmployee,
        firstName,
        lastName,
        age: dateOfBirth,
        gender,
      });
    } else {
      onCreate(payload);
    }

    setFirstName('');
    setLastName('');
    setDateOfBirth('');
    setGender('');
  }

  return (
    <form onSubmit={handleSubmit} className="max-w-sm mx-auto relative top-4">
      <div className="w-full mb-5">
        <input
          className="w-full border-2 border-violet-800 rounded-md p-2"
          value={firstName}
          type="text"
          id="name"
          placeholder="Enter name"
          onChange={e => setFirstName(e.target.value)}
        />
        {errors.firstName && <p className="text-rose-700 text-sm">{errors.firstName}</p>}
      </div>
      <div className='w-full mb-5'>
        <input
          className="w-full border-2 border-violet-800 rounded-md p-2"
          type="text"
          placeholder='Enter surname'
          value={lastName}
          id="surname"
          onChange={e => setLastName(e.target.value)}
        />
        {errors.lastName && <p className="text-rose-700 text-sm">{errors.lastName}</p>}
      </div>
      <div className='w-full mb-5'>
        <div className='flex justify-between'>
          <label htmlFor="date-of-birth">Date of birth: </label>
          <input
            aria-label="Date"
            type="date"
            value={dateOfBirth}
            onChange={e => setDateOfBirth(e.target.value)} />
        </div>
        {errors.age && <p className="text-rose-700 text-sm">{errors.age}</p>}
      </div>
      <div className='w-full mb-5'>
        <select
          className='border-2 border-violet-800 rounded-md p-2 w-full text-gray-500 dark:bg-neutral-800'
          id="gender"
          onChange={e => setGender(e.target.value)}
          value={gender}
        >
          <option value="" disabled selected>Gender</option>
          <option value={Gender.Male}>Male</option>
          <option value={Gender.Female}>Female</option>
          <option value={Gender.Other}>Other</option>
          <option value={Gender.PreferNotToSay}>Prefer Not To Say</option>
        </select>
        {errors.gender && <p className="text-rose-700 text-sm">{errors.gender}</p>}
      </div>
      <button
        className="inline-flex items-center p-0.5 mb-2 me-2 text-sm font-medium text-gray-900 rounded-lg group bg-gradient-to-br from-purple-600 to-blue-500 group-hover:from-purple-600 group-hover:to-blue-500 hover:text-white dark:text-white focus:ring-4 focus:outline-none focus:ring-blue-300 dark:focus:ring-blue-800"
        type="submit">
        <span className="relative px-5 py-2.5 transition-all ease-in duration-75 bg-white dark:bg-gray-900 rounded-md group-hover:bg-transparent group-hover:dark:bg-transparent">
          {editingEmployee ? "Update" : "Create"}
        </span>
      </button>
      {editingEmployee && (
        <button
          type="button"
          onClick={cancelEdit}
          className="relative inline-flex items-center justify-center p-0.5 mb-2 me-2 overflow-hidden text-sm font-medium text-gray-900 rounded-lg group bg-gradient-to-br from-pink-500 to-orange-400 group-hover:from-pink-500 group-hover:to-orange-400 hover:text-white dark:text-white focus:ring-4 focus:outline-none focus:ring-pink-200 dark:focus:ring-pink-800">
          <span className="relative px-5 py-2.5 transition-all ease-in duration-75 bg-white dark:bg-gray-900 rounded-md group-hover:bg-transparent group-hover:dark:bg-transparent">
            Cancel
          </span>
        </button>

      )}
    </form>
  );
}

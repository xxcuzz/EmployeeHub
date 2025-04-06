import { calculateAge } from './AgeCalculator';

export function validateEmployeeForm(firstName: string, lastName: string, age: string|undefined, gender: string) {
  const errors: Record<string, string> = {};

  if (firstName.length < 2 || firstName.length > 20) {
    errors.firstName = 'First name must be between 2 and 20 characters';
  }

  if (lastName.length < 2 || lastName.length > 20) {
    errors.lastName = 'Last name must be between 2 and 20 characters';
  }

  const ageValue = age ? calculateAge(age) : null;
  if (ageValue !== null &&  (ageValue < 18 || ageValue > 100)) {
    errors.age = 'Age must be between 18 and 100 years';
  }

  if (!gender) {
    errors.gender = 'Gender is required';
  }

  return errors;
}



export enum Gender {
  Male = 'Male',
  Female = 'Female',
  Other = 'Other',
  PreferNotToSay = 'PreferNotToSay',
}

export const getGenderDisplay = (gender: string): string => {
  switch (gender) {
    case Gender.Male:
      return 'Male';
    case Gender.Female:
      return 'Female';
    case Gender.Other:
      return 'Other';
    case Gender.PreferNotToSay:
      return 'Prefers Not To Say';
    default:
      return '';
  }
};
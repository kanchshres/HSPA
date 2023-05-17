export interface IPropertyBase {
  ID: number;
  sellOrRent: number;
  name: string;
  propertyType: string,
  furnishingType: string,
  price: number;
  bhk: number,
  builtArea: number,
  city: string,
  readyToMove: number,
  image?: string,
  estPossessionOn?: Date;
}

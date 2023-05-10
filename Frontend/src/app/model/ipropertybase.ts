export interface IPropertyBase {
  ID: number;
  SellOrRent: number;
  Name: string;
  PType: string,
  FType: string,
  Price: number;
  BHK: number,
  BuiltArea: number,
  City: string,
  RTM: number,
  Image?: string;
}

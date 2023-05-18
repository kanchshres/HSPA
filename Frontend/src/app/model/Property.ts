import { IPropertyBase } from './ipropertybase';

export class Property implements IPropertyBase {
  ID!: number;
  sellOrRent!: number;
  name!: string;
  propertyTypeID: number;
  propertyType!: string;
  furnishingTypeID: number;
  furnishingType!: string;
  price!: number;
  bhk!: number;
  builtArea!: number;
  cityID: number;
  city!: string;
  readyToMove!: boolean;
  image?: string;
  carpetArea?: number;
  address!: string;
  address2?: string;
  floorNo?: string;
  totalFloors?: string;
  age?: string;
  mainEntrance?: string;
  security?: number;
  gated?: boolean;
  maintenance?: number;
  estPossessionOn?: string;
  description?: string;
  // PostedOn!: string;
  // PostedBy!: number;
}

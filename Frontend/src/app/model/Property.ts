import { IPropertyBase } from './ipropertybase';

export class Property implements IPropertyBase {
  ID!: number;
  SellOrRent!: number;
  Name!: string;
  PType!: string;
  FType!: string;
  Price!: number;
  BHK!: number;
  BuiltArea!: number;
  City!: string;
  RTM!: number;
  Image?: string;
  CarpetArea?: number;
  Address!: string;
  Address2?: string;
  FloorNo?: string;
  TotalFloor?: string;
  AOP?: string;
  MainEntrance?: string;
  Security?: number;
  Gated?: number;
  Maintenance?: number;
  Possession?: string;
  Description?: string;
  PostedOn!: string;
  PostedBy!: number;
}

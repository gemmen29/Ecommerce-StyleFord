import { IProductCartDetails } from "./IProductCartDetails";

export interface IOrderDetails {
  totalOrderPrice:number;
  productCartDetails:IProductCartDetails[];
  
}
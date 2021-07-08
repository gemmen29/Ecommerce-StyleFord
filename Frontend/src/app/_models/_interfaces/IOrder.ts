export interface IOrder{
  id:number;
  date:string;
  totalPrice:number;
  applicationUserIdentity_Id:string;
  appUser:any;   //represent the the user who makes the order

}
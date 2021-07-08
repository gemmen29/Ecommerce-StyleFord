export interface IProductVM {
  id:number,
  name:string,
  price:number,
  description:string,
  discount:number,
  image:string,
  quantity:number,
  categoryId:number,
  colorId:number,
  colorName:string;
  categoryName:string
  averageRating:number;
}
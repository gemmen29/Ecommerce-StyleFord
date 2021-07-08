import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import {catchError} from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IOrder } from '../_models/_interfaces/IOrder';
import { IOrderDetails } from '../_models/_interfaces/IOrderDetails';
import { IOrderProduct } from '../_models/_interfaces/IOrderProduct';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private _url =  `${environment.apiUrl}/api/order`
  constructor(private _http:HttpClient) { }
  getOrdersByPage(pageSize:number, pageNumber:number):Observable<IOrder[]>{
    return this._http.get<IOrder[]>(`${this._url}/${pageSize}/${pageNumber}`).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  getOrdersByPageForSpecficUser(userID:string,pageSize:number, pageNumber:number):Observable<IOrder[]>{
    return this._http.get<IOrder[]>(`${this._url}/${userID}/${pageSize}/${pageNumber}`).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  getOrderCountForSpecficUser(userID:string):Observable<number>{
    return this._http.get<number>(`${this._url}/countordersforspecifcuser/${userID}`).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  getOrderCount():Observable<number>{
    return this._http.get<number>(`${this._url}/count`).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  orderDetails(id:number):Observable<IOrderProduct[]>{
    return this._http.get<IOrderProduct[]>(`${this._url}/${id}`).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  makeOrder(orderDetails:IOrderDetails){
    let url = `${environment.apiUrl}/api/order`;
    return this._http.post<IOrderDetails>(url, orderDetails)
            .pipe(catchError((err)=>{
              return throwError(err.message ||"Internal Server error contact site adminstarator");
                }
              ));
  }
}

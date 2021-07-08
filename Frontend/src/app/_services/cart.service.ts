import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { IProduct } from '../_models/_interfaces/IProduct';
import { IProductCartVM } from '../_models/_interfaces/IProductCartVM';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  constructor(private _http: HttpClient) { }

  getAll(): Observable<IProductCartVM[]> {
    let url = `${environment.apiUrl}/api/cart`;
    return this._http.get<IProductCartVM[]>(url).pipe(catchError((err) => {
      return throwError(err.message || "Internal Server error contact site adminstarator");
    }));
  }

  addProductToCart(productID: number) {
    console.log("Add Product to Cart");
    let url = `${environment.apiUrl}/api/cart/${productID}`;
    return this._http.post<IProduct>(url, productID)
      .pipe(catchError((err) => {
        return throwError(err.message || "Internal Server error contact site adminstarator");
      }));
  }

  deleteProductFromCart(productID: number){
    let url = `${environment.apiUrl}/api/cart/${productID}`;
    return this._http.delete<any>(url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
}

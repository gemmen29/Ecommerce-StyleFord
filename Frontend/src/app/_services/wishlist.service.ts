import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import {catchError} from 'rxjs/operators';
import { ICategory } from '../_models/_interfaces/ICategory';
import { environment } from '../../environments/environment';
import { IProduct } from '../_models/_interfaces/IProduct';

@Injectable({
  providedIn: 'root'
})
export class WishlistService {

  constructor(private _http: HttpClient) { }
  getAll(): Observable<IProduct[]> {
    let url = `${environment.apiUrl}/api/wishlist`;
    return this._http.get<IProduct[]>(url).pipe(catchError((err) => {
      return throwError(err.message || "Internal Server error contact site adminstarator");
    }));
  }

  addProductToWishlist(productID: number) {
    let url = `${environment.apiUrl}/api/wishlist/${productID}`;
    return this._http.post<IProduct>(url, productID)
      .pipe(catchError((err) => {
        return throwError(err.message || "Internal Server error contact site adminstarator");
      }));
  }

  deleteProductFromWishlist(prodID: number){
    let url = `${environment.apiUrl}/api/wishlist/${prodID}`;
    return this._http.delete<any>(url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
}

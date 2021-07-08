import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IProduct } from '../_models/_interfaces/IProduct';
import { IProductVM } from '../_models/_interfaces/IProductVM';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private _http: HttpClient) { }

  getAllProducts(): Observable<IProductVM[]> {
    let url = `${environment.apiUrl}/api/product`;
    return this._http.get<IProductVM[]>(url).pipe(catchError((err) => {
      return throwError(err.message || "Internal Server error contact site adminstarator");
    }));
  }
  getNewArrivalsProducts(numberOfProducts:number): Observable<IProductVM[]> {
    let url = `${environment.apiUrl}/api/product/newArrivals/${numberOfProducts}`;
    return this._http.get<IProductVM[]>(url).pipe(catchError((err) => {
      return throwError(err.message || "Internal Server error contact site adminstarator");
    }));
  }
  getProductById(id: number): Observable<IProductVM> {
    let url = `${environment.apiUrl}/api/product/${id}`;
    return this._http.get<IProductVM>(url).pipe(catchError((err) => {
      return throwError(err.message || "Internal Server error contact site adminstarator");
    }));
  }
  addNewProduct(newProduct: IProduct): Observable<IProduct> {
    let url = `${environment.apiUrl}/api/product`;
    return this._http.post<IProduct>(url, newProduct)
      .pipe(catchError((err) => {
        return throwError(err.message || "Internal Server error contact site adminstarator");
      }
      ));
  }
  updateProduct(id: number, productToUpdate: IProduct): Observable<IProduct> {
    let url = `${environment.apiUrl}/api/product/${id}`;
    return this._http.put<IProduct>(url, productToUpdate)
      .pipe(catchError((err) => {
        return throwError(err.message || "Internal Server error contact site adminstarator");
      }
      ));
  }
  deleteProduct(id: number): Observable<any> {
    let url = `${environment.apiUrl}/api/product/${id}`;
    return this._http.delete<any>(url).pipe(catchError((err) => {
      return throwError(err.message || "Internal Server error contact site adminstarator");
    }));
  }
  getProductsCount(categoryId:number = 0, colorId = 0): Observable<number> {
    let url = `${environment.apiUrl}/api/product/count`;
    if(categoryId != 0){
      url = `${environment.apiUrl}/api/product/count?categoryId=${categoryId}`;
    }
    if(colorId != 0){
      url = `${environment.apiUrl}/api/product/count?colorId=${colorId}`;
    }
    return this._http.get<number>(url).pipe(catchError((err) => {
      return throwError(err.message || "Internal Server error contact site adminstarator");
    }));
  }
  getProductsByPage(pageSize: number, pageNumber: number): Observable<IProductVM[]> {
    let url = `${environment.apiUrl}/api/product/${pageSize}/${pageNumber}`;
    return this._http.get<IProductVM[]>(url).pipe(catchError((err) => {
      return throwError(err.message || "Internal Server error contact site adminstarator");
    }));
  }
  getProductsByCategoryPaging(categoryId:number, pageSize: number, pageNumber: number): Observable<IProductVM[]> {
    let url = `${environment.apiUrl}/api/product/category/${categoryId}/${pageSize}/${pageNumber}`;
    return this._http.get<IProductVM[]>(url).pipe(catchError((err) => {
      return throwError(err.message || "Internal Server error contact site adminstarator");
    }));
  }
  getProductsByColorPaging(colorId:number, pageSize: number, pageNumber: number): Observable<IProductVM[]> {
    let url = `${environment.apiUrl}/api/product/color/${colorId}/${pageSize}/${pageNumber}`;
    return this._http.get<IProductVM[]>(url).pipe(catchError((err) => {
      return throwError(err.message || "Internal Server error contact site adminstarator");
    }));
  }
  getProductsBySearch(searchKeyWord:string): Observable<IProductVM[]> {
    let url = `${environment.apiUrl}/api/product/search/${searchKeyWord}`;
    return this._http.get<IProductVM[]>(url).pipe(catchError((err) => {
      return throwError(err.message || "Internal Server error contact site adminstarator");
    }));
  }
  getRandomRelatedProducts(categoryId:number, numberOfProducts:number): Observable<IProductVM[]> {
    let url = `${environment.apiUrl}/api/product/relatedProducts/${categoryId}/${numberOfProducts}`;
    return this._http.get<IProductVM[]>(url).pipe(catchError((err) => {
      return throwError(err.message || "Internal Server error contact site adminstarator");
    }));
  }
}

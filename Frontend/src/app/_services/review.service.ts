import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IReview } from '../_models/_interfaces/IReview';
import { IReviewVM } from '../_models/_interfaces/IReviewVM';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  constructor(private _http:HttpClient) { }
  addNewReview(newReview:IReview):Observable<IReviewVM>{
    let url = `${environment.apiUrl}/api/Review`;
    return this._http.post<IReviewVM>(url, newReview)
            .pipe(catchError((err)=>{
              return throwError(err.message ||"Internal Server error contact site adminstarator");
                }
              ));
  }
  updateReview(productId:number, ReviewToUpdate:IReview):Observable<IReviewVM>{
    let url = `${environment.apiUrl}/api/Review/${productId}`;
    return this._http.put<IReviewVM>(url, ReviewToUpdate)
            .pipe(catchError((err)=>{
              return throwError(err.message ||"Internal Server error contact site adminstarator");
                }
              ));
  }
  deleteReview(id:number):Observable<any>{
    let url = `${environment.apiUrl}/api/Review/${id}`;
    return this._http.delete<any>(url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  getReviewsCount(productId:number):Observable<number>{
    let url = `${environment.apiUrl}/api/Review/count/${productId}`;
    return this._http.get<number>(url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  getReviewsByPage(productId:number,pageSize:number, pageNumber:number):Observable<IReviewVM[]>{
    let url = `${environment.apiUrl}/api/Review/${productId}/${pageSize}/${pageNumber}`;
    return this._http.get<IReviewVM[]>(url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  getCurrentUserReviewOnProduct(productId:number):Observable<IReviewVM>{
    let url = `${environment.apiUrl}/api/Review/${productId}`;
    return this._http.get<IReviewVM>(url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
}

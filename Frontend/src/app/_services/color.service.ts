import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IColor } from '../_models/_interfaces/IColor';

@Injectable({
  providedIn: 'root'
})
export class ColorService {

  constructor(private _http: HttpClient) { }

  getAllColors(): Observable<IColor[]> {
    let url = `${environment.apiUrl}/api/color`;
    return this._http.get<IColor[]>(url).pipe(catchError((err) => {
      return throwError(err.message || "Internal Server error contact site adminstarator");
    }));
  }
  getColorById(id: number): Observable<IColor> {
    let url = `${environment.apiUrl}/api/color/${id}`;
    return this._http.get<IColor>(url).pipe(catchError((err) => {
      return throwError(err.message || "Internal Server error contact site adminstarator");
    }));
  }
  addNewColor(newColor: IColor): Observable<IColor> {
    let url = `${environment.apiUrl}/api/color`;
    return this._http.post<IColor>(url, newColor)
      .pipe(catchError((err) => {
        return throwError(err.message || "Internal Server error contact site adminstarator");
      }
      ));
  }
  updateColor(id: number, colorToUpdate: IColor): Observable<IColor> {
    let url = `${environment.apiUrl}/api/color/${id}`;
    return this._http.put<IColor>(url, colorToUpdate)
      .pipe(catchError((err) => {
        return throwError(err.message || "Internal Server error contact site adminstarator");
      }
      ));
  }
  deleteColor(id: number): Observable<any> {
    let url = `${environment.apiUrl}/api/color/${id}`;
    return this._http.delete<any>(url).pipe(catchError((err) => {
      return throwError(err.message || "Internal Server error contact site adminstarator");
    }));
  }
  getColorsCount(): Observable<number> {
    let url = `${environment.apiUrl}/api/color/count`;
    return this._http.get<number>(url).pipe(catchError((err) => {
      return throwError(err.message || "Internal Server error contact site adminstarator");
    }));
  }
  getColorsByPage(pageSize: number, pageNumber: number): Observable<IColor[]> {
    let url = `${environment.apiUrl}/api/color/${pageSize}/${pageNumber}`;
    return this._http.get<IColor[]>(url).pipe(catchError((err) => {
      return throwError(err.message || "Internal Server error contact site adminstarator");
    }));
  }
}

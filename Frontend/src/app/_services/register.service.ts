import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ICategory } from '../_models/_interfaces/ICategory';
import { IUser } from '../_models/_interfaces/IUser';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  constructor(private _http: HttpClient) { }

  getAllAccounts(): Observable<IUser[]> {
    let url = `${environment.apiUrl}/api/account`;
    return this._http.get<IUser[]>(url).pipe(catchError((err) => {
      return throwError(err.message || "Internal Server error contact site adminstarator");
    }));
  }

  getUserById(id: string): Observable<IUser> {
    let url = `${environment.apiUrl}/api/account/${id}`;
    return this._http.get<IUser>(url).pipe(catchError((err) => {
      return throwError(err.message || "Internal Server error contact site adminstarator");
    }));
  }

  getCurrentUser(): Observable<IUser> {
    let url = `${environment.apiUrl}/api/account/current`;
    return this._http.get<IUser>(url).pipe(catchError((err) => {
      return throwError(err.message || "Internal Server error contact site adminstarator");
    }));
  }

  addNewAdmin(newUser: IUser): Observable<IUser> {
    let url = `${environment.apiUrl}/RegisterAdmin`;
    return this._http.post<IUser>(url, newUser)
      .pipe(catchError((err) => {
        return throwError(err.message || "Internal Server error contact site adminstarator");
      }
      ));
  }
  addNewUser(newUser: IUser): Observable<IUser> {
    let url = `${environment.apiUrl}/Register`;
    return this._http.post<IUser>(url, newUser)
      .pipe(catchError((err) => {
        return throwError(err.message || "Internal Server error contact site adminstarator");
      }
      ));
  }

  updateUser(id: string, userToUpdate: IUser): Observable<IUser> {
    let url = `${environment.apiUrl}/api/account/${id}`;
    return this._http.put<IUser>(url, userToUpdate)
      .pipe(catchError((err) => {
        return throwError(err.message || "Internal Server error contact site adminstarator");
      }
      ));
  }

  deleteUser(id: string): Observable<any> {
    let url = `${environment.apiUrl}/api/account/${id}`;
    return this._http.delete<any>(url).pipe(catchError((err) => {
      return throwError(err.message || "Internal Server error contact site adminstarator");
    }));
  }

  getAccountsCount(): Observable<number> {
    let url = `${environment.apiUrl}/api/account/count`;
    return this._http.get<number>(url).pipe(catchError((err) => {
      return throwError(err.message || "Internal Server error contact site adminstarator");
    }));
  }
  getAccountsByPage(pageSize: number, pageNumber: number): Observable<IUser[]> {
    let url = `${environment.apiUrl}/api/account/${pageSize}/${pageNumber}`;
    return this._http.get<IUser[]>(url).pipe(catchError((err) => {
      return throwError(err.message || "Internal Server error contact site adminstarator");
    }));
  }
}

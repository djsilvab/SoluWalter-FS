import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

const apiUrl = 'https://localhost:5001/api/users';
const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(
    private http: HttpClient
  ) { }

    private handleError(error: HttpErrorResponse) {
        if (error.error instanceof ErrorEvent) {
            console.error('Ocurrio el siguiente error:', error.error.message);
        } else {
            console.error(
                `El backend retorno un error con status ${error.status},
                el cuerpo con error: ${error.error}`
            );
        }

        return throwError('Algo a ocurrido, intentar de nuevo en un momento.');
    }

  login(data: any):Observable<any>{
      const url = `${apiUrl}/authenticate`;
      return this.http.post(url, data, httpOptions).pipe(
          catchError(this.handleError)
      );
  }

  registrar(data:any):Observable<any>{
    return this.http.post(apiUrl, data, httpOptions).pipe(
        catchError(this.handleError)
    );
  }
}

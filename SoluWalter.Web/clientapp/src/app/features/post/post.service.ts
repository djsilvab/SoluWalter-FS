import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators'

const apiUrl = 'https://localhost:5001/api/posts';
const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
    providedIn: 'root'
})
export class PostService {

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

    private extractData(res: Response) {
        const body = res;
        return body || {};
    }

    getAll(): Observable<any> {
        return this.http.get(apiUrl, httpOptions).pipe(
            //map(this.extractData),
            catchError(this.handleError)
        )
    }

    getOne(id: string): Observable<any> {
        const url = `${apiUrl}/${id}`;
        return this.http.get(url, httpOptions).pipe(
            //map(this.extractData),
            catchError(this.handleError)
        )
    }

    save(data: any):Observable<any> {
        return this.http.post(apiUrl, data, httpOptions).pipe(
            catchError(this.handleError)
        );
    }

    update(id: string, data: any): Observable<any> {
        const url = `${apiUrl}/${id}`;
        return this.http.put(url, data, httpOptions).pipe(
            catchError(this.handleError)
        )
    }

    delete(id: string): Observable<{}> {
        const url = `${apiUrl}/${id}`;
        return this.http.delete(url, httpOptions).pipe(
            catchError(this.handleError)
        );
    }
}

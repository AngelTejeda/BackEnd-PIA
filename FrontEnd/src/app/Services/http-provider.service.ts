import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpProviderService {
  
  port: number = 44310;
  baseUrl : string = `https://localhost:${this.port}/api/`
  token: string;


  constructor(private http: HttpClient) { }

  // Petición GET al controlador Login. Devuelve true si el login fue satisfactorio.
  login(username: string, password: string) {
    let url = this.baseUrl + `Login?username=${username}&password=${password}`;
    let params: HttpParams = new HttpParams() // Usuario y contraseña incluidos en el URL
      .set("username", username)
      .set("password", password);
    let success = new Subject<boolean>(); // Estado de la petición.

    // Petición GET
    this.http.get(url, {params: params})
      // Esperamos respuesta
      .subscribe(
        // Respuesta satisfactoria.
        (data: any) => {
          this.token = data.token;
          success.next(true);
        },

        // Error
        (err) => {
          success.next(false);
        }
    );

    return success.asObservable();
  }

  // Header con el token de autorización.
  private getHeaders() {
    return new HttpHeaders({'Authorization': 'Bearer ' + this.token});
  }

  // Petición GET
  getRequest<T>(component: string, endpoint: string = "") {
    let url = this.baseUrl + component + "/" + endpoint;

    return this.http.get<T>(url, {headers: this.getHeaders()});
  }

  // Petición POST
  postRequest(component: string, bodyParams: any) {
    let url = this.baseUrl + component;

    return this.http.post(url, bodyParams, {headers: this.getHeaders()});
  }

  // Petición PUT
  putRequest(component: string, endpoint: string = "", bodyParams: any) {
    let url = this.baseUrl + component + "/" + endpoint;

    return this.http.put<string>(url, bodyParams, {headers: this.getHeaders()});
  }

  // Petición DELETE
  deleteRequest(component: string, endpoint: string = "") {
    let url = this.baseUrl + component + "/" + endpoint;

    return this.http.delete(url, {headers: this.getHeaders()});
  }
}

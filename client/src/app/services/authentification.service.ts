import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { tap } from 'rxjs';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthentificationService {

  constructor(private http: HttpClient, private router: Router) { }


  SignUp(
    userName: string,
    email: string,
    password: string,
    confirmPassword: string,
    fullname: string
  ){
    return this.http
    .post<any>(`${environment.apiUrl}/api/User/admin/signup`,
      {
        userName,
        email,
        password,
        confirmPassword,
        fullname
      }
    ).pipe(
      tap(
        (response) => {
          console.log("the response is ",response)
        },
        (error) => {
          console.log("an error occured" , error)
        }
      )
    )
  }


  Login(email: string, password: string) {
    return this.http
      .post<any>(`${environment.apiUrl}/api/User/Login`, { email, password })
      .pipe(
        tap(
          (response) => {
            console.log("Login successful:", response);
          },
          (error) => {
            console.log("Login error:", error);
          }
        )
      );
  }

}

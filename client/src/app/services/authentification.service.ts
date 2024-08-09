import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { tap } from 'rxjs';

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
    .post<any>("http://localhost:5113/api/User/admin/signup",
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
}

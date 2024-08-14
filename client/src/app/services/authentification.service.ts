import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, Subject, tap } from 'rxjs';
import { environment } from '../environments/environment';
import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { GoogleLoginProvider } from '@abacritt/angularx-social-login';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root',
})
export class AuthentificationService {
  private authChangeSub = new Subject<boolean>();
  private extAuthChangeSub = new Subject<SocialUser>();
  public authChanged = this.authChangeSub.asObservable();
  public extAuthChanged = this.extAuthChangeSub.asObservable();
  constructor(
    private http: HttpClient,
    private router: Router,
    private externalAuthService: SocialAuthService
  ) {
    this.externalAuthService.authState.subscribe((user) => {
      console.log(user);
      this.extAuthChangeSub.next(user);
    });
  }

  SignUp(
    userName: string,
    email: string,
    password: string,
    confirmPassword: string,
    fullname: string
  ) {
    return this.http
      .post<any>(`${environment.apiUrl}/api/User/admin/signup`, {
        userName,
        email,
        password,
        confirmPassword,
        fullname,
      })
      .pipe(
        tap(
          (response) => {
            console.log('the response is ', response);
          },
          (error) => {
            console.log('an error occured', error);
          }
        )
      );
  }

  Login(email: string, password: string) {
    return this.http
      .post<any>(
        `${environment.apiUrl}/api/User/Login`,
        { email, password },
        { withCredentials: true }
      )
      .pipe(
        tap(
          (response) => {
            console.log('Login successful:', response);
          },
          (error) => {
            console.log('Login error:', error);
          }
        )
      );
  }
  exeternalAuth(tokenId: string) {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    return this.http
      .post<any>(
        `${environment.apiUrl}/api/User/google-signin`,
        JSON.stringify({ tokenId }),
        {
          headers: headers,
          withCredentials: true,
        }
      )
      .pipe(
        tap(
          (response) => {
            console.log('Login successful:', response);
          },
          (error) => {
            console.log('Login error:', error);
          }
        )
      );
  }
}

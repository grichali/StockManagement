declare var google: any;
import { Component, OnInit } from '@angular/core';
import { AuthentificationService } from '../../services/authentification.service';
import Swal from 'sweetalert2';
import { CookieService } from 'ngx-cookie-service';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'], // Fixed typo here: should be styleUrls
})
export class LoginComponent implements OnInit {
  imgSrc: string = 'image.png';
  googleImgSrc: string = 'icons8-google-48.png';
  email = '';
  password = '';
  loginFailed: boolean = false;
  isAuthenticated: boolean = false;

  constructor(
    private authService: AuthentificationService,
    private cookieService: CookieService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.authService.isAuthenticated().subscribe({
      next: (res: boolean) => {
        this.isAuthenticated = res;
        console.log(this.isAuthenticated);
        if (this.isAuthenticated) {
          Swal.fire({
            title: 'Success',
            text: 'User is authenticated',
            icon: 'success',
          });
          this.router.navigate(['/']);
        }
      },
      error: (err: Error) => {
        console.log(err);
      },
    });

    this.loadGoogleScript()
      .then(() => {
        this.initializeGoogleSignIn();
      })
      .catch((error) => {
        console.error('Google script failed to load', error);
      });
  }

  private loadGoogleScript(): Promise<void> {
    return new Promise((resolve, reject) => {
      if (typeof google !== 'undefined' && google.accounts) {
        resolve(); // Script is already loaded
        return;
      }

      const script = document.createElement('script');
      script.src = 'https://accounts.google.com/gsi/client';
      script.async = true;
      script.defer = true;
      script.onload = () => resolve();
      script.onerror = (error) => reject(error);

      document.head.appendChild(script);
    });
  }

  private initializeGoogleSignIn(): void {
    google.accounts.id.initialize({
      client_id:
        '271434985475-01c119m4h7nhp3d33nmh9tovpo561st7.apps.googleusercontent.com',
      callback: (res: any) => this.handleGoogleSignIn(res),
    });

    google.accounts.id.renderButton(
      document.getElementById('buttonDiv'),
      { theme: 'outline', size: 'large' } // customization attributes
    );

    google.accounts.id.prompt();
  }

  private handleGoogleSignIn(response: any): void {
    this.authService.exeternalAuth(response.credential).subscribe(
      (res) => {
        console.log('Google Sign-In successful:', res);
        Swal.fire({
          title: 'Success',
          text: 'Google Sign-In is successful',
          icon: 'success',
        });
      },
      (err) => {
        console.log('Google Sign-In error:', err);
        Swal.fire({
          title: 'Error',
          text: 'Google Sign-In failed',
          icon: 'error',
        });
      }
    );
  }

  login() {
    this.authService.Login(this.email, this.password).subscribe(
      (response) => {
        console.log('Login successful:', response);
        this.loginFailed = false;
        Swal.fire({
          title: 'Success',
          text: 'Login Is Successful',
          icon: 'success',
        });
      },
      (error) => {
        console.log('Login error:', error);
        this.loginFailed = true;
      }
    );
  }
}

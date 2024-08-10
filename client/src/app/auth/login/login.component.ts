import { Component } from '@angular/core';
import { AuthentificationService } from '../../services/authentification.service';
import Swal from 'sweetalert2';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  imgSrc: string = 'image.png';
  googleImgSrc: string = 'icons8-google-48.png';
  email = '';
  password = '';
  loginFailed: boolean = false;

  constructor(private authService: AuthentificationService){}

  login() {
    this.authService.Login(this.email, this.password).subscribe(
      (response) => {
        console.log('Login successful:', response);
        this.loginFailed = false;
        Swal.fire({
          title: 'Success',
          text: 'Login Is Successful',
          icon: 'success'
        })
      },
      (error) => {
        console.log('Login error:', error);
        this.loginFailed = true;
      }
    );
  }
}

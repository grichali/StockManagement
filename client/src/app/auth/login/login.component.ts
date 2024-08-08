import { Component } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  imgSrc: string = 'image.png';
  googleImgSrc: string = 'icons8-google-48.png';
  username = '';
  password = '';

  login() {}
}

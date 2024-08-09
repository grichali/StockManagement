import { Component } from '@angular/core';
import { AuthentificationService } from '../../services/authentification.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  fullName: string = '';
  userName: string = '';
  email: string = '';
  password: string = '';
  confirmPassword: string = '';
  constructor(private authService: AuthentificationService, private router: Router){}

  register() {
    if (this.password !== this.confirmPassword) {
      console.error('Passwords do not match');
      return;
    }

    this.authService.SignUp(this.userName, this.email, this.password, this.confirmPassword, this.fullName)
      .subscribe(
        response => {
          console.log('Registration successful', response);
          Swal.fire({
            title: 'Welcome!',
            text: `${response.fullName}`,
            icon: 'success',
          });
          this.router.navigate(['/login']);
        },
        error => {
          console.error('An error occurred during registration', error);
        }
      );
  }
}

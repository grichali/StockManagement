import { Component } from '@angular/core';
import { AuthentificationService } from '../../services/authentification.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { NgForm } from '@angular/forms';

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

  register(registerForm: NgForm) {
    if (registerForm.invalid) {
      // Trigger validation messages
      for (const control of Object.values(registerForm.controls)) {
        control.markAsTouched();
      }
      return;
    }


    if (this.password !== this.confirmPassword) {
      Swal.fire({
        title: 'Error',
        text: 'Passwords do not match.',
        icon: 'error',
      });
      return;
    }

    this.authService.SignUp(this.userName, this.email, this.password, this.confirmPassword, this.fullName)
      .subscribe(
        response => {
          console.log('Registration successful', response);
          Swal.fire({
            title: 'Welcome!',
            text: `Hello ${response.fullName}, your registration was successful.`,
            icon: 'success',
          });
          this.router.navigate(['/login']);
        },
        error => {
          console.error('An error occurred during registration', error);
          Swal.fire({
            title: 'Error',
            text: 'An error occurred during registration',
            icon: 'error',
          });
        }
      );
  }
}

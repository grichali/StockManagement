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
          // let errorMessage = 'An unknown error occurred. Please try again later.';
          // if (error.status === 400) {
          //   console.log("hada howa error" )
          //   console.log("xx",error.errorincludes('Email') )
          //   if (error.error.message.includes('Email')) {
          //     errorMessage = 'Email already exists.';
          //   } else if (error.error.message.includes('username')) {
          //     errorMessage = 'Username already exists.';
          //   }
          // }
          // else if (error.error.message.includes('Password')){
          //   errorMessage = error.error.message;
          // } else if (error.status === 0) {
          //   errorMessage = 'Network error. Please check your internet connection.';
          // }
          Swal.fire({
            title: 'Error',
            text: 'An error occurred during registration',
            icon: 'error',
          });
        }
      );
  }
}

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { IUser } from 'src/app/_models/_interfaces/IUser';
import { RegisterService } from 'src/app/_services/register.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  loading = false;
  submitted = false;
  error = '';
  genderList = ["male", "female"]
  constructor
    (private formBuilder: FormBuilder,
      private _route: ActivatedRoute,
      private _router: Router,
      private _registerService: RegisterService
    ) { }

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
      email: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      gender: ['', Validators.required]
    });
  }

  get formFields() { return this.registerForm.controls; }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.registerForm.invalid) {
      return;
    }

    this.loading = true;
    let newUser: IUser = {
      id: "",
      userName: this.formFields.username.value,
      passwordHash: this.formFields.password.value,
      email: this.formFields.email.value,
      firstName: this.formFields.firstName.value,
      lastName: this.formFields.lastName.value,
      gender: this.formFields.gender.value
    }
    this._registerService.addNewUser(newUser)
      .pipe(first())
      .subscribe(
        data => {
          this._router.navigate([""]);
        },
        error => {
          this.error = error;
          this.loading = false;
        });
  }
}

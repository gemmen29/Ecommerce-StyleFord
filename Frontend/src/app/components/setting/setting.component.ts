import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { IUser } from 'src/app/_models/_interfaces/IUser';
import { RegisterService } from 'src/app/_services/register.service';

@Component({
  selector: 'app-setting',
  templateUrl: './setting.component.html',
  styleUrls: ['./setting.component.scss']
})
export class SettingComponent implements OnInit {
  settingForm: FormGroup;
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
    this.settingForm = this.formBuilder.group({
      currentUserID : [''],
      username: ['', Validators.required],
      email: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      gender: ['', Validators.required]
    });
    this._registerService.getCurrentUser()
      .pipe(first())
      .subscribe(
        data => {
          this.settingForm.setValue({
            currentUserID: data.id,
            username: data.userName,
            email: data.email,
            firstName: data.firstName,
            lastName: data.lastName,
            gender: data.gender
          });
        },
        error => {
          this.error = error;
          this.loading = false;
        });
  }

  get formFields() { return this.settingForm.controls; }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.settingForm.invalid) {
      return;
    }

    this.loading = true;
    let UpdatedUser: IUser = {
      id: this.formFields.currentUserID.value,
      userName: this.formFields.username.value,
      passwordHash: "@Admin12345",
      email: this.formFields.email.value,
      firstName: this.formFields.firstName.value,
      lastName: this.formFields.lastName.value,
      gender: this.formFields.gender.value
    }
    console.log(UpdatedUser.id);
    console.log(UpdatedUser)
    this._registerService.updateUser(UpdatedUser.id, UpdatedUser)
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

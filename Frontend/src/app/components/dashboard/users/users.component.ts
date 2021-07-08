import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { UserRoles } from 'src/app/_models/_enums/UserRoles';
import { IUser } from 'src/app/_models/_interfaces/IUser';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { RegisterService } from 'src/app/_services/register.service';
import { ConfirmModalComponent } from '../../_reusableComponents/confirm-modal/confirm-modal.component';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  @ViewChild('addOrUpdateModelCloseBtn') addOrUpdateModelCloseBtn;
  @ViewChild(ConfirmModalComponent) confirmModal:ConfirmModalComponent;
  hasUsers:boolean = false;
  registerForm: FormGroup;
  private _UserToUpdate:IUser;
  error = '';
  genderList = ["male", "female"]
  roles: string[] = Object.values(UserRoles);
  allUsers:IUser[]; 
  errorMsg:string;
  // userForm : FormGroup;
  loading = false;
  submitted = false;
  actionName:string;
  usersCount:number;
  pageSize:number = 8;
  currentPageNumber:number = 1;
  numberOfPages:number; // productsCount / pageSize
  constructor(
    private formBuilder: FormBuilder,
    private _route: ActivatedRoute,
    private _router: Router,
    private _registerService: RegisterService ,
    private _authenticationService : AuthenticationService
    ) { }

  ngOnInit(): void {

    this.registerForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
      email: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      gender: ['', Validators.required],
      role: ['', Validators.required]
    });

    this._registerService.getAccountsCount().subscribe(
      data => {
        this.usersCount = data
        this.numberOfPages = Math.ceil(this.usersCount / this.pageSize)
      },
      error=>
      {
       this.errorMsg = error;
      }
    ) 
    this.getSelectedPage(1);
  }

  get formFields() { return this.registerForm.controls; }

  onAddUserSubmit() {
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
    if(this.formFields.role.value == UserRoles.Admin)
    {
      this._registerService.addNewAdmin(newUser)
        .pipe(first())
        .subscribe(
          data => {
            this._router.routeReuseStrategy.shouldReuseRoute = () => false;
            this._router.onSameUrlNavigation = 'reload';
            this.addOrUpdateModelCloseBtn.nativeElement.click();
            this._router.navigate([this._router.url]);
          },
          error => {
            this.error = error;
            this.loading = false;
          });
      }
      else if(this.formFields.role.value == UserRoles.User){
        this._registerService.addNewUser(newUser)
        .pipe(first())
        .subscribe(
          data => {
            this._router.routeReuseStrategy.shouldReuseRoute = () => false;
            this._router.onSameUrlNavigation = 'reload';
            this.addOrUpdateModelCloseBtn.nativeElement.click();
            this._router.navigate([this._router.url]);
          },
          error => {
            this.error = error;
            this.loading = false;
          });
      }
  }

  private onUpdateUserSubmit(){
    this.submitted = true;

    // stop here if form is invalid
    if (this.registerForm.invalid) {
        return;
      }

    this.loading = true;
    this._UserToUpdate.userName = this.formFields.username.value;
    this._UserToUpdate.passwordHash = this.formFields.password.value;
    this._UserToUpdate.email = this.formFields.email.value;
    this._UserToUpdate.firstName = this.formFields.firstName.value;
    this._UserToUpdate.lastName = this.formFields.lastName.value;
    this._UserToUpdate.gender = this.formFields.gender.value;


    this._registerService.updateUser(this._UserToUpdate.id, this._UserToUpdate)
        .pipe(first())
        .subscribe(
            data => {
                this._router.routeReuseStrategy.shouldReuseRoute = () => false;
                this._router.onSameUrlNavigation = 'reload';
                this.addOrUpdateModelCloseBtn.nativeElement.click();
                this._router.navigate([this._router.url]);
            },
            error => {
                this.errorMsg = error;
                this.loading = false;
            });
  }


  onAddOrUpdateSubmit(){
    if(this.actionName == "Add"){
      this.onAddUserSubmit();
    }else{
      this.onUpdateUserSubmit()
    }
  }

  openAddUserModal(){
    this.registerForm.setValue({
      username:"",
      password:"",
      email:"",
      firstName:"",
      lastName:"",
      gender:"Select Gender",
      //role:data.role
      role:"Select Role"
    }); 
    this.actionName = "Add";
  }

  openUpdateUserModal(userId){
    this.actionName = "Update";
    console.log(userId);
    this._registerService.getUserById(userId)
        .pipe(first())
        .subscribe(
            data => {
                this.registerForm.setValue({
                  username: data.userName,
                  password:data.passwordHash,
                  email:data.email,
                  firstName:data.firstName,
                  lastName:data.lastName,
                  gender:data.gender,
                  //role:data.role
                  role:UserRoles.User
                }); 
                this._UserToUpdate = data;
            },
            error => {
                this.errorMsg = error;
                this.loading = false;
            });
  }
  openDeleteUserModal(UserId:string){
    this.confirmModal.pointerToFunction = this._registerService.deleteUser;
    this.confirmModal.title = "Delete User";
    this.confirmModal.itemId = UserId;
    this.confirmModal.message = "Are you sure to delete this User";
    this.confirmModal.pageUrl = this._router.url;
  }

   // pagination
  counter(i: number) {
    return new Array(i);
  }

  getSelectedPage(currentPageNumber:number){
    this._registerService.getAccountsByPage(this.pageSize,currentPageNumber).subscribe(
      data => {
        this.allUsers = data
        this.currentPageNumber = currentPageNumber;
        this.hasUsers = (data.length != 0) ? true : false;
      },
      error=>
      {
       this.errorMsg = error;
      }
    ) 
  }

}

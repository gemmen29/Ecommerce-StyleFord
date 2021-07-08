import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { IColor } from 'src/app/_models/_interfaces/IColor';
import { ColorService } from 'src/app/_services/color.service';
import { environment } from 'src/environments/environment';
import { ConfirmModalComponent } from '../../_reusableComponents/confirm-modal/confirm-modal.component';

@Component({
  selector: 'app-colors',
  templateUrl: './colors.component.html',
  styleUrls: ['./colors.component.scss']
})
export class ColorsComponent implements OnInit {
  @ViewChild('addOrUpdateModelCloseBtn') addOrUpdateModelCloseBtn;
  @ViewChild(ConfirmModalComponent) confirmModal:ConfirmModalComponent;
  hasColors:boolean = false;
  private _colorToUpdate:IColor;
  allColors:IColor[]; 
  errorMsg:string;
  colorForm : FormGroup;
  loading = false;
  submitted = false;
  actionName:string;
  colorsCount:number;
  pageSize:number = 8;
  currentPageNumber:number = 1;
  numberOfPages:number; // colorsCount / pageSize

  // convenience getter for easy access to form fields
  get formFields() { return this.colorForm.controls; }
  constructor(private _colorService:ColorService,
    private _formBuilder: FormBuilder,
    private _router:Router) { }

  ngOnInit(): void {
    this.getColorsCount();
    this.colorForm = this._formBuilder.group({
      name:['', Validators.required]
    });
    this.getSelectedPage(1);
  }
  private getColorsCount(){
    this._colorService.getColorsCount().subscribe(
      data => {
        this.colorsCount = data
        this.numberOfPages = Math.ceil(this.colorsCount / this.pageSize)
      },
      error=>
      {
       this.errorMsg = error;
      }
    ) 
  }
  private onAddColorSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.colorForm.invalid) {
        return;
      }

    this.loading = true;
    let newColor:IColor = 
    {
      id:0 ,
      name : this.formFields.name.value,
    };
    this._colorService.addNewColor(newColor)
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

  private onUpdateColorSubmit(){
    this.submitted = true;

    // stop here if form is invalid
    if (this.colorForm.invalid) {
        return;
      }

    this.loading = true;
    this._colorToUpdate.name = this.formFields.name.value;
    this._colorService.updateColor(this._colorToUpdate.id, this._colorToUpdate)
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
      this.onAddColorSubmit();
    }else{
      this.onUpdateColorSubmit()
    }
  }
 
  openAddColorModal(){
    this.formFields.name.setValue("");
    this.actionName = "Add";
  }

  openUpdateColorModal(colorId){
    this.actionName = "Update";
    this._colorService.getColorById(colorId)
        .pipe(first())
        .subscribe(
            data => {
                this.colorForm.setValue({
                  name: data.name
                }); 
                this._colorToUpdate = data;
            },
            error => {
                this.errorMsg = error;
                this.loading = false;
            });
  }
  openDeleteColorModal(colorId){
    this.confirmModal.pointerToFunction = this._colorService.deleteColor
    this.confirmModal.title = "Delete Color";
    this.confirmModal.itemId = colorId;
    this.confirmModal.message = "Are you sure to delete this color";
    this.confirmModal.pageUrl = this._router.url;
  }

// pagination
  counter(i: number) {
    return new Array(i);
  }
  getSelectedPage(currentPageNumber:number){
    this._colorService.getColorsByPage(this.pageSize,currentPageNumber).subscribe(
      data => {
        this.allColors = data
        this.currentPageNumber = currentPageNumber;
        if(data.length != 0)
          this.hasColors = true;
        else
          this.hasColors = false;

      },
      error=>
      {
       this.errorMsg = error;
      }
    ) 
  }
}

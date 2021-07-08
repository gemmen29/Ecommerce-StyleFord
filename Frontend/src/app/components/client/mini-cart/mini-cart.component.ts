import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { first } from 'rxjs/operators';
import { IProductCartVM } from 'src/app/_models/_interfaces/IProductCartVM';
import { CartService } from 'src/app/_services/cart.service';
import { SharedService } from 'src/app/_services/shared.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-mini-cart',
  templateUrl: './mini-cart.component.html',
  styleUrls: ['./mini-cart.component.scss']
})
export class MiniCartComponent implements OnInit {
  productsCart: IProductCartVM[] = [];
  totalOrderPrice: number = 0;
  clickEventsubscription: Subscription;
  count:number=0;

  constructor(private _cartService: CartService,
    private _router: Router,
    private _sharedService: SharedService) {
    this.clickEventsubscription = this._sharedService
      .getClickEvent().subscribe(() => {
        this.getAllCart();
      })
  }
  
  ngOnInit(): void {
    this.getAllCart();
  }

  getAllCart() {
    this._cartService.getAll()
      .pipe(first())
      .subscribe(
        data => {
          this.productsCart = data
        },
        error => {
        });
  }

  public createImgPath = (serverPath: string) => {
    return `${environment.apiUrl}/${serverPath}`;
  }

}

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CartComponent } from './components/client/cart/cart.component';
import { CheckoutComponent } from './components/client/checkout/checkout.component';
import { ClientComponent } from './components/client/client.component';
import { HomeComponent } from './components/client/home/home.component';
import { ProductDetailsComponent } from './components/client/product-details/product-details.component';
import { SearchResultsComponent } from './components/client/search-results/search-results.component';
import { ShopComponent } from './components/client/shop/shop.component';
import { UserOrdersComponent } from './components/client/user-orders/user-orders.component';
import { WishlistComponent } from './components/client/wishlist/wishlist.component';
import { CategoriesComponent } from './components/dashboard/categories/categories.component';
import { ColorsComponent } from './components/dashboard/colors/colors.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { OrderComponent } from './components/dashboard/order/order.component';
import { ProductsComponent } from './components/dashboard/products/products.component';
import { UsersComponent } from './components/dashboard/users/users.component';
import { LoginComponent } from './components/login/login.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { RegisterComponent } from './components/register/register.component';
import { SettingComponent } from './components/setting/setting.component';
import { AuthGuard } from './_helpers/auth.guard';
import { UserRoles } from './_models/_enums/UserRoles';

const routes: Routes = [
  {path:'login', component: LoginComponent},
  {path:'register', component: RegisterComponent},
  {path:'setting' , component: SettingComponent, canActivate: [AuthGuard],},
  {
    path:'dashboard',
    component:DashboardComponent,
    canActivate: [AuthGuard],
    canActivateChild : [AuthGuard],
    data:{
      role: UserRoles.Admin
    },
    children:[
      {path: 'categories', component: CategoriesComponent},
      {path: 'orders', component: OrderComponent},
      {path: 'products', component: ProductsComponent},
      {path: 'users', component: UsersComponent},
      {path: 'colors', component: ColorsComponent},
    ]
  },
  {
    path:'', 
    component:ClientComponent,
    children:[
      {path: '', component: HomeComponent},
      {path: 'shop', component: ShopComponent},
      {path: 'cart', component: CartComponent, canActivate: [AuthGuard],},
      {path: 'wishlist', component: WishlistComponent, canActivate: [AuthGuard],},
      {path: 'checkout', component: CheckoutComponent, canActivate: [AuthGuard],},
      {path: 'search-results/:searchkeyword', component: SearchResultsComponent},
      {path: 'myorders', component: UserOrdersComponent,canActivate:[AuthGuard]},
      {path: 'product-details/:id', component: ProductDetailsComponent,},  

    ]
  },
  {path: '**', component: PageNotFoundComponent},
 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

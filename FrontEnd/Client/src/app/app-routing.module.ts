import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TestErrorComponent } from './core/test-error/test-error.component';
import { HomeComponent } from './home/home/home.component';
import { ProductDetailsComponent } from './shop/product-details/product-details.component';
import { ShopComponent } from './shop/shop/shop.component';

const routes: Routes = [
  {path:"home",component:HomeComponent},
  {path:"test-error",component:TestErrorComponent},
  {path:"shop",loadChildren:()=>import('./shop/shop.module').then(m=>m.ShopModule)},
  {path:"**",redirectTo:'',pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

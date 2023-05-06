import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ShopRoutingModule } from './shop-routing.module';
import { ShopComponent } from './shop/shop.component';
import { SharedModule } from '../shared/shared.module';
import { ShopitemComponent } from './shopitem/shopitem.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { RouterModule } from '@angular/router';


@NgModule({
  declarations: [
    ShopComponent,
    ShopitemComponent,
    ProductDetailsComponent
  ],
  imports: [
    CommonModule,
    ShopRoutingModule,
    SharedModule,
    RouterModule
    
  ],
  exports:[
    ShopComponent
  ]
})
export class ShopModule { }

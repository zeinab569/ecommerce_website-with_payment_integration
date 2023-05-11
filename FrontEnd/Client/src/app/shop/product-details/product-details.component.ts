import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { take } from 'rxjs';
import { BasketService } from 'src/app/basket/basket.service';
import { Product } from 'src/app/shared/models/product';
import { BreadcrumbService } from 'xng-breadcrumb';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {
  
  product?:Product;
  quentity=1;
  quentityInbBasket=0;

  constructor(
    private shopservices:ShopService,
    private activatesroute:ActivatedRoute,
    private bcservices:BreadcrumbService,
    private basketservices:BasketService
    ) {}

  ngOnInit(): void {
    this.LoadProduct()
  }
LoadProduct(){
   const id= this.activatesroute.snapshot.paramMap.get('id');
   if(id) this.shopservices.GetProduct(+id).subscribe({
     next:product=>{
       this.product=product;
       this.bcservices.set('@productDetails',product.name)
       this.basketservices.basktSource$.pipe(take(1)).subscribe({
         next:basket=>{
           const item = basket?.items.find(x=>x.id ===+id)
           if(item){
             this.quentity=item.quantity;
             this.quentityInbBasket=item.quantity;
            }
         }
       })
      },
     error:error=>console.log(error)
   })
}

increamentQuentity(){
  this.quentity++;
}
decreamentQuenttity(){
  this.quentity--;
}
updateBasket(){
  if(this.product){
    if(this.quentity > this.quentityInbBasket){
      const itemToAdd=this.quentity - this.quentityInbBasket;
      this.quentityInbBasket += itemToAdd;
      this.basketservices.addItemToBasket(this.product,itemToAdd);
    }else{
      const itemToRemove=this.quentityInbBasket-this.quentity;
      this.quentityInbBasket -= itemToRemove;
      this.basketservices.removeItemFromBasket(this.product.id,itemToRemove)
    }
  }

}

getText(){
  return this.quentityInbBasket === 0?"Add To Basket":"Update Basket"
}
}

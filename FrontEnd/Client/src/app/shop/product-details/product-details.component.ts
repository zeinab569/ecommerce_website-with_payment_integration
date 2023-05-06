import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {
  
  product?:Product;

  constructor(private shopservices:ShopService,private activatesroute:ActivatedRoute) {}

  ngOnInit(): void {
    this.LoadProduct()
  }
LoadProduct(){
   const id= this.activatesroute.snapshot.paramMap.get('id');
   if(id) this.shopservices.GetProduct(+id).subscribe({
     next:product=>this.product=product,
     error:error=>console.log(error)
   })
}
}

import { Component, Input, OnInit } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { Product } from '../../shared/models/product';

@Component({
  selector: 'app-shopitem',
  templateUrl: './shopitem.component.html',
  styleUrls: ['./shopitem.component.css']
})
export class ShopitemComponent implements OnInit{
 
  @Input() product?:Product
  
  constructor(private basketservices:BasketService) {}
  ngOnInit(): void {
   
  }
addItemtoBasket(){
  this.product&& this.basketservices.addItemToBasket(this.product)
}
  
}

import { Component, OnInit } from '@angular/core';
import { BasketItem } from 'src/app/shared/models/basket';
import { BasketService } from '../basket.service';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.css']
})
export class BasketComponent implements OnInit {
  
  constructor(public basketService:BasketService) {}

  ngOnInit(): void {
  
  }

  incrementQuentity(item:BasketItem){
    this.basketService.addItemToBasket(item)

  }

  removeItem(event:{id:number ,quentity:number}){
    this.basketService.removeItemFromBasket(event.id,event.quentity)
  }

}

import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';
import { BasketService } from 'src/app/basket/basket.service';
import { BasketItem } from 'src/app/shared/models/basket';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  
  constructor( public basketservices:BasketService,public accountServices:AccountService)  {}
  ngOnInit(): void {
   
  }
  
getCount(items:BasketItem[]){
  return items.reduce((sum,item)=>sum+item.quantity,0)
}
}

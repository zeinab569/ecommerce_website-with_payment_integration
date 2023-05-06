import { Component, Input, OnInit } from '@angular/core';
import { Product } from '../../shared/models/product';

@Component({
  selector: 'app-shopitem',
  templateUrl: './shopitem.component.html',
  styleUrls: ['./shopitem.component.css']
})
export class ShopitemComponent implements OnInit{
 
  @Input() product?:Product
  
  constructor() {}
  ngOnInit(): void {
    console.log(this.product?.pictureUrl)
    //throw new Error('Method not implemented.');
  }

  
}

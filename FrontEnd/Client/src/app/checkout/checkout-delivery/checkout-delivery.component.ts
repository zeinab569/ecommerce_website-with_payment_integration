import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { BasketService } from 'src/app/basket/basket.service';
import { DeliveryMethod } from 'src/app/shared/models/deliveryMethod';
import { CheckoutService } from '../checkout.service';

@Component({
  selector: 'app-checkout-delivery',
  templateUrl: './checkout-delivery.component.html',
  styleUrls: ['./checkout-delivery.component.css']
})
export class CheckoutDeliveryComponent implements OnInit {
  @Input() checkoutForm?:FormGroup;
  deliveryMethods:DeliveryMethod[]=[];
  constructor(private chekoutservices:CheckoutService , private basketservices:BasketService){}

  ngOnInit(): void {
    this.chekoutservices.getDeliveryMethods().subscribe({
      next:dm=>this.deliveryMethods=dm
    })
  }

  setShippingPrice(deliveryMethod:DeliveryMethod)
  {
    this.basketservices.setShippingPrice(deliveryMethod)
  }

}

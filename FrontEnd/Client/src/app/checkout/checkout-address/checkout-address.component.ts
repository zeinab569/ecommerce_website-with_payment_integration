import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/account/account.service';

@Component({
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html',
  styleUrls: ['./checkout-address.component.css']
})
export class CheckoutAddressComponent implements OnInit {
  @Input() checkoutForm?:FormGroup
  
  constructor(private accountservices:AccountService,private toastr:ToastrService){}
  ngOnInit(): void {
   
  }
saveUserAddress(){
  this.accountservices.updateUserAddress(this.checkoutForm?.get('addressForm')?.value).subscribe({
    next:()=>{
      this.toastr.success('Address Saved')
      this.checkoutForm?.get('addressForm')?.reset(this.checkoutForm?.get('addressForm')?.value)
    }
  })
}
}

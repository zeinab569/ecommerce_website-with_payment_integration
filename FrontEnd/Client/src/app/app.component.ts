import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from './account/account.service';
import { BasketService } from './basket/basket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Store App';
  constructor(private http:HttpClient , private basketserbvice:BasketService,private accountServices:AccountService) { }
  products:any[]=[];

  ngOnInit(): void {
    this.http.get("https://localhost:7215/api/Product").subscribe({

      next:(response:any)=>this.products= response.data,
      error:error=>console.error(error),
      complete:()=>{
        console.log("completed")
      }
    })
    const basketId= localStorage.getItem("basket_Id");
    if(basketId) this.basketserbvice.getBasket(basketId)   
  }

  loadCurrentUser(){
    const token = localStorage.getItem('token');
    this.accountServices.loadCurrentUser(token).subscribe();
  }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Basket, BasketItem, BasketToltals } from '../shared/models/basket';
import { DeliveryMethod } from '../shared/models/deliveryMethod';
import { Product } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  baseurl=environment.apiUrl;
  private basktSource= new BehaviorSubject<Basket | null>(null);
  basktSource$=this.basktSource.asObservable();

  private basketTotalSourse=new BehaviorSubject<BasketToltals |null>(null);
  basketTotalSourse$= this.basketTotalSourse.asObservable()

  shipping=0;

  constructor(private http:HttpClient) { }

  
  getBasket(id:string){
    return this.http.get<Basket>(this.baseurl+'basket?id=').subscribe({
      next:basket=>
      {
        this.basktSource.next(basket),
        this.calculateTotals()
      },
      
    })
  }

  setBasket(basket:Basket){
    return this.http.post<Basket>(this.baseurl+'basket',basket).subscribe({
      next:basket=>{
        this.basktSource.next(basket),
        this.calculateTotals()
      }
    })
  }

  setShippingPrice(deliverymethod:DeliveryMethod){
    this.shipping=deliverymethod.price
    this.calculateTotals();
  }
 removeItemFromBasket(id:number,quantity=1)
 {
   const basket = this.getCurrentBasketValue();
   if(!basket) return
   const item = basket.items.find(x=>x.id === id)
   if(item){
     item.quantity -= quantity;
     if(item.quantity===0){
       basket.items=basket.items.filter(x=>x.id != id) // delete this item 
     }
     if(basket.items.length >0 ) this.setBasket(basket)
     else
       this.deleteBasket(basket)
   }

 }

  deleteBasket(basket: Basket) {
    return this.http.delete(this.baseurl+'basket/?id'+basket.id).subscribe({
      next:()=>{
       this.deleteLocalBasket()
      }
    })
  }
 
  deleteLocalBasket(){
    this.basktSource.next(null),
    this.basketTotalSourse.next(null),
    localStorage.removeItem('basket_Id')
  }

  getCurrentBasketValue(){
   return this.basktSource.value;
  }

  addItemToBasket(item: Product | BasketItem,quentity=1){
    if(this.isProduct(item)) item = this.mapProductItemtoBasketItem(item);
    const basket= this.getCurrentBasketValue() ?? this.createBasket();
    basket.items=this.addOrUpdateItem(basket.items, item,quentity);
    this.setBasket(basket);
  }

  addOrUpdateItem(items:BasketItem[],itemToAdd:BasketItem,quantity:number): BasketItem[] {
     const item = items.find(x=>x.id === itemToAdd.id);
     if(item) item.quantity += quantity;
     else{
       itemToAdd.quantity=quantity;
       items.push(itemToAdd);
     }
     return items;
  }

  createBasket(): Basket  {
   const basket=new Basket();
   localStorage.setItem("basket_Id",basket.id)
   return basket;
  }

private  mapProductItemtoBasketItem(item: Product) {
    return{
      id:item.id,
      productName:item.name,
      price:item.price,
      quantity:0,
      pictureUrl:item.pictureUrl,
      brand:item.ProductBrand,
      type:item.ProductType
    }
  }

  private calculateTotals(){
    const basket=this.getCurrentBasketValue();
    if(!basket) return;
    const subtotal=basket.items.reduce((a,b)=>(b.price*b.quantity)+a,0)
    const total = this.shipping+subtotal;
    this.basketTotalSourse.next({shipping:this.shipping,total,subtotal})
  }

  //product or basket item
  private isProduct(item:Product|BasketItem) :item is Product
  {
   return (item as Product).ProductBrand!==undefined 
  }
}

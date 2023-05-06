import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Brand } from 'src/app/shared/models/brands';
import { Product } from 'src/app/shared/models/product';
import { shopParams } from 'src/app/shared/models/shopParams';
import { Type } from 'src/app/shared/models/types';
import {ShopService} from 'src/app/shop/shop.service'

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent implements OnInit{
@ViewChild('search')searchTerms?:ElementRef;

 products:Product[]=[];
 brands:Brand[]=[];
 types:Type[]=[];
 totalCount=0;
shopparams=new shopParams();

 sortOptions=[
   {name:"Alphabetical",value:'name'},
   {name:'price : Low to high',value:'priceAsc'},
   {name:'price High to low',value:'priceDes'}
 ]
  constructor(private ShopService:ShopService) { }

  ngOnInit(): void {
    this.GetProducts();
    this.Getbrands();
    this.Gettypes();
  }

  GetProducts(){
    this.ShopService.getproducts(this.shopparams).subscribe({
      next:response=>{
        this.products=response.data,
        this.shopparams.pageNumber=response.pageIndex,
        this.shopparams.pageSize=response.pageSize,
        this.totalCount=response.count
      },
      error:error=>console.log(error)
    })
  }
  Gettypes(){
    this.ShopService.GetTypes().subscribe({
      next:response=>this.types=[{id:0,name:"All"},...response],
      error:error=>console.log(error)
    })

  }

  Getbrands(){
    this.ShopService.GetBrands().subscribe({
      next:response=>this.brands=[{id:0,name:"All"},...response],
      error:error=>console.log(error)
    })
  }

OnBrandSelected(brandid:number){
  this.shopparams.brandId=brandid;
  this.shopparams.pageNumber=1;
  this.GetProducts()
}

OnTypeSelected(typeid:number){
  this.shopparams.typeId=typeid;
  this.shopparams.pageNumber=1;
  this.GetProducts()

}

OnSortSelected(event:any){
   this.shopparams.sort=event.target.value;
   this.GetProducts()
 }

 OnPageChanged(event:any){
   if(this.shopparams.pageNumber != event.page){
     this.shopparams.pageNumber=event.page;
     this.GetProducts();
   }
 }

 OnSearch(){
   this.shopparams.search=this.searchTerms?.nativeElement.value;
   this.shopparams.pageNumber=1;
   this.GetProducts();
 }

 OnReset(){
   if(this.searchTerms)this.searchTerms.nativeElement.value="";
   this.shopparams = new shopParams()
   this.GetProducts();

 }
}

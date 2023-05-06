import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Brand } from '../shared/models/brands';
import { Pagination } from '../shared/models/paging';
import { Product } from '../shared/models/product';
import { shopParams } from '../shared/models/shopParams';
import { Type } from '../shared/models/types';


@Injectable({
  providedIn: 'root'
})
export class ShopService {

  constructor(private http:HttpClient) { }
  baseurl="https://localhost:7215/api/";

  getproducts(shopparams:shopParams){
    let params=new HttpParams();
    if(shopparams.brandId>0) params=params.append("brandId",shopparams.brandId);
    if(shopparams.typeId>0) params=params.append("brandId",shopparams.typeId);
    params=params.append("sort",shopparams.sort);
    params=params.append("PageIndex",shopparams.pageNumber);
    params=params.append("PageSize",shopparams.pageSize);
    if(shopparams.search) params=params.append("search",shopparams.search);
    return this.http.get<Pagination<Product[]>>(this.baseurl +'product',{params:params})
  }

  GetBrands(){
    return this.http.get<Brand[]>(this.baseurl+'product/brands')
  }
  GetTypes(){
    return this.http.get<Type[]>(this.baseurl+'product/types')
  }
  GetProduct(id:number){
    return this.http.get<Product>(this.baseurl+'product/'+id)
  }
}

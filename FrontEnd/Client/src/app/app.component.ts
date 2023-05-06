import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Store App';
  constructor(private http:HttpClient) { }
  products:any[]=[];

  ngOnInit(): void {
    this.http.get("https://localhost:7215/api/Product").subscribe({

      next:(response:any)=>this.products= response.data,
      error:error=>console.error(error),
      complete:()=>{
        console.log("completed")
      }
    });
   
  }
}

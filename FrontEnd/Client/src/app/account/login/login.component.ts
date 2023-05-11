import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{
  returnUrl:string="";
  
  constructor(private accountServices:AccountService,private router:Router,private activateroute:ActivatedRoute) {
    this.returnUrl=this.activateroute.snapshot.queryParams['returnUrl'] || '/shop';
  }
  loginForm= new FormGroup({
    email:new FormControl('',Validators.required),
    password:new FormControl('',Validators.required)
  })
  
  ngOnInit(): void {
    
  }

  onSubmit(){
   // console.log(this.loginForm.value)
   this.accountServices.login(this.loginForm.value).subscribe({
     next:user=>this.router.navigateByUrl(this.returnUrl)
   })
  }
}

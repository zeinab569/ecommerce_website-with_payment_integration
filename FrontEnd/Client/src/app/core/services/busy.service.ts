import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
 busyRequestcount=0;
  constructor(private spinnerservice:NgxSpinnerService) { }
  busy(){
    this.busyRequestcount++;
    this.spinnerservice.show(undefined,{
      type:'timer',
      bdColor:'rgba(255,255,255,0.7)',
      color:'#3333333'
    })
  }
  idle(){
    this.busyRequestcount--;
    if(this.busyRequestcount<=0){
      this.spinnerservice.hide()
    }
  }
}

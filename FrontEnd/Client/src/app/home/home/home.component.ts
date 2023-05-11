import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  myInterval = 1500;
  activeSlideIndex = 0;
  slides: {image: string; text?: string}[] = [
    {image: 'assets/images/home1.png'},
    {image: 'assets/images/home2.png'},
    {image: 'assets/images/home3.png'}
  ];

}

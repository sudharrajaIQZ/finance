import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { HeaderComponent } from './component/header/header.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderComponent,CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'frontend';
  showHeader:boolean= true;
  constructor(private router:Router){}
  ngOnInit(){
    this.router.events.subscribe(event=>{
      if(event instanceof NavigationEnd){
        this.showHeader = event.url !=='/auth/login';
      }
    });
  }
}

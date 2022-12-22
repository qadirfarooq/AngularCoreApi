import { Component, OnInit } from '@angular/core';
import { RouteReuseStrategy } from '@angular/router';
import { Observable } from 'rxjs';
import { User } from '../_modal/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any={

  }

  constructor( public accountService : AccountService) { }

  ngOnInit(): void {

  }
  login(){
    this.accountService.login(this.model).subscribe(res => {
      console.log(res);
      //this.loggedIn = true;
    }, error =>{
      console.log(error);
    } )
    console.log(this.model)
  }

  logout(){
    this.accountService.logout();
   // this.loggedIn =false;
  }

  // getCurrentUSer(){
  //   this.accountService.currentuser$.subscribe(user =>{
  //     this.loggedIn = !!user;
  //   }, error =>{
  //     console.log(error)
  //   }
  //   )
  // }
}

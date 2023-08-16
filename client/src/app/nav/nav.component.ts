import { Component, OnInit } from '@angular/core';
import { Router, RouteReuseStrategy } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
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

  constructor( public accountService : AccountService , private router:Router, private toastr: ToastrService){ }

  ngOnInit(): void {

  }
  login(){
    this.accountService.login(this.model).subscribe(res => {
      //console.log(res);
      //this.loggedIn = true;
      this.router.navigateByUrl('/members');
    },
    error =>{
      this.toastr.error(error.error);
      console.log(error);
    }

    )
    //console.log(this.model)
  }

  logout(){
    this.accountService.logout();
    this.router.navigateByUrl('/');
   // this.loggedIn =false;
  }

  // getCurrentUSer(){
  //   this.accountService.currentUser$.subscribe(user =>{
  //     this.loggedIn = !!user;
  //   }, error =>{
  //     console.log(error)
  //   }
  //   )
  // }
}

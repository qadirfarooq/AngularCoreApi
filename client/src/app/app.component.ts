import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_modal/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'My New Angular Application';
  users :any;
  constructor(private htpp: HttpClient, private accountService : AccountService){}
  ngOnInit() {
    // this.getUsers();
    this.setCurrentUser();
  }


  setCurrentUser(){
    const user: User= JSON.parse(localStorage.getItem('user'));
    console.log(' app user ' + user)// this is un defined
    this.accountService.setCurrentUser(user);
  }

}

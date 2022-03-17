import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'My New Angular Application';
  users :any;
  constructor(private htpp: HttpClient){}
  ngOnInit() {
    this.getUsers();
  }
  getUsers()
  {
    this.htpp.get('https://localhost:7290/api/users').subscribe(response =>{
      this.users=response;
    },error => {
      console.log(error)
    });
  }
}

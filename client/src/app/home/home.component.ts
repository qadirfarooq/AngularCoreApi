import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  registerMode=false;
  users: any;

  constructor (private http: HttpClient){}

  ngOnInit(): void {
    // this.getUsers();
  }
registerToggle(){
  this.registerMode = !this.registerMode;
}

CancelRegisterMode(event : boolean){
  this.registerMode = event;
}

// getUsers()
// {
//   this.http.get('https://localhost:7290/api/users').subscribe(response =>{
//     this.users=response;
//   },error => {
//     console.log(error)
//   });
// }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {map} from 'rxjs/operators'
import { User } from '../_modal/user';
import { ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseURl =environment.apiUrl;
  private currentUserSource = new ReplaySubject<User>(1);


  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http:HttpClient) { }

  login(model: any){
    return this.http.post<User>(this.baseURl +'account/login',model).pipe(
      map( (response: User) =>{
        const user = response;
        if(user){
          // localStorage.setItem('user',JSON.stringify(user));
          // this.currentUserSource.next(user);
          this.setCurrentUser(user);
        }
      })
    )
  }
  Register(model: any){
    return this.http.post<User>(this.baseURl +'account/register',model).pipe(
      map( (response: User) =>{
        const user = response;
        if(user){
          //localStorage.setItem('user',JSON.stringify(user));
          //this.currentUserSource.next(user);
          this.setCurrentUser(user);
        }
        return user;
      })
    )
  }

  setCurrentUser(user : User)
  {
    localStorage.setItem('user',JSON.stringify(user));
    this.currentUserSource.next(user)

  }
  logout(){
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }



}

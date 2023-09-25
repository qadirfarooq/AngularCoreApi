import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from 'src/app/_modal/member';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class MembersService {

  baseUrl = environment.apiUrl;
  members: Member[] = [];

  constructor( private http: HttpClient) { }

getMembers ()
{
  if(this.members.length>0) return  of (this.members);

  return this.http.get <Member []> (this.baseUrl + 'users/').pipe(
    map(mem => {
      this.members = mem;
      return this.members;
    })
  )
  //return this.http.get <Member []> (this.baseUrl + 'users/', this.getHttpOptions())
}
getMember( username : string )
{
  const member  = this.members.find(x=> x.userName === username);
  if (member) return of (member);
  //console.log(' at member service the user is ' + username)
  return this.http.get <Member> (this.baseUrl + 'users/' + username);
  //return this.http.get <Member> (this.baseUrl + 'users/' + username, this.getHttpOptions());
}

updateMember(member: Member)
{
  return this.http.put(this.baseUrl+'users', member).pipe(
    map(()=> {
      const index = this.members.indexOf(member);
      this.members[index] = {...this.members[index], ... member}
    })
  );
}



setMainPhoto(photId: number)
{
  return this.http.put(this.baseUrl + 'users/set-main-photo/' + photId, {});
}

deletePhoto(photId: number)
{
   return this.http.delete(this.baseUrl+ 'users/delete-photo/' + photId)
}



  // getHttpOptions ()
  // {
  //   const userString = localStorage.getItem('user');

  //   if (!userString) return;

  //   const user= JSON.parse(userString);

  //   return {
  //     headers :  new HttpHeaders ({
  //     Authorization : 'Bearer ' +user.token
  //     })
  //   }

  // }
}

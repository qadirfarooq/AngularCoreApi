import { ReturnStatement } from '@angular/compiler';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Toast, ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  // canActivate(
  //   route: ActivatedRouteSnapshot,
  //   state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
  //   return true;
  // }

  constructor (private accountService: AccountService, private toastr : ToastrService){

  }
  canActivate(): Observable<boolean> {
    return this.accountService.currentuser$.pipe(map(user => {
      if(user)
      return true;
      else{
        this.toastr.error('you cant proceed');
        return false;
      }
    }))
  }
}

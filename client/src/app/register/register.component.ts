import { Component, Input, OnInit, Output,EventEmitter } from '@angular/core';
import { AccountService } from '../_services/account.service';
// import { EventEmitter } from 'stream';
//mport {EventEmitter } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  // @Input() usersFromHomecomponents: any;
  @Output() cancelRegister = new EventEmitter();
 model: any = {

 }
  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }
register(){
  console.log(this.model)
  this.accountService.Register(this.model).subscribe({
    next: response => {
      console.log(response)
      this.cancel();
    },
    error :error =>
    console.log(error)
    })
 // console.log(this.model)
}
cancel(){
  this.cancelRegister.emit(false);
  console.log('cancel')
}
}

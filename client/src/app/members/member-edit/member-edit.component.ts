import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { Member } from 'src/app/_modal/member';
import { User } from 'src/app/_modal/user';
import { MembersService } from 'src/app/_services/members.service';
import { AccountService  } from 'src/app/_services/account.service';
import { take } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';
@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editForm') editForm:  NgForm | undefined;
  @HostListener ('window:beforeunload',['$event']) unloadNotification($event:any){
    $event.returnValue = true;
  }
member: Member | undefined;
user: User | null = null;


  constructor( private accountService: AccountService,
    private memberService: MembersService ,private route: ActivatedRoute,
    private toaster :  ToastrService) {
    console.log(this.accountService.currentUser$);
    this.accountService.currentUser$.pipe(take(1)).subscribe({

      next: user => this.user = user
    })

    console.log(' current user ' + this.user.userName)
   }

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember0(){
    const username = this.route.snapshot.paramMap.get('username');
    console.log(' member user name ' + username)
    if(!username) return ;
    this.memberService.getMember(this.route.snapshot.paramMap.get('username')).subscribe({
      next: mem=> {
        this.member = mem;
        //this.galleryImages = this.getImages();
      }
    })
  }


loadMember(){
  if(!this.user) {
    console.log('no user')
    return;
  }
  this.memberService.getMember(this.user.userName).subscribe({
    next: member => this.member = member
  })

}

updateMember(){
  this.memberService.updateMember(this.editForm?.value).subscribe({
    next: _ => {
      //console.log(this.member)
      this.toaster.success(' profile updated success  ');
      this.editForm?.reset(this.member);
    }
  })

}
}

import { Component, OnInit } from '@angular/core';
import { Member } from 'src/app/_modal/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {

  members : Member[] = []
  constructor(private memberService : MembersService) { }

  ngOnInit(): void {
    this.localMembers();
  }
localMembers()
{
  this.memberService.getMembers().subscribe({
    next: mem => this.members = mem
  })
}
}

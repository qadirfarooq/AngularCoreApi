import { Component, Input, OnInit } from '@angular/core';
import { Member } from 'src/app/_modal/member';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})

// member card is child to member list. to pass data to child we use @input
export class MemberCardComponent implements OnInit {
  @Input() member: Member | undefined;


  constructor() { }

  ngOnInit(): void {
  }

}

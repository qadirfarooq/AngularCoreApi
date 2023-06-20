import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
// import { NgxGalleryImage } from '@kolkov/ngx-gallery/lib/ngx-gallery-image';
// import { NgxGalleryOptions } from '@kolkov/ngx-gallery/lib/ngx-gallery-options';
// import { NgxGalleryAnimation } from '@kolkov/ngx-gallery/public-api';
import { Member } from 'src/app/_modal/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  member : Member | undefined;
  //galleryOptions: NgxGalleryOptions [] = [];
  //galleryImages: NgxGalleryImage [] = [];

  constructor( private memberService : MembersService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadMember();

    // this.galleryOptions = [
    //   {
    //     width: '600px',
    //     height: '400px',
    //     imagePercent: 100,
    //     thumbnailsColumns: 4,
    //     imageAnimation: NgxGalleryAnimation.Slide
    //   },
    //   // max-width 800
    //   {
    //     breakpoint: 800,
    //     width: '100%',
    //     height: '600px',
    //     imagePercent: 80,
    //     thumbnailsPercent: 20,
    //     thumbnailsMargin: 20,
    //     thumbnailMargin: 20
    //   },
    //   // max-width 400
    //   {
    //     breakpoint: 400,
    //     preview: false
    //   }
    // ];

  }
  getImages(){
    if(!this.member) return [];
    const imageUrls = [];
    for(const photo of this.member.photos)
    {
      imageUrls.push({
        small: photo.url,
        medium: photo.url,
        big: photo.url,
      })
    }
    return imageUrls;
  }
  loadMember(){
    const username = this.route.snapshot.paramMap.get('username');
    if(!username) return ;
    this.memberService.getMember(this.route.snapshot.paramMap.get('username')).subscribe({
      next: mem=> {
        this.member = mem;
        //this.galleryImages = this.getImages();
      }
    })
  }
}

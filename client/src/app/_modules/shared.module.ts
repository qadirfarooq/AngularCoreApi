//import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';
import { TabsModule } from 'ngx-bootstrap/tabs';
//import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from "@angular/core";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
// Import library module
import { NgxSpinnerModule } from "ngx-spinner";

interface NgxSpinnerConfig {
  type?: 'ball-scale-multiple';
}
@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    ToastrModule.forRoot({
      positionClass : 'toast-bottom-right'
    }),
    NgxSpinnerModule,



    //NgxGalleryModule
  ],
  exports:[
    BsDropdownModule,
    ToastrModule,
    TabsModule,
    NgxSpinnerModule,


    //NgxGalleryModule
  ],
})
export class SharedModule { }

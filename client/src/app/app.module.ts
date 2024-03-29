import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
//import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
//import { ToastrModule  } from 'ngx-toastr';
import { SharedModule } from './_modules/shared.module';
import { TestErrorComponent } from './errors/test-error.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { NotfoundComponent } from './errors/notfound.component';
import { ServerErrorComponent } from './errors//server-error.component';
import { MemberCardComponent } from './members/member-card/member-card.component';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { PhotoEditorComponent } from './members/photo-editor/photo-editor.component';
import { TextInputComponent } from './_forms/text-input/text-input.component';
import { DatePickerComponent } from './_forms/date-picker/date-picker.component';
//import { AngularFontAwesomeModule } from 'angular-font-awesome';
@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    MemberListComponent,
    MemberDetailComponent,
    ListsComponent,
    MessagesComponent,
    TestErrorComponent,
    NotfoundComponent,
    ServerErrorComponent,
    MemberCardComponent,
    MemberEditComponent,
    PhotoEditorComponent,
    TextInputComponent,
    DatePickerComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    SharedModule,
    ReactiveFormsModule
    //AngularFontAwesomeModule

  ],
  providers: [
    {provide : HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    {provide : HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    {provide : HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

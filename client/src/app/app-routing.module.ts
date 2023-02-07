import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';
import {TestErrorComponent} from './errors/test-error.component';
import { AuthGuard } from './_guards/auth.guard';
import { NotfoundComponent } from './errors/notfound.component';
import { ServerErrorComponent } from './errors/server-error.component';
const routes: Routes = [
  {path:'', component : HomeComponent},
  {path:'',
      runGuardsAndResolvers : 'always',
      canActivate : [AuthGuard],
      children: [
        {path:'members', component : MemberListComponent},
        {path:'members/:id', component : MemberDetailComponent},
        {path:'lists', component :ListsComponent },
        {path:'messeage', component : MessagesComponent}, {path:'errors', component : TestErrorComponent},
        {path:'not-found', component : NotfoundComponent},
        {path:'server-error', component : ServerErrorComponent},
        {path:'**', component : HomeComponent,pathMatch: 'full'},
      ]
  },



  {path:'', component : HomeComponent},

   {path:'', component : HomeComponent},
  // {path:'', component : HomeComponent},
  // {path:'', component : HomeComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

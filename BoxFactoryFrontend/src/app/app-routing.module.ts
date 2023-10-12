import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import {App} from "@capacitor/app";
import {BoxFeedComponent} from "./boxFeed.component";

const routes: Routes = [
  {
    path: '',
    component: BoxFeedComponent
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }

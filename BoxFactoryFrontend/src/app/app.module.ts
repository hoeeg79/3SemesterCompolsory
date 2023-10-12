import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {RouteReuseStrategy, RouterModule} from '@angular/router';

import { IonicModule, IonicRouteStrategy } from '@ionic/angular';

import { AppRoutingModule } from './app-routing.module';
import {HttpClientModule} from "@angular/common/http";
import {ReactiveFormsModule} from "@angular/forms";
import {CreateboxComponent} from "./createbox.component";
import {UpdateboxComponent} from "./updatebox.component";
import {ShowBoxComponent} from "./showBox.component";
import {AppComponent} from "./app.component";

@NgModule({
  declarations: [CreateboxComponent, UpdateboxComponent, ShowBoxComponent],
  imports: [BrowserModule, IonicModule.forRoot({mode: "ios"}),
    AppRoutingModule, HttpClientModule, ReactiveFormsModule,
  RouterModule],
  exports: [RouterModule],
  providers: [{ provide: RouteReuseStrategy, useClass: IonicRouteStrategy }],
  bootstrap: [AppComponent]
})
export class AppModule {}

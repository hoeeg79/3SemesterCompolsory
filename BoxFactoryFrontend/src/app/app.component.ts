
import { Component } from '@angular/core';
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {ModalController, ToastController} from "@ionic/angular";
import {FormBuilder, FormControl, FormGroup} from "@angular/forms";
import {State} from "../state";
import {firstValueFrom} from "rxjs";
import {BoxItem} from "../models";
import {environment} from "../environments/environment.prod";
import {CreateboxComponent} from "./createbox.component";
import {UpdateboxComponent} from "./updatebox.component";

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
})
export class AppComponent {
  searchBoxForm: FormGroup;
  searchOnGoing: boolean = false;
  constructor(public http: HttpClient, public modalController: ModalController, public formBuilder: FormBuilder,
              public state: State, public toastController: ToastController) {
    this.searchBoxForm = this.formBuilder.group({searchQuery: new FormControl(""),
    });
  }

  async search(event: any){
    if (event && event.target) {
      const searchQuery = event.target.value;
      if (searchQuery === '' && this.searchOnGoing) {
        this.fetchBoxes();
      } else if (searchQuery >= 4) {
        const result = await firstValueFrom(this.http.get<BoxItem[]>(environment.baseUrl + "/api/boxes?searchterm=" +
        searchQuery));
        this.state.boxItems = result!;
        this.searchOnGoing = true;
      } else if (this.searchOnGoing && searchQuery.length < 4){
        this.fetchBoxes();
        this.searchOnGoing = false;
      }
    }
  }

  clearSearchInput() {
    this.searchBoxForm.get('searchQuery')!.setValue('');
  }

  async fetchBoxes() {
    const result = await firstValueFrom(this.http.get<BoxItem[]>(environment.baseUrl + "/api/catalogue/"));
    this.state.boxItems = result!;
  }

  async deleteBox(boxId: number | undefined) {
    try {
      await firstValueFrom(this.http.delete(environment.baseUrl + "/api/deletebox/" + boxId));
      this.state.boxItems = this.state.boxItems.filter(box => box.boxId != boxId);
    } catch (e) {
      if (e instanceof HttpErrorResponse) {
        this.toastController.create({message: e.error.messageToClient}).then(res => res.present)
      };
    }
  }

  async updateBox(boxId: number | undefined) {
    const modal = await this.modalController.create({
      component: UpdateboxComponent
    });
    modal.present;
  }

  async openModal() {
    const modal = await this.modalController.create({
      component: CreateboxComponent
    });
    modal.present;
  }
}

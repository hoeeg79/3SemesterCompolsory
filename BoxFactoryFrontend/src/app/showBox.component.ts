import {Component} from "@angular/core";
import {State} from "../state";
import {UpdateboxComponent} from "./updatebox.component";
import {ModalController, ToastController} from "@ionic/angular";
import {firstValueFrom} from "rxjs";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";

@Component({
  templateUrl: 'showBox.html',
  styleUrls: ['showBox.component.css'],
})

export class ShowBoxComponent{
  constructor(public state: State, public modalController: ModalController, public toastController: ToastController,
              public http: HttpClient) {
  }

  async updateBox(boxItem: any) {
    const modal = await this.modalController.create({
      component: UpdateboxComponent
    });
    this.state.currentBox = boxItem!;
    modal.present();
    this.modalController.dismiss();
  }

  async deleteBox(boxId: any) {
    try {
      await firstValueFrom(this.http.delete("https://boxfactoryupload.azurewebsites.net/api/deletebox/" + boxId));
      this.state.boxItems = this.state.boxItems.filter(box => box.boxId != boxId);
    } catch (e) {
      if (e instanceof HttpErrorResponse) {
        this.toastController.create({message: e.error.messageToClient}).then(res => res.present)
      };
    }
    this.modalController.dismiss();
  }
}

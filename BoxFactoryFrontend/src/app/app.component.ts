
import {Component, OnInit} from '@angular/core';
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {ModalController, ToastController} from "@ionic/angular";
import {FormBuilder, FormControl, FormGroup} from "@angular/forms";
import {State} from "../state";
import {firstValueFrom} from "rxjs";
import {BoxItem} from "../models";
import {CreateboxComponent} from "./createbox.component";
import {UpdateboxComponent} from "./updatebox.component";
import {ShowBoxComponent} from "./showBox.component";

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component2.css'],
})
export class AppComponent implements OnInit{
  searchBoxForm: FormGroup;
  searchOnGoing: boolean = false;
  themeToggle = false;
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
        this.searchOnGoing = false
      } else if (searchQuery.length >= 4) {
        const result = await firstValueFrom(this.http.get<BoxItem[]>("http://localhost:5000/api/boxes?searchterm=" +
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
    const result = await firstValueFrom(this.http.get<BoxItem[]>("http://localhost:5000/api/catalogue"));
    console.log(result);
    this.state.boxItems = result!;
  }

  async deleteBox(boxId: number | undefined) {
    try {
      await firstValueFrom(this.http.delete("http://localhost:5000/api/deletebox/" + boxId));
      this.state.boxItems = this.state.boxItems.filter(box => box.boxId != boxId);
    } catch (e) {
      if (e instanceof HttpErrorResponse) {
        this.toastController.create({message: e.error.messageToClient}).then(res => res.present)
      };
    }
  }

  async updateBox(box: BoxItem | undefined) {
    const modal = await this.modalController.create({
      component: UpdateboxComponent
    });
    this.state.currentBox = box!;
    modal.present();
  }

  async createBox() {
    const modal = await this.modalController.create({
      component: CreateboxComponent
    });
    modal.present();
  }

  async openBox(boxId: number | undefined){
    const modal = await this.modalController.create({
      component: ShowBoxComponent
    });
    this.state.currentBox = await firstValueFrom(this.http.get<BoxItem>("http://localhost:5000/api/boxes/" + boxId));

    modal.present();
  }

  ngOnInit(): void {
    this.fetchBoxes();
  }

  toggleTheme() {
    window.open("https://www.youtube.com/watch?v=dQw4w9WgXcQ&pp=ygUXbmV2ZXIgZ29ubmEgZ2l2ZSB5b3UgdXA%3D")
  }
}

import {Component} from "@angular/core";
import {FormBuilder, Validators} from "@angular/forms";
import {ModalController, ToastController} from "@ionic/angular";
import {HttpClient} from "@angular/common/http";
import {State} from "../state";
import {BoxItem} from "../models";


@Component({
  templateUrl: 'createBox.html',

})

export class CreateboxComponent {
  createNewBoxForm = this.fb.group( {
    name: ['', Validators.required],
    size: ['', Validators.required],
    description: ['', Validators.required],
    price: ['', Validators.required],
    materials: ['', Validators.required],
    boxImgUrl: ['', Validators.required]
  })


  constructor(public fb: FormBuilder, public modalController: ModalController, public http: HttpClient,
              public state: State, public toastController: ToastController) {
  }

 async submit() {
    try {
      let dto = this.createNewBoxForm.getRawValue() as BoxItem;
      const observale

    } catch (e) {

    }

  }
}

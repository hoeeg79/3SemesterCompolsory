import {Component, OnInit} from "@angular/core";
import {FormBuilder, Validators} from "@angular/forms";
import {ModalController, ToastController} from "@ionic/angular";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {State} from "../state";
import {BoxItem} from "../models";
import {environment} from "../environments/environment.prod";
import {firstValueFrom, observable} from "rxjs";


@Component({
    templateUrl: 'updateBox.html',
    styleUrls: ['updateBox.component.css']

})

export class UpdateboxComponent implements OnInit{
    updateBoxForm = this.fb.group( {
        name: [this.state.currentBox.name, Validators.required],
        size: [this.state.currentBox.size, Validators.required],
        description: [this.state.currentBox.description, Validators.required],
        price: [this.state.currentBox.price, [Validators.required, Validators.min(0)]],
        materials: [this.state.currentBox.materials, Validators.required],
        boxImgUrl: [this.state.currentBox.boxImgUrl, Validators.required]
    })


    constructor(public fb: FormBuilder, public modalController: ModalController, public http: HttpClient,
                public state: State, public toastController: ToastController) {
    }

    async submit() {
        try {
            let dto = this.updateBoxForm.getRawValue();
            const observable = this.http.put<BoxItem>('http://localhost:5000/api/boxes/' + this.state.currentBox.boxId, dto);
            const response = await firstValueFrom(observable);
            this.state.boxItems = this.state.boxItems.filter(box => box.boxId != this.state.currentBox.boxId);
            this.state.boxItems.push(response);
            this.modalController.dismiss();
        } catch (e) {
            if (e instanceof HttpErrorResponse) {
                this.toastController.create({message: e.error.messageToClient}).then(res => res.present)
            };
        }
    }

  ngOnInit(): void {
  }
}

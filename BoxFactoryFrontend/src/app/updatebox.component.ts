import {Component} from "@angular/core";
import {FormBuilder, Validators} from "@angular/forms";
import {ModalController, ToastController} from "@ionic/angular";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {State} from "../state";
import {BoxItem} from "../models";
import {environment} from "../environments/environment.prod";
import {firstValueFrom} from "rxjs";


@Component({
    templateUrl: 'updateBox.html',

})

export class UpdateboxComponent {
    updateBoxForm = this.fb.group( {
        name: ['', Validators.required],
        size: ['', Validators.required],
        description: ['', Validators.required],
        price: [undefined, [Validators.required, Validators.min(0)]],
        materials: ['', Validators.required],
        boxImgUrl: ['', Validators.required]
    })


    constructor(public fb: FormBuilder, public modalController: ModalController, public http: HttpClient,
                public state: State, public toastController: ToastController) {
    }

    async submit() {
        try {
            let dto = this.updateBoxForm.getRawValue();
            const observable = this.http.put<BoxItem>(environment.baseUrl + '/api/boxes/' + this.state.currentBox.boxId, dto);
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
}

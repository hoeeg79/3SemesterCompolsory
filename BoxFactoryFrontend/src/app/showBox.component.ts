import {Component} from "@angular/core";
import {State} from "../state";

@Component({
  templateUrl: 'showBox.html',
})

export class ShowBoxComponent{
  constructor(public state: State) {
  }

  updateBox(boxItem: any) {
    
  }

  deleteBox(boxId: any) {
    
  }
}

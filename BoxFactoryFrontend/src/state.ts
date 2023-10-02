import {Injectable} from "@angular/core";
import {BoxItem} from "./models";


@Injectable({
  providedIn: 'root'
})

export class State{
  boxItems: BoxItem[] = [];
  currentBox: BoxItem = new BoxItem();
}

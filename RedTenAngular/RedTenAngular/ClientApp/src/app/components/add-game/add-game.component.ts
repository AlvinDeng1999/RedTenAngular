import { Component, OnInit } from '@angular/core';
import { fadeInOut } from '../../services/animations';

@Component({
  selector: 'app-add-game',
  templateUrl: './add-game.component.html',
  styleUrls: ['./add-game.component.scss'],
  animations: [fadeInOut]
})
export class AddGameComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}

import { Component, OnInit } from '@angular/core';
import { fadeInOut } from '../../services/animations';

@Component({
  selector: 'app-players',
  templateUrl: './players.component.html',
  styleUrls: ['./players.component.scss'],
  animations: [fadeInOut]
})
export class PlayersComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}

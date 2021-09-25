import { Component, OnInit } from '@angular/core';
import { fadeInOut } from '../../services/animations';

@Component({
  selector: 'app-add-player',
  templateUrl: './add-player.component.html',
  styleUrls: ['./add-player.component.scss'],
  animations: [fadeInOut]
})
export class AddPlayerComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}

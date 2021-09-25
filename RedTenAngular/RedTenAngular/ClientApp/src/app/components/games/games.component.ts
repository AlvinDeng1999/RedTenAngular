import { Component, OnInit, ViewChild } from '@angular/core';
import { fadeInOut } from '../../services/animations';
import { ModalDirective } from 'ngx-bootstrap/modal'

@Component({
  selector: 'app-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.scss'],
  animations: [fadeInOut]
})
export class GamesComponent implements OnInit {
  @ViewChild('gameModal', { static: true })
  gameModal: ModalDirective;
  formResetToggle: boolean = true;
  constructor() { }

  ngOnInit(): void {
  }

  addGame() {
    this.formResetToggle = false;

    setTimeout(() => {
      this.formResetToggle = true;

      this.gameModal.show();
    });
  }
}

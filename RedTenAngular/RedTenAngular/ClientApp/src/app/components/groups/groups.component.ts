import { Component, OnInit, ViewChild } from '@angular/core';
import { fadeInOut } from '../../services/animations';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.scss'],
  animations: [fadeInOut]
})
export class GroupsComponent implements OnInit {
  @ViewChild('groupModal', { static: true })
  groupModal: ModalDirective;
  formResetToggle: boolean = true;
  constructor() { }

  ngOnInit(): void {
  }

  addGroup() {
    this.formResetToggle = false;

    setTimeout(() => {
      this.formResetToggle = true;

      this.groupModal.show();
    });
  }
}

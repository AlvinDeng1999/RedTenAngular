import { Component, OnInit } from '@angular/core';
import { fadeInOut } from '../../services/animations';

@Component({
  selector: 'app-add-group',
  templateUrl: './add-group.component.html',
  styleUrls: ['./add-group.component.scss'],
  animations: [fadeInOut]
})
export class AddGroupComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}

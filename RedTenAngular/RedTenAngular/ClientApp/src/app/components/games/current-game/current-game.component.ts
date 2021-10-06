import { Component, OnInit, Input, Output, EventEmitter, OnChanges, ViewChild } from '@angular/core';
import { Game } from '../../../models/game.model';
import { Round } from '../../../models/round.model';
import { ModalDirective } from 'ngx-bootstrap/modal';

import { GameService } from '../../../services/game.service';

@Component({
  selector: 'app-current-game',
  templateUrl: './current-game.component.html',
  styleUrls: ['./current-game.component.scss']
})
export class CurrentGameComponent implements OnInit, OnChanges {
  @Input() openFromParent: Game;
  @Output() openToParent = new EventEmitter<Game>();

  @ViewChild('closeModal', { static: true })
  closeModal: ModalDirective;

  constructor(private gameService: GameService) { }

  open: Game = new Game();
  rowDataOpen: Game[] = [];
  columnDefs = [
    {
      field: 'id',
      hide: true,
      suppressToolPanel: true
    },
    {
      field: 'location',
    },
    { field: 'date' },
    {
      field: 'status'
    },
    {
      field: 'groupid',
      hide: true,
      suppressToolPanel: true
    }
  ];
  
  private gridApi: any;
  private gridColumnApi: any;
  onGridReady(params) {
    this.gridApi = params.api;
    this.gridColumnApi = params.columnApi;
    this.gridApi.sizeColumnsToFit();
  }


  ngOnInit(): void {

  }

  ngOnChanges(): void {
    this.open = this.openFromParent;
    this.rowDataOpen = [];
    console.log("here");
    if (this.open.id != 0) {
      this.rowDataOpen.push(this.open);   
    }
    
  }

  closeGame() {
    setTimeout(() => {
      this.closeModal.show();
    });
  }

  confirmGameClose() {
    this.open.status = 'Closed';
    this.gameService.updateGame(this.open).subscribe(result => {
      this.open = new Game();
      this.open.id = 0;
      this.rowDataOpen = [];

    },
      error => {
      });
    this.openToParent.emit(this.open);
    this.closeModal.hide();
  }

  toggleRound() {

  }

  cancelModal() {
    this.closeModal.hide();
  }

}

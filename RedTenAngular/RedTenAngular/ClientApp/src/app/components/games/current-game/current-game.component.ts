import { Component, OnInit, Input, Output, EventEmitter, OnChanges, ViewChild } from '@angular/core';
import { Game } from '../../../models/game.model';
import { Group } from '../../../models/group.model';
import { Round } from '../../../models/round.model';
import { Player } from '../../../models/player.model';
import { PlayerScore } from '../../../models/playerscore.model';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GameDetails } from '../../../models/GameDetails.model';

import { GameService } from '../../../services/game.service';

import { faPlus, faEdit, faTimes, faSave } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-current-game',
  templateUrl: './current-game.component.html',
  styleUrls: ['./current-game.component.scss']
})
export class CurrentGameComponent implements OnInit, OnChanges {

  faPlus = faPlus;
  faEdit = faEdit;
  faTimes = faTimes;
  faSave = faSave;

  @ViewChild('closeModal', { static: true })
  closeModal: ModalDirective;
  @ViewChild('gameDetails', { static: true })
  gameDetails: ModalDirective;

  @Input() openFromParent: Game;
  @Input() groupsFromParent: Group[];
  @Input() playerScoresFromParent: PlayerScore[];
 
  @Output() openToParent = new EventEmitter<Game>();

  open: Game = new Game();
  groups: Group[] = [];
  playerScores: PlayerScore[] = [];
  players: Player[] = [];

  formResetToggle: boolean = false;

  constructor(private gameService: GameService) { }
  
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
    if (this.open.id != 0) {
      this.rowDataOpen.push(this.open);   
    }
    this.playerScores = this.playerScoresFromParent;
    this.groups = this.groupsFromParent;
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

  editRoundToggle: boolean = false;
  toggleRound() {
    this.editRoundToggle = !this.editRoundToggle;
    this.getGroupPlayers();
  }

  cancelModal() {
    this.closeModal.hide();
    this.gameDetails.hide();
  }

  editGameDetails() {
    this.formResetToggle = false;

    setTimeout(() => {
      this.formResetToggle = true;

      this.gameDetails.show();
    });
  }

  saveEdit() {

    this.gameService.updateGame(this.open).subscribe(result => {
      this.rowDataOpen = [];
      this.rowDataOpen.push(this.open);
    },
      error => {
      });

    this.cancelModal();
  }

  cancelRound(cancelFromChild: string) {
    this.toggleRound();
  }

  private getGroupPlayers() {
    if (this.groups.length > 0) {
      this.players = this.groups[0].players;
    }
  }

  roundSubmit(submitFromChild: Round) {
    this.toggleRound();
    this.loadGame();
  }

  loadGame() {
    this.gameService.getGame(this.open.id).subscribe(results => this.onLoadGameSuccess(results), error => this.onLoadFail());
  }

  onLoadGameSuccess(gameDetails: GameDetails) {
    this.playerScores = gameDetails.playerGameScores;
  }

  onLoadFail() {

  }
}

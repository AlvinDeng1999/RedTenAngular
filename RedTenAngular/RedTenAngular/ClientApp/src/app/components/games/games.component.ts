import { Component, OnInit, ViewChild } from '@angular/core';
import { fadeInOut } from '../../services/animations';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GameService } from '../../services/game.service';
import { Game } from '../../models/game.model';

import { AlertService, MessageSeverity } from '../../services/alert.service';
import { GroupService } from '../../services/group.service';
import { Group } from '../../models/group.model';
import { data } from 'jquery';

import { RoundService } from '../../services/round.service';
import { Round } from '../../models/round.model';

import { PlayerService } from '../../services/player.service';
import { Player } from '../../models/player.model';


@Component({
  selector: 'app-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.scss'],
  animations: [fadeInOut]
})
export class GamesComponent implements OnInit {
  @ViewChild('gameModal', { static: true })
  gameModal: ModalDirective;
  @ViewChild('closeModal', { static: true })
  closeModal: ModalDirective;
  @ViewChild('roundModal', { static: true })
  roundModal: ModalDirective;

  games: Game[] = [];
  groups: Group[];
  open: Game = new Game();
  closed: Game[] = [];

  players: Player[] = [];

  roundEdit: Round = new Round();
  scoreEdit: number = 0;

  chooseLosers: boolean = false;
  losersToggle() {
    this.chooseLosers = !this.chooseLosers;
  }

  rowDataRounds: Round[] = [];
  //rowSelection: string = "Multiple";
  gameEdit: Game = new Game();
  gameEditToggle: boolean = false;
  editToggle() {
    this.gameEditToggle = !this.gameEditToggle;
  }

  editRoundToggle: boolean = false;
  toggleRound() {
    this.editRoundToggle = !this.editRoundToggle;
    this.getGroupPlayers();
    
  }

  selectedPlayers: Player[] = [];

  formResetToggle: boolean = true;

  constructor(private groupService: GroupService, private alertService: AlertService, private gameService: GameService, private playerService: PlayerService, private roundService: RoundService) { }

  defaultColDef = true;

  columnRoundDef = [
    {
      field: 'id',
      hide: true,
      suppressToolPanel: true
    },
    {
      field: 'time',
    }
  ];

  columnPlayerDef = [
    {
      field: 'id',
      hide: true,
      suppressToolPanel: true
    },
    {
      field: 'firstName',
      checkboxSelection: true
    },
    { field: 'lastName' },
    {
      field: 'nickName',
      hide: true,
      suppressToolPanel: true
    },
    {
      field: 'email',
      hide: true,
      suppressToolPanel: true
    }
  ];

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
  rowData = [];
  rowDataOpen = [];
  rowDataPlayer = [];
  rowDataLosers = [];
  private gridApi: any;
  private gridColumnApi: any;
  rowSelection = 'multiple';
  onGridReady(params) {
    this.gridApi = params.api;
    this.gridColumnApi = params.columnApi;
    this.gridApi.sizeColumnsToFit();
  }

  ngOnInit(): void {
    this.loadGroups();
    this.open.id = 0;
    this.loadGames();

  }

  getSelectedRowData() {
    let selectedNodes = this.gridApi.getSelectedNodes();
    this.selectedPlayers = selectedNodes.map(node => node.data);
    console.log(this.selectedPlayers.length);
    this.rowDataLosers = this.selectedPlayers;
  }

  private loadGames() {
    this.alertService.startLoadingMessage();
    this.gameService.getGames().subscribe(results => this.onLoadGamesSuccess(results), error => this.onLoadFail(error));
    
  }

  private loadGroups() {
    this.alertService.startLoadingMessage();
    this.groupService.getGroups().subscribe(results => this.onLoadSuccess(results), error => this.onLoadFail(error));
  }

  private onLoadSuccess(groups: Group[]) {
    this.groups = groups;
    this.alertService.stopLoadingMessage();
  }

  private onLoadGamesSuccess(games: Game[]) {
    this.games = games;
    for (let i = 0; i < this.games.length; i++) {
      if (this.games[i].status == 'Closed') {
        this.closed.push(this.games[i]);
      }
      else if(this.games[i].status == 'Open') {
        this.open = this.games[i];
      }
      console.log(this.open.id);
    }
    this.alertService.stopLoadingMessage();
    this.rowData = this.closed;
    this.rowDataOpen = [];
    this.rowDataOpen.push(this.open);
    console.log(this.open.id);
  }

  private onLoadFail(error: any) {
    this.alertService.stopLoadingMessage();
    console.log("fail");
    this.alertService.showStickyMessage('Load Error', 'You have no groups', MessageSeverity.error, error);
  }


  addGame() {
    this.formResetToggle = false;

    setTimeout(() => {
      this.formResetToggle = true;
      this.gameEdit = new Game();
      this.gameModal.show();
    });
  }

  cancelGame() {
    this.gameModal.hide();
    console.log("cancel game");
  }

  closeGame() {
    setTimeout(() => {
      this.closeModal.show();
    });
  }

  cancelCancel() {
    this.closeModal.hide();
  }

  confirmClose() {
    this.alertService.startLoadingMessage();
    this.open.status = 'Closed';
    this.gameService.updateGame(this.open).subscribe(result => {
      this.gridApi.updateRowData({ add: [this.open] });
      this.alertService.showStickyMessage('Game Closed');
      this.alertService.stopLoadingMessage();
      this.closed.push(result);
      this.open = new Game();
      this.open.id = 0;
      this.rowDataOpen = [];
      
    },
      error => {
        this.alertService.showStickyMessage('Save Error', 'Game could not be closed', MessageSeverity.error, error);
        this.alertService.stopLoadingMessage();
      });
    this.closeModal.hide();
  }

  saveEdit() {
    this.alertService.startLoadingMessage();

    this.gameService.updateGame(this.open).subscribe(result => {
      this.alertService.showStickyMessage('Game Saved');
      this.games.push(result);
      this.closed = [];
      for (let i = 0; i < this.games.length; i++) {
        if (this.games[i].status == 'Closed') {
          this.closed.push(this.games[i]);
        }
        else if (this.games[i].status == 'Open') {
          this.open = this.games[i];
          this.rowDataOpen = [];
          this.rowDataOpen.push(this.open);
        }
      }
      this.rowData = this.closed;
      this.gridApi.updateRowData({ delete: data })
      this.gridApi.updateRowData({ add: [this.closed] });
      this.alertService.stopLoadingMessage();
    },
      error => {
        this.alertService.showStickyMessage('Save Error', 'Game could not be created', MessageSeverity.error, error);
        this.alertService.stopLoadingMessage();
      });

    this.editToggle();
  }

  saveGame() {
    this.alertService.startLoadingMessage();
    this.gameEdit.status = 'Open';

    this.gameService.createGame(this.gameEdit).subscribe(result => {
      this.alertService.showStickyMessage('Game Saved');
      this.games.push(result);
      this.closed = [];
      for (let i = 0; i < this.games.length; i++) {
        if (this.games[i].status == 'Closed') {
          this.closed.push(this.games[i]);
        }
        else if (this.games[i].status == 'Open') {
          this.open = this.games[i];
          this.rowDataOpen = [];
          this.rowDataOpen.push(this.open);
        }
      }
      this.rowData = this.closed;
      this.gridApi.updateRowData({ delete: data })
      this.gridApi.updateRowData({ add: [this.closed] });
      this.alertService.stopLoadingMessage();
    },
      error => {
        this.alertService.showStickyMessage('Save Error', 'Game could not be created', MessageSeverity.error, error);
        this.alertService.stopLoadingMessage();
      });

    this.gameModal.hide();
  }

  private getGroupPlayers() {
    if (this.groups.length > 0) {
      this.players = this.groups[0].players;
      this.rowDataPlayer = this.players;
    }
  }

  addRound() {
    this.getSelectedRowData();
    this.toggleRound();
    this.losersToggle();
  }

  submitRound() {
    this.roundEdit.gameid = this.open.id;
    if (!this.open.rounds) {
      this.open.rounds = [];
    }
    this.saveRound();
    this.open.rounds.push(this.roundEdit);
    this.saveEdit();
    this.editToggle();
    this.losersToggle();
  }

  saveRound() {
    this.alertService.startLoadingMessage();
    this.alertService.startLoadingMessage();
    this.roundService.createRound(this.roundEdit).subscribe(result => {
      this.alertService.showStickyMessage('Round Saved');
      this.alertService.stopLoadingMessage();
    },
      error => {
        this.alertService.showStickyMessage('Save Error', 'Round could not be saved', MessageSeverity.error, error);
        this.alertService.stopLoadingMessage();
      });
  }

}

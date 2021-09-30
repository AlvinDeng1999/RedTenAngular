import { Component, OnInit, ViewChild } from '@angular/core';
import { fadeInOut } from '../../services/animations';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GameService } from '../../services/game.service';
import { Game } from '../../models/game.model';

import { AlertService, MessageSeverity } from '../../services/alert.service';
import { GroupService } from '../../services/group.service';
import { Group } from '../../models/group.model';

@Component({
  selector: 'app-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.scss'],
  animations: [fadeInOut]
})
export class GamesComponent implements OnInit {
  @ViewChild('gameModal', { static: true })
  gameModal: ModalDirective;

  games: Game[] = [];
  groups: Group[];

  gameEdit: Game = new Game();
  formResetToggle: boolean = true;

  constructor(private groupService: GroupService, private alertService: AlertService, private gameService: GameService) { }

  defaultColDef = true;

  //public id: number;
  //public location: string;
  //public date: Date;
  //public status: string;
  //public groupid: number;
  //public rounds: Round[];

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
  private gridApi: any;
  private gridColumnApi: any;
  onGridReady(params) {
    this.gridApi = params.api;
    this.gridColumnApi = params.columnApi;
    this.gridApi.sizeColumnsToFit();
  }

  ngOnInit(): void {
    this.loadGroups();
    this.loadGames();
    
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
    this.alertService.stopLoadingMessage();
    this.rowData = this.games;
    console.log(this.games.length);
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

  saveGame() {
    this.alertService.startLoadingMessage();
    console.log(this.gameEdit.location);
    console.log(this.gameEdit.date);
    console.log(this.gameEdit.status);
    this.gameEdit.status = 'Open';
    console.log(this.gameEdit.status);

    this.gameService.createGame(this.gameEdit).subscribe(result => {
      this.alertService.showStickyMessage('Game Saved');
      this.alertService.stopLoadingMessage();
    },
      error => {
        this.alertService.showStickyMessage('Save Error', 'Game could not be created', MessageSeverity.error, error);
        this.alertService.stopLoadingMessage();
      });

    this.gameModal.hide();
  }

}

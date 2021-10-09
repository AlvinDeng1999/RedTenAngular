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
import { RoundViewModel } from '../../models/roundviewmodel.model';
import { PlayerViewModel } from '../../models/playerviewmodel.model';
import { PlayerScore } from '../../models/playerscore.model';
import { GameDetails } from '../../models/gamedetails.model';

import { faPlus, faSave } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.scss'],
  animations: [fadeInOut]
})
export class GamesComponent implements OnInit {

  faPlus = faPlus;
  faSave = faSave;

  @ViewChild('gameModal', { static: true })
  gameModal: ModalDirective;

  games: Game[] = [];
  open: Game = new Game();
  closed: Game[] = [];

  groups: Group[];
  
  players: Player[] = [];
  playerScores: PlayerScore[] = [];

  gameEdit: Game = new Game();
  roundEdit: Round = new Round();
  scoreEdit: number = 0;

  formResetToggle: boolean = true;

  editRoundToggle: boolean = false;
  toggleRound() {
    this.editRoundToggle = !this.editRoundToggle;
  }

  chooseLosers: boolean = false;
  losersToggle() {
    this.chooseLosers = !this.chooseLosers;
  }

  constructor(private groupService: GroupService, private alertService: AlertService, private gameService: GameService,
    private playerService: PlayerService, private roundService: RoundService) { }

  ngOnInit(): void {
    this.loadGroups();
    this.open.id = 0;
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
    for (let i = 0; i < this.games.length; i++) {
      if (this.games[i].status == 'Closed') {
        this.closed.push(this.games[i]);
      }
      else if(this.games[i].status == 'Open') {
        this.open = this.games[i];
      }
    }
    this.alertService.stopLoadingMessage();
    if (this.open.id > 0) {
      this.loadGame();
    }
  }

  private onLoadFail(error: any) {
    this.alertService.stopLoadingMessage();
    this.alertService.showStickyMessage('Load Error', 'You have no groups', MessageSeverity.error, error);
  }

  addGame() {
    this.formResetToggle = false;

    setTimeout(() => {
      this.formResetToggle = true;
      this.gameEdit = new Game();
      this.gameEdit.date = new Date();
      this.gameModal.show();
    });
  }

  cancelGame() {
    this.gameModal.hide();
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
        }
      }
      this.alertService.stopLoadingMessage();
      this.playerScores = [];
    },
      error => {
        this.alertService.showStickyMessage('Save Error', 'Game could not be created', MessageSeverity.error, error);
        this.alertService.stopLoadingMessage();
      });
    this.gameModal.hide();
  }

  loadGame() {
    this.alertService.startLoadingMessage();
    this.gameService.getGame(this.open.id).subscribe(results => this.onLoadGameSuccess(results), error => this.onLoadFail(error));
  }

  onLoadGameSuccess(gameDetails: GameDetails) {
    this.playerScores = gameDetails.playerGameScores;
    this.alertService.stopLoadingMessage();
  }

  gameClosed(openToParent: Game) {
    this.open.id = 0;
    this.closed.push(openToParent);
  }
}

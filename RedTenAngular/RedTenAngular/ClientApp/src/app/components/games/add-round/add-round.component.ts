import { Component, OnInit, Input, Output, OnChanges, EventEmitter } from '@angular/core';
import { Round } from '../../../models/round.model';
import { Group } from '../../../models/group.model';
import { Player } from '../../../models/player.model';
import { Game } from '../../../models/game.model';
import { RoundViewModel } from '../../../models/roundviewmodel.model';
import { PlayerViewModel } from '../../../models/playerviewmodel.model';
import { RoundService } from '../../../services/round.service';

@Component({
  selector: 'app-add-round',
  templateUrl: './add-round.component.html',
  styleUrls: ['./add-round.component.scss']
})
export class AddRoundComponent implements OnInit {
  @Input() playersFromParent: Player[];
  @Input() openFromParent: Game;
  
  @Output() cancelFromChild = new EventEmitter<string>();
  @Output() submitFromChild = new EventEmitter<Round>();

  selectedPlayers: Player[] = [];
  open: Game = new Game();

  chooseLosers: boolean = false;
  toggleLosers() {
    this.chooseLosers = !this.chooseLosers;
  }

  roundEdit: Round = new Round();
  scoreEdit: number = 0;

  constructor(private roundService: RoundService) { }

  rowDataPlayer: Player[] = [];
  rowDataRoundPlayers = [];
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
      field: 'nickName'
    },
    {
      field: 'email',
      hide: true,
      suppressToolPanel: true
    }
  ];
  private gridApi: any;
  private gridColumnApi: any;
  rowSelection = 'multiple';
  onGridReady(params) {
    this.gridApi = params.api;
    this.gridColumnApi = params.columnApi;
    const sortModel = [
      { colId: 'date', sort: 'desc' }
    ];
    this.gridApi.setSortModel(sortModel);
    this.gridApi.sizeColumnsToFit();
  }

  ngOnInit(): void {
  }

  ngOnChanges(): void {
    this.rowDataPlayer = this.playersFromParent;
    this.open = this.openFromParent;
  }

  addRound() {
    this.getSelectedRowData();
    this.toggleLosers();
  }

  getSelectedRowData() {
    let selectedNodes = this.gridApi.getSelectedNodes();
    this.selectedPlayers = selectedNodes.map(node => node.data);
    this.rowDataRoundPlayers = this.selectedPlayers;
  }

  roundCancel() {
    this.cancelFromChild.emit("NULL");
  }

  submitRound() {
    this.roundEdit.gameid = this.open.id;
    if (!this.open.rounds) {
      this.open.rounds = [];
    }
    this.saveRound();
    this.open.rounds.push(this.roundEdit);
  }

  saveRound() {
    let rvm = new RoundViewModel();
    rvm.gameid = this.open.id;
    rvm.time = this.roundEdit.time;
    rvm.players = [];
    let selectedNodes = this.gridApi.getSelectedNodes();
    let selectedLosers = selectedNodes.map(node => node.data);

    this.rowDataRoundPlayers.forEach((player) => {
      if (selectedLosers.includes(player)) {
        player.score = this.scoreEdit;
      }
      else {
        player.score = 0;
      }
      rvm.players.push(new PlayerViewModel(player.id, player.score));
    });
    this.roundService.createRound(rvm).subscribe(result => {
      this.submitFromChild.emit(result);
    },
      error => {
      });
    this.toggleLosers();
  }
}

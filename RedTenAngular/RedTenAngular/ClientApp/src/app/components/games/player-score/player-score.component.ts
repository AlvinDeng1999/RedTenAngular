import { Component, OnInit, Input, OnChanges} from '@angular/core';
import { PlayerScore } from '../../../models/playerscore.model';

@Component({
  selector: 'app-player-score',
  templateUrl: './player-score.component.html',
  styleUrls: ['./player-score.component.scss']
})
export class PlayerScoreComponent implements OnInit, OnChanges {
  @Input() playerScoresFromParent: PlayerScore[];
  playerScores: PlayerScore[] = [];

  constructor() {
    
  }

  ngOnInit(): void {

  }

  ngOnChanges() {
    this.playerScores = this.playerScoresFromParent;
  }

  defaultColDef = true;
  columnDef = [
    {
      field: 'id',
      hide: true,
      suppressToolPanel: true
    },
    {
      field: 'firstName'
    },
    { field: 'lastName' },
    {
      headerName: 'Score',
      field: 'playerScore'
    }
  ];

  private gridApi: any;
  private gridColumnApi: any;

  onGridReady(params) {
    this.gridApi = params.api;
    this.gridColumnApi = params.columnApi;
    this.gridApi.sizeColumnsToFit();
  }
}

import { Component, OnInit, Input, Output, EventEmitter, OnChanges, ViewChild } from '@angular/core';
import { Game } from '../../../models/game.model';

@Component({
  selector: 'app-past-games',
  templateUrl: './past-games.component.html',
  styleUrls: ['./past-games.component.scss']
})
export class PastGamesComponent implements OnInit {
  @Input() pastGamesFromParent: Game[];

  constructor() { }

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
  rowData: Game[] = [];
  private gridApi: any;
  private gridColumnApi: any;
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
    this.rowData = this.pastGamesFromParent;
  }
}

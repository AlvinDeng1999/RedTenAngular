import { Component, OnInit, ViewChild } from '@angular/core';
import { fadeInOut } from '../../services/animations';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GroupService } from '../../services/group.service';
import { Group } from '../../models/group.model';
import { PlayerService } from '../../services/player.service';
import { Player } from '../../models/player.model';
import { AlertService, MessageSeverity } from '../../services/alert.service';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.scss'],
  animations: [fadeInOut]
})

export class GroupsComponent implements OnInit {
  @ViewChild('groupModal', { static: true })
  groupModal: ModalDirective;
  @ViewChild('playerModal', { static: true })
  playerModal: ModalDirective;

  formResetToggle: boolean = true;
  groupEdit: Group = new Group();
  playerEdit: Player = new Player();
  groups: Group[] = [];
  viewPlayerToggle: boolean = false;
  

  defaultColDef = true;

  columnDefs = [
    {
      field: 'id',
      hide: true,
      suppressToolPanel: true},
    { field: 'firstName' },
    { field: 'lastName' },
    {
      field: 'nickname'
    },
    {
      field: 'email',
      hide: true,
      suppressToolPanel: true    }
  ];
  rowData = [];
  private gridApi: any;
  private gridColumnApi: any;
  onGridReady(params) {
    this.gridApi = params.api;
    this.gridColumnApi = params.columnApi;
    this.gridApi.sizeColumnsToFit();
  }
  constructor(private groupService: GroupService, private alertService: AlertService, private playerService: PlayerService ) { }

  ngOnInit(): void {
    this.loadGroups();
  }

  private loadGroups() {
    this.alertService.startLoadingMessage();
    this.groupService.getGroups().subscribe(results => this.onLoadSuccess(results), error => this.onLoadFail(error));
  }

  private onLoadSuccess(groups: Group[]) {
    this.groups = groups
    this.alertService.stopLoadingMessage();
    if(groups.length > 0) this.rowData = groups[0].players;
  }

  private onLoadFail(error: any) {
    this.alertService.stopLoadingMessage();
    this.alertService.showStickyMessage('Load Error', 'You have no groups', MessageSeverity.error, error);
  }
  

  addGroup() {
    this.formResetToggle = false;

    setTimeout(() => {
      this.formResetToggle = true;
      this.groupEdit = {id:0, name:"", players: []};
      this.groupModal.show();
    });
  }

  saveGroup() {
    this.alertService.startLoadingMessage();
    this.groupService.createGroup(this.groupEdit).subscribe(result =>  {
      this.groups.push(result);
      this.alertService.showStickyMessage('Group saved');
      this.alertService.stopLoadingMessage();
    },
      error =>  {
        this.alertService.showStickyMessage('Save Error','Group could not be created', MessageSeverity.error, error);
        this.alertService.stopLoadingMessage();
    });
    this.groupModal.hide();
  }
  private onSaveSuccess(group: Group) {

  }

  cancelGroup() {
    this.groupModal.hide();
  }

  togglePlayers() {
    this.viewPlayerToggle = !this.viewPlayerToggle;
  }

  addPlayer() {
    this.formResetToggle = false;

    setTimeout(() => {
      this.formResetToggle = true;
      this.playerEdit = new Player();
      this.playerModal.show();
    });
  }

  savePlayer() {
    this.alertService.startLoadingMessage();
    console.log("save group");
    console.log(this.playerEdit.firstName);
    this.playerService.createPlayer(this.playerEdit).subscribe(result => {
      if (!this.groups[0].players) {
        this.groups[0].players = [];
        }
      this.groups[0].players.push(result);
      this.rowData = this.groups[0].players;
      this.gridApi.updateRowData({ add: [result] });
      this.alertService.showStickyMessage('Player Saved');
      this.alertService.stopLoadingMessage();
    },
      error => {
        this.alertService.showStickyMessage('Save Error', 'Player could not be created', MessageSeverity.error, error);
        this.alertService.stopLoadingMessage();
      });

    this.playerModal.hide();
  }

  cancelPlayer() {
    this.playerModal.hide();
    console.log("cancel group")
  }
}

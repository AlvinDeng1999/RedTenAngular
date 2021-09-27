import { Component, OnInit, ViewChild } from '@angular/core';
import { fadeInOut } from '../../services/animations';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GroupService } from '../../services/group.service';
import { Group } from '../../models/group.model';
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
  formResetToggle: boolean = true;
  groupEdit: Group = { id: 0, name: "" };
  groups: Group[] = [];

  constructor(private groupService: GroupService, private alertService: AlertService ) { }

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
  }

  private onLoadFail(error: any) {
    this.alertService.stopLoadingMessage();
    this.alertService.showStickyMessage('Load Error', 'You have no groups', MessageSeverity.error, error);
  }
  

  addGroup() {
    this.formResetToggle = false;

    setTimeout(() => {
      this.formResetToggle = true;
      this.groupEdit = {id:0, name:""};
      this.groupModal.show();
    });
  }

  saveGroup() {
    this.alertService.startLoadingMessage();
    console.log("save group");
    console.log(this.groupEdit.name);
    this.groupService.createGroup(this.groupEdit).subscribe(result => function () {
      this.alertService.showStickyMessage('Group saved');
      this.alertService.stopLoadingMessage();
    },
      error => function () {
        this.alertService.showStickyMessage('Save Error','Group could not be created', MessageSeverity.error, error);
        this.alertService.stopLoadingMessage();
    });
    this.groupModal.hide();
  }

  cancelGroup() {
    this.groupModal.hide();
    console.log("cancel group")
  }
}

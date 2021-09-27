import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { GroupEndpointService } from './group-endpoint.service';

import { Group } from '../models/group.model';

@Injectable({
  providedIn: 'root'
})
export class GroupService {

  constructor(private authService: AuthService,
    private groupEndpoint: GroupEndpointService) { }

  getGroups() {
    return this.groupEndpoint.getGroupEndpoint<Group[]>();
  }

  createGroup(group: Group) {
    return this.groupEndpoint.getNewGroupEndpoint<Group>(group);
  }

}

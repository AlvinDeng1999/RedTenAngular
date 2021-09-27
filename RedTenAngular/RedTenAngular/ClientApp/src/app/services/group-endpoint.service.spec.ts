import { TestBed } from '@angular/core/testing';

import { GroupEndpointService } from './group-endpoint.service';

describe('GroupEndpointService', () => {
  let service: GroupEndpointService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GroupEndpointService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

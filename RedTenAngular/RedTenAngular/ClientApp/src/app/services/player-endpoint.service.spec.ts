import { TestBed } from '@angular/core/testing';

import { PlayerEndpointService } from './player-endpoint.service';

describe('PlayerEndpointService', () => {
  let service: PlayerEndpointService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PlayerEndpointService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

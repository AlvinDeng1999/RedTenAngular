import { TestBed } from '@angular/core/testing';

import { GameEndpointService } from './game-endpoint.service';

describe('GameEndpointService', () => {
  let service: GameEndpointService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GameEndpointService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

import { TestBed } from '@angular/core/testing';

import { RoundEndpointService } from './round-endpoint.service';

describe('RoundEndpointService', () => {
  let service: RoundEndpointService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RoundEndpointService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

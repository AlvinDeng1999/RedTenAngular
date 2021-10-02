import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { RoundEndpointService } from './round-endpoint.service';

import { Round } from '../models/round.model';
@Injectable({
  providedIn: 'root'
})
export class RoundService {

  constructor(private authService: AuthService,
    private roundEndpoint: RoundEndpointService) { }

  createRound(round: Round) {
    return this.roundEndpoint.getNewRoundEndpoint<Round>(round);
  }
}

import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { PlayerEndpointService } from './player-endpoint.service';

import { Player } from '../models/player.model';
@Injectable({
  providedIn: 'root'
})
export class PlayerService {

  constructor(private authService: AuthService,
    private playerEndpoint: PlayerEndpointService) { }

  createPlayer(player: Player) {
    return this.playerEndpoint.getNewPlayerEndpoint<Player>(player);
  }
}

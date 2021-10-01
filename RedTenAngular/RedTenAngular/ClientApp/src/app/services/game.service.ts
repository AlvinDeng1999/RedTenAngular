import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { GameEndpointService } from './game-endpoint.service';

import { Game } from '../models/game.model';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  constructor(private authService: AuthService, private gameEndpoint: GameEndpointService) { }

  getGames() {
    return this.gameEndpoint.getGameEndpoint<Game[]>();
  }

  createGame(game: Game) {
    return this.gameEndpoint.getNewGameEndpoint<Game>(game);
  }

  updateGame(game: Game) {
    return this.gameEndpoint.updateGameEndpoint<Game>(game);
  }

}

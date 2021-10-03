import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { GameEndpointService } from './game-endpoint.service';

import { GameDetails } from '../models/GameDetails.model';
import { Game } from '../models/game.model';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  constructor(private authService: AuthService, private gameEndpoint: GameEndpointService) { }

  getGames() {
    return this.gameEndpoint.getGamesEndpoint<Game[]>();
  }

  getGame(id: number) {
    return this.gameEndpoint.getGameEndpoint<GameDetails>(id)
  }

  createGame(game: Game) {
    return this.gameEndpoint.getNewGameEndpoint<Game>(game);
  }

  updateGame(game: Game) {
    return this.gameEndpoint.updateGameEndpoint<Game>(game);
  }

}

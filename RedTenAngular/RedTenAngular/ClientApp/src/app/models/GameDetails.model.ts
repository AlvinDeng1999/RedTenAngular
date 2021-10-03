import { Round } from './round.model';
import { PlayerScore } from './playerscore.model';
import { Game } from './game.model';

export class GameDetails  {
  public playerGameScores: PlayerScore[];

  constructor(playerGameScores?: PlayerScore[]) {
    this.playerGameScores = playerGameScores;
  }
}

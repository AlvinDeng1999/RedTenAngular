export class PlayerViewModel {
  playerid: number;
  score: number;

  constructor(playerid?: number, score?: number) {
    this.playerid = playerid;
    this.score = score;
  }
}

import { Round } from './round.model';
import { PlayerViewModel } from './playerviewmodel.model';

export class RoundViewModel extends Round {
  players: PlayerViewModel[];


  constructor(id?: number, time?: Date, gameid?: number, players?: PlayerViewModel[]) {
    super(id, time, gameid);
    this.players = players;
  }
}

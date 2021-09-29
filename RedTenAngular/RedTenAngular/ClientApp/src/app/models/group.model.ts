import { Player } from "./player.model";

export class Group {
  public id: number;
  public name: string;
  public players: Player[];

  constructor(id?: number, name?: string, players?: Player[]) {
    this.id = id;
    this.name = name;
    this.players = players;
  }
}

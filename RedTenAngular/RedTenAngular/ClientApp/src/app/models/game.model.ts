import { Round } from './round.model';

export class Game {
  public id: number;
  public location: string;
  public date: Date;
  public status: string;
  public groupid: number;
  public rounds: Round[];

  constructor(id?: number, location?: string, date?: Date, status?: string, groupid?: number, rounds?: Round[]) {
    this.id = id;
    this.location = location;
    this.date = date;
    this.status = status;
    this.groupid = groupid;
    this.rounds = rounds;
  }
}

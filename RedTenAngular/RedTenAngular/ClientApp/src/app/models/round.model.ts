export class Round {
  public id: number;
  public time: Date;
  public gameid: number;


  constructor(id?: number, time?: Date, gameid?: number) {
    this.id = id;
    this.time = time;
    this.gameid = gameid;
  }
}

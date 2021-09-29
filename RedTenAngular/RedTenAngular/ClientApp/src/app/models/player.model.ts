export class Player {
  public id: number;
  public firstName: string;
  public lastName: string;
  public nickname: string;
  public email: string;

  constructor(id?: number, firstName?: string, lastName?: string, nickname?: string, email?: string) {
    this.id = id;
    this.firstName = firstName;
    this.lastName = lastName;
    this.nickname = nickname;
    this.email = email;
  }
}

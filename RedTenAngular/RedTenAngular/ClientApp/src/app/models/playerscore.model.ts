export class PlayerScore {
  id: number;
  firstName: string;
  lastName: string;
  score: number;

  constructor(id?: number, firstName?: string, lastName?: string, score?: number) {
    this.id = id;
    this.firstName = firstName;
    this.lastName = lastName;
    this.score = score;
  }
}

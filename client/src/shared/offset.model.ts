export class Offset {
  public offsetNumber: number;
  public limit: number;

  constructor(offset, limit = 10) {
    this.offsetNumber = offset ?? 0;
    this.limit = limit;
  }
}

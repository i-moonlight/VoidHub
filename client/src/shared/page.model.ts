export class Page {
  public pageNumber: number;
  public pageSize: number;

  constructor(pageNumber, pageSize = 10) {
    this.pageNumber = pageNumber ?? 1;
    this.pageSize = pageSize;
  }

  public Equals(other: Page): boolean {
    return (
      this.pageNumber == other.pageNumber &&
      this.pageSize == other.pageSize
    );
  }
}

import { StringExtension } from "src/shared/string.extension";

export class SearchParams {
  public sort: string;
  public withPostContent: boolean;

  constructor(sort: string, withPostContent: string) {
    this.sort = sort ?? "";
    this.withPostContent = StringExtension.ConvertToBoolean(withPostContent);
  }

  public Equals(other: SearchParams): boolean {
    return (
      this.sort == other.sort &&
      this.withPostContent == other.withPostContent
    );
  }


}

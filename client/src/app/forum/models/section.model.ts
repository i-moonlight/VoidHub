import { Forum } from "./forum.model";

export class Section {
  public id: number;
  public title: string;
  public isHidden: boolean;
  public orderPosition: number;

  public forums: Forum[];
}

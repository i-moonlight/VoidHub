import { User } from "../../../shared/models/user.model";

export class Post {
  public id: number;
  public content: string;
  public createdAt: Date;
  public user: User;
}

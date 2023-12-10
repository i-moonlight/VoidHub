import { User } from "src/shared/models/user.model";

export class LastTopic {
  public id: number;
  public title: string;
  public updatedAt: Date;

  public user: User;
}

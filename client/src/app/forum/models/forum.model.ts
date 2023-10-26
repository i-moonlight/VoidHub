import { LastTopic } from "./last-topic.model";

export class Forum {
  public id: number;
  public title: string;
  public sectionId: number;

  public postsCount: number;
  public topicsCount: number;

  public lastTopic: LastTopic;
}

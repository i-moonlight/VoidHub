import { User } from "src/shared/models/user.model";
import { Jwt } from "./jwt.model";

export class AuthResponse {
  public tokens: Jwt;
  public user: User;
}

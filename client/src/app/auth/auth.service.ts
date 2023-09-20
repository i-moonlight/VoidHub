import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, of, tap } from "rxjs";
import { User } from "../../shared/models/user.model";
import { Register } from "./models/register.model";
import { AuthResponse } from "./models/auth-response.model";
import { Login } from "./models/login.model";

@Injectable({
  providedIn: 'root'
})
export class AuthService
{
  private user = new BehaviorSubject<User>(null);
  user$ = this.user.asObservable();

  private baseURL:string = 'http://localhost:5000/api/v1/auth/';

  constructor(private http: HttpClient) {}

  public login(login: Login) {
    return this.http.post<AuthResponse>(this.baseURL + 'login', login)
      .pipe(tap(data => this.handleAuth(data)));
  }
  public register(register: Register) {
    return this.http.post<AuthResponse>(this.baseURL + 'register', register)
      .pipe(tap(data => this.handleAuth(data)));
  }

  public logout() {
    localStorage.removeItem('access-token');
    localStorage.removeItem('refresh-token');
    localStorage.removeItem('user');

    this.user.next(null);
  }

  public setUser(user: User) {
    this.user.next(user);
  }

  public refreshAndAuth() {
    //refresh and handle auth
    const refreshToken = localStorage.getItem('refresh-token');
    if(!refreshToken)
      return of(null);

    return this.http.get<AuthResponse>(this.baseURL + 'refresh?refreshToken=' + refreshToken)
      .pipe(tap(data => this.handleAuth(data)))
  }

  private handleAuth(authResponse:AuthResponse) {
    if(!authResponse || !authResponse.user || !authResponse.tokens)
      return;

    localStorage.setItem('access-token', authResponse.tokens.accessToken);
    localStorage.setItem('refresh-token', authResponse.tokens.refreshToken);

    localStorage.setItem('user', JSON.stringify(authResponse.user));

    this.user.next(authResponse.user);
  }
}

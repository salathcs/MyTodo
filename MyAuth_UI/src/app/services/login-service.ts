import { Injectable } from "@angular/core";
import { AuthResult } from "../models/auth-result";

@Injectable({
  providedIn: "root"
})
export class LoginService {
  private authResult?: AuthResult;

  public setAuthResult(authResult: AuthResult): void {
    this.authResult = authResult;
  }

  public getAuthResult(): AuthResult | undefined { 
    return this.authResult;
  }

  public clearAuthResult(): void {
    this.authResult = undefined;
  }
}

import { Injectable } from '@angular/core';
import { User, UserManager, UserManagerSettings } from 'oidc-client';
import { ConstantURLS } from '../constants/constant-urls';
import { Subject } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _userManager: UserManager;
  private _user: User;

  private _loginChangedSubject = new Subject<boolean>();
  public loginChanged = this._loginChangedSubject.asObservable();
  
  private get idpSettings(): UserManagerSettings {
    return {
      authority: ConstantURLS.idpAuthority,
      client_id: ConstantURLS.clientId,
      redirect_uri: `${ConstantURLS.clientRoot}/signin-callback`, // redirect after successful authentication
      scope: "openid address roles profile IdentityServer4.API.Scope",
      response_type: "code", // grant type
      post_logout_redirect_uri: `${ConstantURLS.clientRoot}/signout-callback`,// redirect after logout
      
    }
  }
  constructor(private router:Router) {
    this._userManager = new UserManager(this.idpSettings);
  }
  public login = () => {
    return this._userManager.signinRedirect().then(x=> {console.log(x)}).catch(x=> console.log(x));
  }

  public isAuthenticated = (): Promise<boolean> => {
    return this._userManager.getUser()
    .then((user) => {
      if(this._user !== user){
        this._loginChangedSubject.next(this.checkUser(user));
      }
      this._user = user;
      return this.checkUser(user);
    })
  }
  private checkUser = (user : User): boolean => {
    return !!user && !user.expired;
  }

  public finishLogin = (): Promise<User> => {
    return this._userManager.signinRedirectCallback()
    .then(user => {
      this._loginChangedSubject.next(this.checkUser(user));
      this.router.navigate(['/']);
      return user;
    })
  }

  public logout = () => {
    this._userManager.signoutRedirect();
  }
  public  finishLogout = () => {
    this._user = null;
    return this._userManager.signoutRedirectCallback();
  }

  public async getAccessToken(){
    return this._userManager.getUser().then(user => {
      return !!user && !user.expired ? user.access_token : null;
    })
  }
}

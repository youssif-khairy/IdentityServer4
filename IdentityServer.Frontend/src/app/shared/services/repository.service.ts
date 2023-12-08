import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { ConstantURLS } from '../constants/constant-urls';
import { firstValueFrom, from } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RepositoryService {

  constructor(private http: HttpClient,  private _authService: AuthService) { }

  private weatherController = "WeatherForecast/"

  public getWeatherData = () => {
    return from(
      this._authService.getAccessToken()
      .then(token => {
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
        return firstValueFrom(this.http.get(this.createCompleteRoute(ConstantURLS.apiRoot, this.weatherController + "Get"), { headers: headers }));
      })
    );
  }

private createCompleteRoute = ( envAddress: string , route: string) =>    {
  return `${envAddress}/${route}`;
}
}

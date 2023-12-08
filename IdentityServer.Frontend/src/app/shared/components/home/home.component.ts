import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { RepositoryService } from '../../services/repository.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  public userAuthenticated = false;
  weatherData = []
  constructor(private _authService: AuthService,private repositoryService:RepositoryService){
    this._authService.loginChanged
    .subscribe(userAuthenticated => {
      this.userAuthenticated = userAuthenticated;
      if(!userAuthenticated){
        this._authService.login();
      }
    })
  }
  
  ngOnInit(): void {
    this.listenOnAuthentication();
    this.getWeatherData()
  }
  getWeatherData() {
    this.repositoryService.getWeatherData().subscribe({
      next : (value :any[])=> {
        this.weatherData = value;
      },
      error : (err) => {
        
      },
    })
  }
  listenOnAuthentication() {
    this._authService.isAuthenticated()
    .then(userAuthenticated => {
      this.userAuthenticated = userAuthenticated;
      if(!userAuthenticated){
        this._authService.login();
      }
    })
  }
}

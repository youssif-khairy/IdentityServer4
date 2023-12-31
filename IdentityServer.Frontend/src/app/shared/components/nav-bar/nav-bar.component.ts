import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent {

  userAuthenticated: boolean;

  constructor(
    private authService:AuthService
  ) { }
  ngOnInit(): void {
    this.subsUserAuthenticated()
  }

  subsUserAuthenticated() {
    this.authService.isAuthenticated()
    .then(userAuthenticated => {
      this.userAuthenticated = userAuthenticated;
    })
  }

  logout(){
    this.authService.logout()
  }

}

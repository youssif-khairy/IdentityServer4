import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-signout-callback',
  templateUrl: './signout-callback.component.html',
  styleUrls: ['./signout-callback.component.scss']
})
export class SignoutCallbackComponent implements OnInit{
  constructor(private router:Router,
    private authService:AuthService){

  }


  ngOnInit(): void {
    this.authService.finishLogout().then(re=>{
      this.router.navigate(['login']);

    })
  }
}

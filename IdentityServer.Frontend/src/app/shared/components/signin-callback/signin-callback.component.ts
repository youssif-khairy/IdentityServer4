import { Component,OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-signin-callback',
  templateUrl: './signin-callback.component.html',
  styleUrls: ['./signin-callback.component.scss']
})
export class SigninCallbackComponent implements OnInit {

  constructor(private authService:AuthService,){}

  ngOnInit(): void {
    this.authService.finishLogin();
  }
}

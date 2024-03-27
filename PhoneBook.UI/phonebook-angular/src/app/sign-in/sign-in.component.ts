import { Component, OnInit } from '@angular/core';
import { AuthService } from "../contact/service/auth.service"
import { ActivatedRoute, Router } from '@angular/router';
import { reload } from 'firebase/auth';
@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  constructor(
    public authService: AuthService,
    private router:Router,private activatedRoute:ActivatedRoute
  ) { }
  ngOnInit() { }

  signup(){
    this.router.navigate(['register-user'])
  }
  forgotPassword(){
    this.router.navigate(['forgot-password'])
  }
}
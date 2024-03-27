import { Component, OnInit } from '@angular/core';
import { AuthService } from "../contact/service/auth.service";
import { Router } from '@angular/router';
@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  constructor(
    public authService: AuthService,
    private router:Router
  ) { }
  ngOnInit() { }
 signin(){
    this.router.navigate(['/sign-in']);
 } 
}
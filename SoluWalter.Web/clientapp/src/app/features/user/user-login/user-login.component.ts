import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CustomerService } from '../customer.service';
import { UserService } from '../user.service';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.scss']
})
export class UserLoginComponent implements OnInit {

    user: any = {};

  constructor(
    private userService: UserService,
    private router : Router,
    private customer: CustomerService
  ) { }

  ngOnInit(): void {
  }

  loginUser(){
    this.userService.login(this.user).subscribe(data =>{
        if(data){
            const token = data.token;
            //console.log("token generado:", token);

            if(token){
                this.customer.saveToken(token);
                this.router.navigate(['/posts']);
            }
        }else{ 
            alert("Usuario u/o password incorrecto");
        }
    }, error => console.log(error))
  }

}

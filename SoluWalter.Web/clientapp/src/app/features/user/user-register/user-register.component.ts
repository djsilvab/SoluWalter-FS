import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CustomerService } from '../customer.service';
import { UserService } from '../user.service';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.scss']
})
export class UserRegisterComponent implements OnInit {

    user:any = {};

  constructor(
      private userService: UserService,
      private router: Router,
      private customer: CustomerService
  ) { }

  ngOnInit(): void {
  }

    registrarUser(){
        this.userService.registrar(this.user).subscribe(data =>{
            if(data){
                const token = data.token;
                //console.log("token generado:", token);
                if(token){
                    this.customer.saveToken(token);
                    this.router.navigate(['/posts']);
                }
            }else{
                alert("Ocurrio un problema, vuelva a intentar");
            }
        }, error =>{
            console.log(error);
        })
    }

}

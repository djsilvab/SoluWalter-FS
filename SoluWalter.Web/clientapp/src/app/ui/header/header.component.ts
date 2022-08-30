import { Component, OnInit } from '@angular/core';
import { CustomerService } from 'src/app/features/user/customer.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  constructor(
    private customer : CustomerService
  ) { }

  ngOnInit(): void {
  }

  logout(){
    this.customer.logout();
  }

}

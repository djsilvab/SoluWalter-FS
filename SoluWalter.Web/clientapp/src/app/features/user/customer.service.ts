import { Injectable } from '@angular/core';

const TOKEN = 'myToken'

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor() { }

  saveToken(token: string): void{
      sessionStorage.setItem(TOKEN, token);
  }

  isLogged(){
    return sessionStorage.getItem(TOKEN) != null;
  }

  logout(){
    sessionStorage.removeItem(TOKEN);
  }
}

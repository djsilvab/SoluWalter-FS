import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { CustomerService } from './customer.service';

@Injectable({
  providedIn: 'root'
})
export class NeedauthGuard implements CanActivate {
    
    constructor(
        private customerService: CustomerService,
        private router:Router
    ) {
        
        
    }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

        //const redirectUrl = route['_routerState']['url'];
      const redirectUrl = route.url;
      console.log("redirectUrl:", redirectUrl);

        if(this.customerService.isLogged()){
            return true;
        }

        this.router.navigateByUrl(
            this.router.createUrlTree(
                ['/users'],{
                    queryParams:{
                        redirectUrl
                    }
                }
            )
        )

        return false;
  }
  
}

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NeedauthGuard } from '../user/needauth.guard';
import { PostActualizarComponent } from './post-actualizar/post-actualizar.component';
import { PostCrearComponent } from './post-crear/post-crear.component';
import { PostDetalleComponent } from './post-detalle/post-detalle.component';
import { PostListarComponent } from './post-listar/post-listar.component';

const routes: Routes = [
    { path: "posts", component: PostListarComponent, canActivate: [NeedauthGuard]},
    { path: "posts/crear", component: PostCrearComponent, canActivate: [NeedauthGuard] },
    { path: "posts/details/:id", component: PostDetalleComponent, canActivate: [NeedauthGuard] },    
    { path: "posts/actualizar/:id", component: PostActualizarComponent, canActivate: [NeedauthGuard] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PostRoutingModule { }

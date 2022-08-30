import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { PostRoutingModule } from './post-routing.module';
import { PostListarComponent } from './post-listar/post-listar.component';
import { PostCrearComponent } from './post-crear/post-crear.component';
import { PostDetalleComponent } from './post-detalle/post-detalle.component';
import { PostActualizarComponent } from './post-actualizar/post-actualizar.component';


@NgModule({
  declarations: [
    PostListarComponent,
    PostCrearComponent,
    PostDetalleComponent,
    PostActualizarComponent
  ],
  imports: [
    CommonModule,
    PostRoutingModule,
    HttpClientModule,
    FormsModule
  ]
})
export class PostModule { }

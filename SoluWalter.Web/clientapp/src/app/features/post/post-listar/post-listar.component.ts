import { Component, OnInit } from '@angular/core';
import { PostService } from '../post.service';

@Component({
  selector: 'app-post-listar',
  templateUrl: './post-listar.component.html',
  styleUrls: ['./post-listar.component.scss']
})
export class PostListarComponent implements OnInit {

    public posts: Array<any> = [];

  constructor(
        private postService:PostService
    ) { 

    }

  ngOnInit(): void {
    this.getAll();
  }

  //metodos privados
  getAll(){
    this.postService.getAll().subscribe(data => {
        this.posts = data;
    })
  }

  borrarPost(id:string){
    this.postService.delete(id).subscribe(() =>{        
        this.getAll();
    }, error =>{
        console.log(error);
    });
  }
}

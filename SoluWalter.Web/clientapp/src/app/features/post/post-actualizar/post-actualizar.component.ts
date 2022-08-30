import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PostService } from '../post.service';

@Component({
  selector: 'app-post-actualizar',
  templateUrl: './post-actualizar.component.html',
  styleUrls: ['./post-actualizar.component.scss']
})
export class PostActualizarComponent implements OnInit {
   post : any = {};

  constructor(
    private postService: PostService,
    private route: ActivatedRoute,
    private router: Router
    
  ) { }

  ngOnInit(): void {
      this.getOne(this.route.snapshot.params['id']);
  }

  getOne(id: string){
    this.postService.getOne(id).subscribe( data =>{        
        this.post = data;
    }, error => {
        console.log(error)
    });
  }

  actualizarPost(id: string){
      this.postService.update(id,this.post).subscribe((data: any) => {
          this.router.navigate(['/posts/details/', data.id]);
      }, error => {
          console.log(error);
      });
    }

}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PostService } from '../post.service';

@Component({
  selector: 'app-post-detalle',
  templateUrl: './post-detalle.component.html',
  styleUrls: ['./post-detalle.component.scss']
})
export class PostDetalleComponent implements OnInit {

    post: any = {};

  constructor(
    private route:ActivatedRoute,
    private postService: PostService
  ) { }

  ngOnInit(): void {
        
      this.getPostDetail(this.route.snapshot.params['id']);
  }

  getPostDetail(id: string){
    this.postService.getOne(id).subscribe(data =>{
        this.post = data;
    })

  }

}

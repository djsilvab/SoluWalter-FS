import { Component, OnInit } from '@angular/core';
import { PostService } from '../post.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-post-crear',
    templateUrl: './post-crear.component.html',
    styleUrls: ['./post-crear.component.scss']
})
export class PostCrearComponent implements OnInit {

    post: any = {};

    constructor(
        private postService: PostService,
        private router: Router

    ) { }

    ngOnInit(): void {
    }

    savePost() {        
        this.postService.save(this.post).subscribe((data: any) => {
            this.router.navigate(['/posts/details/', data.id]);
        }, error => {
            console.log(error);
        });
    }

}

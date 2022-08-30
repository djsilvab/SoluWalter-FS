import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UiModule } from './ui/ui.module';
import { HomeComponent } from './features/home/home.component';
import { PostModule } from './features/post/post.module';
import { UserModule } from './features/user/user.module';
import { NeedauthGuard } from './features/user/needauth.guard';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    UiModule,
    PostModule,
    UserModule
  ],
  providers: [NeedauthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }

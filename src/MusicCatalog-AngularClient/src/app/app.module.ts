import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { CookieService } from 'ngx-cookie-service';
import { ModalModule } from 'ngx-bootstrap/modal';

import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { NavMenuComponent } from  './nav-menu/nav-menu.component'
import { AuthInterceptor } from './_helpers/auth.interceptor';
import { ErrorInterceptor } from './_helpers/error.interceptor';
import { AlbumListComponent } from './album/list/album-list.component';
import { GenreComponent } from './genre/genre.component';
import { PerformerComponent } from './performer/performer.component';
import { UserComponent } from './user/user.component';
import { AddAlbumComponent } from './album/add-album/add-album.component';
import { EditAlbumComponent } from './album/edit-album/edit-album.component';
import { AddSongComponent } from './home/add-song/add-song.component';
import { EditSongComponent } from './home/edit-song/edit-song.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
    NavMenuComponent,
    AlbumListComponent,
    GenreComponent,
    PerformerComponent,
    UserComponent,
    AddAlbumComponent,
    EditAlbumComponent,
    AddSongComponent,
    EditSongComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ModalModule.forRoot(),
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    CookieService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

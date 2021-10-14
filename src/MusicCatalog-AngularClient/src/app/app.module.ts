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
import { AlbumListComponent } from './album/album-list/album-list.component';
import { GenreListComponent } from './genre/genre-list/genre-list.component';
import { AddGenreComponent } from './genre/add-genre/add-genre.component';
import { PerformerComponent } from './performer/performer.component';
import { UserListComponent } from './user/user-list/user-list.component';
import { AddAlbumComponent } from './album/add-album/add-album.component';
import { EditAlbumComponent } from './album/edit-album/edit-album.component';
import { AddSongComponent } from './home/add-song/add-song.component';
import { EditSongComponent } from './home/edit-song/edit-song.component';
import { EditUserComponent } from './user/edit-user/edit-user.component';
import { EditGenreComponent } from './genre/edit-genre/edit-genre.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
    NavMenuComponent,
    AlbumListComponent,
    GenreListComponent,
    AddGenreComponent,
    PerformerComponent,
    UserListComponent,
    AddAlbumComponent,
    EditAlbumComponent,
    AddSongComponent,
    EditSongComponent,
    EditUserComponent,
    EditGenreComponent
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

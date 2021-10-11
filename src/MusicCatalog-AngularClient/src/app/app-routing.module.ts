import { NgModule} from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { HomeComponent} from './home/home.component';
import { AuthGuard } from './_helpers/auth.guard';
import { AlbumListComponent } from './album/list/album-list.component';
import { GenreComponent } from './genre/genre.component';
import { PerformerComponent } from './performer/performer.component';
import { UserComponent } from './user/user.component';
import { AddAlbumComponent } from './album/add-album/add-album.component';
import { EditAlbumComponent } from './album/edit-album/edit-album.component';
import { AddSongComponent } from './home/add-song/add-song.component';
import { EditSongComponent } from './home/edit-song/edit-song.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'songs/add-song', component: AddSongComponent },
  { path: 'songs/:id/edit', component: EditSongComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'albums', component: AlbumListComponent, canActivate: [AuthGuard] },
  { path: 'albums/add-album', component: AddAlbumComponent },
  { path: 'albums/:id/edit', component: EditAlbumComponent },
  { path: 'genres', component: GenreComponent, canActivate: [AuthGuard] },
  { path: 'performers', component: PerformerComponent, canActivate: [AuthGuard]},
  { path: 'users', component: UserComponent, canActivate: [AuthGuard] },
  { path: '**', redirectTo: '' }
]

@NgModule({
  imports: [RouterModule.forRoot((routes))],
  exports: [RouterModule]
})
export class  AppRoutingModule { }

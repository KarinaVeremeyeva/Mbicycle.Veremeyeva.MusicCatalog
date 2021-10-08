import { NgModule} from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { HomeComponent} from './home/home.component';
import { AuthGuard } from './_helpers/auth.guard';
import { AlbumComponent } from './album/album.component';
import { GenreComponent } from './genre/genre.component';
import { PerformerComponent } from './performer/performer.component';
import { UserComponent } from './user/user.component';
import { AddAlbumComponent } from "./album/add-album/add-album.component";
import { AlbumDetailsComponent } from './album/album-details/album-details.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'albums', component: AlbumComponent },
  { path: 'albums/:id', component: AlbumDetailsComponent },
  { path: 'albums/add-album', component: AddAlbumComponent },
  { path: 'genres', component: GenreComponent },
  { path: 'performers', component: PerformerComponent },
  { path: 'users', component: UserComponent, canActivate: [AuthGuard] },
  { path: '**', redirectTo: '' }
]

@NgModule({
  imports: [RouterModule.forRoot((routes))],
  exports: [RouterModule]
})
export class  AppRoutingModule { }

import { NgModule} from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
  { path: '', loadChildren: () => import('./home/song.module').then(m => m.SongModule) },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'albums', loadChildren: () => import('./album/album.module').then(m => m.AlbumModule) },
  { path: 'genres', loadChildren: () => import('./genre/genre.module').then(m => m.GenreModule) },
  { path: 'performers', loadChildren: () => import('./performer/performer.module').then(m => m.PerformerModule) },
  { path: 'users', loadChildren: () => import('./user/user.module').then(m => m.UserModule) },
  { path: '**', redirectTo: '' }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class  AppRoutingModule { }

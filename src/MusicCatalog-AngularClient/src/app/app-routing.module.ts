import { NgModule} from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from './_helpers/auth.guard';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { UserListComponent } from './user/user-list/user-list.component';
import { EditUserComponent } from './user/edit-user/edit-user.component';

const routes: Routes = [
  { path: '', loadChildren: () => import('./home/song.module').then(m => m.SongModule) },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'albums', loadChildren: () => import('./album/album.module').then(m => m.AlbumModule) },
  { path: 'genres', loadChildren: () => import('./genre/genre.module').then(m => m.GenreModule) },
  { path: 'performers', loadChildren: () => import('./performer/performer.module').then(m => m.PerformerModule) },
  { path: 'users', component: UserListComponent, canActivate: [AuthGuard] },
  { path: 'users/:id/edit', component: EditUserComponent, canActivate: [AuthGuard] },
  { path: '**', redirectTo: '' }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class  AppRoutingModule { }

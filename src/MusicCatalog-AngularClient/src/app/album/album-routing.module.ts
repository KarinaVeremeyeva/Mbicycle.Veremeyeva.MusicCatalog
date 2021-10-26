import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from '../_helpers/auth.guard';
import { AlbumListComponent } from './album-list/album-list.component';
import { AddAlbumComponent } from './add-album/add-album.component';
import { EditAlbumComponent } from './edit-album/edit-album.component';

const routes: Routes = [
  { path: '', component: AlbumListComponent, canActivate: [AuthGuard] },
  { path: 'add-album', component: AddAlbumComponent,canActivate: [AuthGuard] },
  { path: ':id/edit', component: EditAlbumComponent, canActivate: [AuthGuard] }
]
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AlbumRoutingModule { }

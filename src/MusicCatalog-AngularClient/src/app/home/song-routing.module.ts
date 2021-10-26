import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from '../_helpers/auth.guard';
import { HomeComponent } from './home.component';
import { AddSongComponent } from './add-song/add-song.component';
import { EditSongComponent } from './edit-song/edit-song.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'songs/add-song', component: AddSongComponent, canActivate: [AuthGuard] },
  { path: 'songs/:id/edit', component: EditSongComponent, canActivate: [AuthGuard] },
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SongRoutingModule { }

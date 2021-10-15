import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard } from '../_helpers/auth.guard';
import { GenreListComponent } from './genre-list/genre-list.component';
import { AddGenreComponent } from './add-genre/add-genre.component';
import { EditGenreComponent } from './edit-genre/edit-genre.component';

const routes: Routes = [
  { path: '', component: GenreListComponent, canActivate: [AuthGuard] },
  { path: 'add-genre', component: AddGenreComponent,canActivate: [AuthGuard] },
  { path: ':id/edit', component: EditGenreComponent, canActivate: [AuthGuard] }
]
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GenreRoutingModule { }

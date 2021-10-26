import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard } from '../_helpers/auth.guard';
import { PerformerListComponent} from './performer-list/performer-list.component';
import { AddPerformerComponent } from './add-performer/add-performer.component';
import { EditPerformerComponent } from './edit-performer/edit-performer.component';

const routes: Routes = [
  { path: '', component: PerformerListComponent, canActivate: [AuthGuard] },
  { path: 'add-performer', component: AddPerformerComponent,canActivate: [AuthGuard] },
  { path: ':id/edit', component: EditPerformerComponent, canActivate: [AuthGuard] }
]
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PerformerRoutingModule { }

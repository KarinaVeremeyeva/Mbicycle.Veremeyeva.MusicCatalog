import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { PerformerRoutingModule } from './performer-routing.module';
import { PerformerListComponent } from './performer-list/performer-list.component';
import { AddPerformerComponent } from './add-performer/add-performer.component';
import { EditPerformerComponent } from './edit-performer/edit-performer.component';

@NgModule({
  declarations: [
    PerformerListComponent,
    AddPerformerComponent,
    EditPerformerComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    PerformerRoutingModule
  ]
})
export class PerformerModule { }

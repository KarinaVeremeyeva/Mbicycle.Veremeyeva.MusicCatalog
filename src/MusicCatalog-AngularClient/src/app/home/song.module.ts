import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { SongRoutingModule } from './song-routing.module';
import { HomeComponent} from './home.component';
import { AddSongComponent } from './add-song/add-song.component';
import { EditSongComponent } from './edit-song/edit-song.component';
import { FilterPipe } from '../filter.pipe';

@NgModule({
  declarations: [
    HomeComponent,
    AddSongComponent,
    EditSongComponent,
    FilterPipe
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SongRoutingModule,
  ]
})
export class SongModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';


import { AlbumRoutingModule } from './album-routing.module';
import { AlbumListComponent } from './album-list/album-list.component';
import { AddAlbumComponent } from './add-album/add-album.component';
import { EditAlbumComponent } from './edit-album/edit-album.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AlbumListComponent,
    EditAlbumComponent,
    AddAlbumComponent
  ],
  imports: [
    CommonModule,
    AlbumRoutingModule,
    FormsModule,
  ]
})
export class AlbumModule { }

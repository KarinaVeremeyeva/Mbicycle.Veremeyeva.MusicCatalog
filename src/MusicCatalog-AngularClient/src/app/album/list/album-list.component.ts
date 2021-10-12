import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

import { Album } from '../../_models/album';
import { AlbumService } from '../../_services/album.service';

@Component({
  selector: 'app-album',
  templateUrl: './album-list.component.html'
})
export class AlbumListComponent implements OnInit {
  public albums: Album[];
  modalRef: BsModalRef = new BsModalRef<any>();
  idToBeDeleted!: number;
  message: string | undefined;
  config = {
    class: 'modal-sm modal-dialog-centered'
  }

  constructor(
    private albumService: AlbumService,
    private modalService: BsModalService) {
    this.albums = [];
  }

  ngOnInit(): void {
    this.getAlbumsList();
  }

  getAlbumsList() {
    this.albumService.getAlbums().subscribe(response => {
      this.albums = response;
    });
  }

  deleteAlbum(id: number) {
    this.albumService.deleteAlbum(id).subscribe(() => {
      this.albums = this.albums.filter(item => item.albumId !== id);
    })
  }

  confirmDeleteModal(template: TemplateRef<any>, id: any){
    this.modalRef = this.modalService.show(template, this.config);
    this.idToBeDeleted = id;
  }

  confirm(): void {
    this.message = 'Confirmed!';
    this.modalRef.hide();
    this.delete();
  }

  delete():void{
    this.deleteAlbum(this.idToBeDeleted);
    console.log('deleted',this.idToBeDeleted,' record');
  }

  decline(): void {
    this.message = 'Declined!';
    this.modalRef.hide();
  }
}

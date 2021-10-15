import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService} from 'ngx-bootstrap/modal';

import { Song } from '../_models/song';
import { SongService } from '../_services/song.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
  loading = false;
  songs: Song[] = [];
  modalRef: BsModalRef = new BsModalRef<any>();
  idToBeDeleted!: number;
  config = {
    class: 'modal-sm modal-dialog-centered'
  }

  constructor(
    private songService: SongService,
    private modalService: BsModalService) { }

  ngOnInit(): void {
    this.loading = true;
    this.songService.getSongs().subscribe(response => {
      this.loading = false;
      this.songs = response;
    })
  }

  confirmDeleteModal(template: TemplateRef<any>, id: any){
    this.modalRef = this.modalService.show(template, this.config);
    this.idToBeDeleted = id;
  }

  confirm(): void {
    this.modalRef.hide();
    this.deleteSong();
  }

  deleteSong():void{
    this.songService.deleteSong(this.idToBeDeleted).subscribe(() => {
      this.songs = this.songs.filter(item => item.songId !== this.idToBeDeleted);
    });
    console.log(`Song with id = ${this.idToBeDeleted} was deleted`);
  }

  decline(): void {
    this.modalRef.hide();
  }
}

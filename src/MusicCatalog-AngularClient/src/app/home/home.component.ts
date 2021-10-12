import {Component, OnInit, TemplateRef} from '@angular/core';
import { Song } from '../_models/song';
import { SongService } from '../_services/song.service';
import { BsModalRef, BsModalService} from "ngx-bootstrap/modal";

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

  deleteSong(id: number) {
    this.songService.deleteSong(id).subscribe(() => {
      this.songs = this.songs.filter(item => item.songId !== id);
    })
  }

  confirmDeleteModal(template: TemplateRef<any>, id: any){
    this.modalRef = this.modalService.show(template, this.config);
    this.idToBeDeleted = id;
  }

  confirm(): void {
    this.modalRef.hide();
    this.delete();
  }

  delete():void{
    this.deleteSong(this.idToBeDeleted);
    console.log('deleted',this.idToBeDeleted,' record');
  }

  decline(): void {
    this.modalRef.hide();
  }
}

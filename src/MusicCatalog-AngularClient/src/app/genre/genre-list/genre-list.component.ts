import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

import { Genre } from '../../_models/genre';
import { GenreService } from '../../_services/genre.service';

@Component({
  selector: 'app-genre',
  templateUrl: './genre-list.component.html'
})
export class GenreListComponent implements OnInit {
  loading = false;
  genres: Genre[];
  modalRef: BsModalRef = new BsModalRef<any>();
  idToBeDeleted!: number;
  message: string | undefined;
  config = {
    class: 'modal-sm modal-dialog-centered'
  }

  constructor(
    private genreService: GenreService,
    private modalService: BsModalService)
  {
    this.genres = [];
  }

  ngOnInit(): void {
    this.loading = true;
    this.getGenresList();
  }

  getGenresList() {
    this.genreService.getGenres().subscribe(response => {
      this.loading = false;
      this.genres = response;
    });
  }

  confirmDeleteModal(template: TemplateRef<any>, id: any){
    this.modalRef = this.modalService.show(template, this.config);
    this.idToBeDeleted = id;
  }

  confirm(): void {
    this.message = 'Confirmed!';
    this.modalRef.hide();
    this.deleteGenre();
  }

  deleteGenre():void{
    this.genreService.deleteGenre(this.idToBeDeleted).subscribe(() => {
      this.genres = this.genres.filter(item => item.genreId !== this.idToBeDeleted);
    })
    console.log(`Genre with id = ${this.idToBeDeleted} was deleted`);
  }

  decline(): void {
    this.message = 'Declined!';
    this.modalRef.hide();
  }
}

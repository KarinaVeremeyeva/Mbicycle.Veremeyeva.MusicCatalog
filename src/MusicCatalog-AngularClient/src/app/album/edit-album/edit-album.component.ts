import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators} from '@angular/forms';
import { ActivatedRoute, Router} from '@angular/router';

import { Album } from '../../_models/album';
import { AlbumService } from '../../_services/album.service';

@Component({
  selector: 'app-edit-album',
  templateUrl: './edit-album.component.html'
})
export class EditAlbumComponent implements OnInit {
  currentAlbum: Album = new Album;
  editForm;

  constructor(
    private albumService: AlbumService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder)
  {
    this.editForm = this.formBuilder.group({
      name: ['', Validators.required],
      releaseDate: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.currentAlbum.albumId = this.route.snapshot.params['id'];

    this.albumService.getAlbum(this.currentAlbum.albumId).subscribe((response: Album) => {
      this.currentAlbum = response;
    });
  }

  onSubmit(formData) {
    formData.value.albumId = this.currentAlbum.albumId;

    this.albumService.putAlbum(formData.value).subscribe(() => {
      this.router.navigateByUrl('albums').then();
    })
  }

}

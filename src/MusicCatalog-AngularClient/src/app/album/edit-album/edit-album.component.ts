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
  album: Album = new Album;
  editForm;

  constructor(
    private albumService: AlbumService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder)
  {
    this.editForm = this.formBuilder.group({
      albumId: [''],
      name: ['', Validators.required],
      releaseDate: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.album.albumId = this.route.snapshot.params['id'];

    this.albumService.getAlbum(this.album.albumId).subscribe((response: Album) => {
      this.album = response;
    });
  }

  onSubmit(formData) {
    this.albumService.putAlbum(formData.value).subscribe((response => {
      this.router.navigateByUrl('albums')
    }))
  }

}

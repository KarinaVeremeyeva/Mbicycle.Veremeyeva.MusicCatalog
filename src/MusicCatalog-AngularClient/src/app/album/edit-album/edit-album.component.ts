import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Album } from '../../_models/album';
import { AlbumService } from '../../_services/album.service';

@Component({
  selector: 'app-edit-album',
  templateUrl: './edit-album.component.html'
})
export class EditAlbumComponent implements OnInit {
  currentAlbum: Album = new Album();
  editForm: FormGroup;
  submitted = false;

  constructor(
    private albumService: AlbumService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder)
  {
    this.editForm = this.formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      releaseDate: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.currentAlbum.albumId = this.route.snapshot.params['id'];
    this.albumService.getAlbum(this.currentAlbum.albumId)
      .subscribe((response: Album) => {
        this.currentAlbum = response;
        this.editForm.controls["name"].setValue(this.currentAlbum.name);
        this.editForm.controls["releaseDate"].setValue(this.currentAlbum.releaseDate);
      });
  }

  get formField() { return this.editForm.controls; }

  onSubmit(formData) {
    this.submitted = true;
    if (this.editForm.invalid) {
      return;
    }

    formData.value.albumId = this.currentAlbum.albumId;
    this.albumService.putAlbum(formData.value).subscribe(() => {
      this.router.navigateByUrl('albums').then();
    });
  }
}

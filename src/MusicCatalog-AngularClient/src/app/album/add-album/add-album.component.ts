import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DatePipe } from '@angular/common';

import { Album } from '../../_models/album';
import { AlbumService } from '../../_services/album.service';

@Component({
  selector: 'app-add-album',
  templateUrl: './add-album.component.html',
  providers: [DatePipe]
})
export class AddAlbumComponent implements OnInit {
  album: Album = new Album();
  createForm: FormGroup;
  submitted = false;

  constructor(
    private albumService: AlbumService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder,
    private datePipe: DatePipe)
  {
    this.createForm = this.formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      releaseDate: [this.datePipe.transform(new Date(), 'mediumDate'), [Validators.required]]
    });
  }

  ngOnInit(): void { }

  get formField() { return this.createForm.controls; }

  onSubmit(formData): void {
    this.submitted = true;
    if (this.createForm.invalid) {
      return;
    }

    this.albumService.postAlbum(formData.value).subscribe( () => {
      this.router.navigateByUrl('albums').then();
    });
  }
}

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Genre } from '../../_models/genre';
import { GenreService } from '../../_services/genre.service';

@Component({
  selector: 'app-add-genre',
  templateUrl: './add-genre.component.html'
})
export class AddGenreComponent implements OnInit {
  genre: Genre = new Genre();
  createForm: FormGroup;
  submitted = false;

  constructor(
    private genreService: GenreService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder)
  {
    this.createForm = this.formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
    });
  }

  ngOnInit(): void { }

  get formField() { return this.createForm.controls; }

  onSubmit(formData): void {
    this.submitted = true;
    if (this.createForm.invalid) {
      return;
    }

    this.genreService.postGenre(formData.value).subscribe(() => {
      this.router.navigateByUrl('genres').then();
    });
  }
}

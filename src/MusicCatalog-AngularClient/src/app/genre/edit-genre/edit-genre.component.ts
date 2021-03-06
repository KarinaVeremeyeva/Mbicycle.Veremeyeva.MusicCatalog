import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Genre } from '../../_models/genre';
import { GenreService } from '../../_services/genre.service';

@Component({
  selector: 'app-edit-genre',
  templateUrl: './edit-genre.component.html'
})
export class EditGenreComponent implements OnInit {
  currentGenre: Genre = new Genre();
  editForm: FormGroup;
  submitted = false;

  constructor(
    private genreService: GenreService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder)
  {
    this.editForm = this.formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(3)]]
    });
  }

  ngOnInit(): void {
    this.currentGenre.genreId = this.route.snapshot.params['id'];
    this.genreService.getGenre(this.currentGenre.genreId)
      .subscribe((response: Genre) => {
        this.currentGenre = response;
        this.editForm.controls["name"].setValue(this.currentGenre.name);
      });
  }

  get formField() { return this.editForm.controls; }

  onSubmit(formData) {
    this.submitted = true;
    if (this.editForm.invalid) {
      return;
    }

    formData.value.genreId = this.currentGenre.genreId;
    this.genreService.putGenre(formData.value).subscribe(() => {
        this.router.navigateByUrl('genres').then();
    });
  }
}

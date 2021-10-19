import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Performer } from '../../_models/performer';
import { PerformerService } from '../../_services/performer.service';

@Component({
  selector: 'app-add-performer',
  templateUrl: './add-performer.component.html'
})
export class AddPerformerComponent implements OnInit {
  performer: Performer = new Performer();
  createForm: FormGroup;
  submitted = false;

  constructor(
    private performerService: PerformerService,
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

  onSubmit() {
    this.submitted = true;
    if (this.createForm.invalid) {
      return;
    }

    this.performerService.postPerformer(this.performer).subscribe(() => {
      this.router.navigateByUrl('performers').then();
    });
  }
}

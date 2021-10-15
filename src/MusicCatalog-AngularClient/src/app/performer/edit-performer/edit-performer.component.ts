import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators} from '@angular/forms';
import { ActivatedRoute, Router} from '@angular/router';

import { Performer } from '../../_models/performer';
import { PerformerService } from '../../_services/performer.service';

@Component({
  selector: 'app-edit-performer',
  templateUrl: './edit-performer.component.html'
})
export class EditPerformerComponent implements OnInit {
  currentPerformer: Performer = new Performer();
  editForm;

  constructor(
    private performerService: PerformerService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {
    this.editForm = this.formBuilder.group({
      name: ['', Validators.required]
    });

    this.currentPerformer.performerId = this.route.snapshot.params['id'];

    this.performerService.getPerformer(this.currentPerformer.performerId)
      .subscribe((response: Performer) => {
        this.currentPerformer = response;
        this.editForm.controls["name"].setValue(this.currentPerformer.name)
      });
  }

  onSubmit(formData) {
    formData.value.performerId = this.currentPerformer.performerId;

    this.performerService.putPerformer(formData.value).subscribe(() => {
      this.router.navigateByUrl('performers').then();
    });
  }
}

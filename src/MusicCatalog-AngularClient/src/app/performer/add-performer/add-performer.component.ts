import { Component, OnInit } from '@angular/core';

import { Performer } from '../../_models/performer';
import { PerformerService } from '../../_services/performer.service';

@Component({
  selector: 'app-add-performer',
  templateUrl: './add-performer.component.html'
})
export class AddPerformerComponent implements OnInit {
  performer: Performer = new Performer();
  submitted = false;
  errorMessage = '';

  constructor(private performerService: PerformerService) { }

  ngOnInit(): void {
  }

  createPerformer(): void {
    this.performerService.postPerformer(this.performer)
      .subscribe(() => {
        this.submitted = true;
      },
        err => {
        this.errorMessage = err;
      });
  }
}

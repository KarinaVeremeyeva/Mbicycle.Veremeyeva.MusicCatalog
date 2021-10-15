import { Component, OnInit } from '@angular/core';

import { Performer } from '../_models/performer';
import { PerformerService } from '../_services/performer.service';

@Component({
  selector: 'app-performer',
  templateUrl: './performer-list.component.html'
})
export class PerformerListComponent implements OnInit {
  public performers: Performer[];

  constructor(private service: PerformerService) {
    this.performers = [];
  }

  ngOnInit(): void {
    this.getPerformerList();
  }

  getPerformerList() {
    this.service.getPerformers().subscribe(response => {
      this.performers = response;
    })
  }
}

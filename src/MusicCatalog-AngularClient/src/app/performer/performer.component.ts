import { Component, OnInit } from '@angular/core';
import { Performer } from '../_models/performer';
import  { PerformerService } from '../_services/performer.service';

@Component({
  selector: 'app-performer',
  templateUrl: './performer.component.html'
})
export class PerformerComponent implements OnInit {
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

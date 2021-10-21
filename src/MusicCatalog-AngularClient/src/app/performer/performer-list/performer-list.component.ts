import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ModalComponent } from '../../modal/modal.component';

import { Performer } from '../../_models/performer';
import { PerformerService } from '../../_services/performer.service';

@Component({
  selector: 'app-performer',
  templateUrl: './performer-list.component.html'
})
export class PerformerListComponent implements OnInit {
  performers: Performer[];

  constructor(
    private performerService: PerformerService,
    public dialog: MatDialog)
  {
    this.performers = [];
  }

  ngOnInit(): void {
    this.getPerformerList();
  }

  getPerformerList() {
    this.performerService.getPerformers().subscribe(response => {
      this.performers = response;
    });
  }

  deletePerformer(id: number): void {
    this.performerService.deletePerformer(id).subscribe(() => {
      this.performers = this.performers.filter(item => item.performerId !== id);
    });
  }

  openDialog(id: number): void {
    const dialogRef = this.dialog.open(ModalComponent, {
      data: 'Are you sure you want to delete this performer?'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.deletePerformer(id);
      }
    });
  }
}

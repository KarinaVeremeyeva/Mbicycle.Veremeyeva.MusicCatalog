import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

import { Performer } from '../../_models/performer';
import { PerformerService } from '../../_services/performer.service';

@Component({
  selector: 'app-performer',
  templateUrl: './performer-list.component.html'
})
export class PerformerListComponent implements OnInit {
  public performers: Performer[];
  modalRef: BsModalRef = new BsModalRef<any>();
  idToBeDeleted!: number;
  message: string | undefined;
  config = {
    class: 'modal-sm modal-dialog-centered'
  }

  constructor(
    private performerService: PerformerService,
    private modalService: BsModalService)
  {
    this.performers = [];
  }

  ngOnInit(): void {
    this.getPerformerList();
  }

  getPerformerList() {
    this.performerService.getPerformers().subscribe(response => {
      this.performers = response;
    })
  }

  confirmDeleteModal(template: TemplateRef<any>, id: any){
    this.modalRef = this.modalService.show(template, this.config);
    this.idToBeDeleted = id;
  }

  confirm(): void {
    this.message = 'Confirmed!';
    this.modalRef.hide();
    this.deletePerformer();
  }

  deletePerformer():void{
    this.performerService.deletePerformer(this.idToBeDeleted).subscribe(() => {
      this.performers = this.performers.filter(item => item.performerId !== this.idToBeDeleted);
    })
    console.log(`Performer with id = ${this.idToBeDeleted} was deleted`);
  }

  decline(): void {
    this.message = 'Declined!';
    this.modalRef.hide();
  }
}

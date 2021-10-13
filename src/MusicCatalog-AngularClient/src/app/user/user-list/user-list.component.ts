import { Component, OnDestroy, OnInit, TemplateRef} from '@angular/core';
import { first } from 'rxjs/operators';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

import { User } from '../../_models/user';
import { UserService } from '../../_services/user.service';
import { AuthService } from '../../_services/auth.service';
import {AuthUser} from "../../_models/auth-user";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-user',
  templateUrl: './user-list.component.html'
})
export class UserListComponent implements OnInit, OnDestroy {
  users: User[] = [];
  currentUser: AuthUser = new AuthUser();
  loading = false;
  modalRef: BsModalRef = new BsModalRef<any>();
  idToBeDeleted!: string;
  config = {
    class: 'modal-sm modal-dialog-centered'
  }
  constructor(
    private authService: AuthService,
    private userService: UserService,
    private modalService: BsModalService)
  {
    this.authService.currentUser.subscribe(user => {
      this.currentUser = user;
    })
  }

  ngOnInit(): void {
    this.loading = true;
    this.loadUsers()
  }

  ngOnDestroy() {
    //this.currentUserSubscription.unsubscribe();
  }

  private loadUsers() {
    this.userService.getUsers().pipe(first()).subscribe(users => {
      this.loading = false;
      this.users = users;
    });
  }

  confirmDeleteModal(template: TemplateRef<any>, id: any){
    this.modalRef = this.modalService.show(template, this.config);
    this.idToBeDeleted = id;
  }

  confirm(): void {
    this.modalRef.hide();
    this.deleteUser();
  }

  deleteUser():void{
    this.userService.deleteUser(this.idToBeDeleted).subscribe(() => {
      this.users = this.users.filter(item => item.id !== this.idToBeDeleted);
    });
    console.log(`User with id = ${this.idToBeDeleted} was deleted`);
  }

  decline(): void {
    this.modalRef.hide();
  }
}

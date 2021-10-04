import { Component, OnInit } from '@angular/core';

import { TokenStorageService } from './_services/token-storage.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit{
  constructor(private tokenStorageService: TokenStorageService) { }

  ngOnInit(): void {
  }

  logout(): void {
  this.tokenStorageService.signOut();
  window.location.reload();
}
}

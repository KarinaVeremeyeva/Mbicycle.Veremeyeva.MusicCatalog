import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-switch-language',
  templateUrl: './switch-language.component.html'
})
export class SwitchLanguageComponent implements OnInit {
  siteLanguage: string | undefined;
  siteLocale: string | undefined;
  localesList = [
    { code: 'en-US', label: 'English' },
    { code: 'ru', label: 'Русский' },
  ]
  constructor() { }

  ngOnInit(): void {
    this.siteLocale = window.location.pathname.split('/')[1]
    this.siteLanguage = this.localesList.find(
      (f) => f.code === this.siteLocale
    )?.label
    if (!this.siteLanguage) {
      this.onChange(this.localesList[0].code)
    }
  }

  onChange(selectedLangCode: string) {
    window.location.href = `/${selectedLangCode}`
  }
}

import { Component, isDevMode, OnInit } from '@angular/core';

@Component({
  selector: 'app-switch-language',
  templateUrl: './switch-language.component.html'
})
export class SwitchLanguageComponent implements OnInit {
  isDev = isDevMode()
  siteLanguage: string | undefined;
  siteLocale: string | undefined;
  languageList = [
    { code: 'en-US', label: 'English' },
    { code: 'ru', label: 'Русский' },
  ]
  constructor() { }

  ngOnInit(): void {
    this.siteLocale = window.location.pathname.split('/')[1]
    this.siteLanguage = this.languageList.find(
      (f) => f.code === this.siteLocale
    )?.label
    if (!this.siteLanguage) {
      this.onChange(this.languageList[0].code)
    }
  }

  onChange(selectedLangCode: string) {
    window.location.href = `/${selectedLangCode}`
  }
}

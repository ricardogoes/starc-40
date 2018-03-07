import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { CpfCnpjValidatorDirective } from './cpf-cnpj.directive';

@NgModule({
  imports: [
    BrowserModule
  ],
  declarations: [ CpfCnpjValidatorDirective ],
  exports: [ CpfCnpjValidatorDirective ]
})
export class CpfCnpjValidatorModule { }

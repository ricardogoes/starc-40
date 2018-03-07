import { NgModule } from '@angular/core';

import { CepFilter } from './cep.filter';
import { CpfCnpjFilter } from './cpf-cnpj.filter';
import { StatusFilter } from './status.filter';

@NgModule({
  declarations: [CepFilter, CpfCnpjFilter, StatusFilter],
  exports: [CepFilter, CpfCnpjFilter, StatusFilter]
})
export class FilterModule { }

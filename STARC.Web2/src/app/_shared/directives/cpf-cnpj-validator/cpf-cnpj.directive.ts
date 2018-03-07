import { Directive, SimpleChanges } from '@angular/core';
import { AbstractControl, FormControl, NG_VALIDATORS, Validator }from '@angular/forms';

import { CpfCnpjValidator } from './validators/cpf-cnpj.validator';


export function CpfCnpjValidate(control: FormControl){
    //const no = CpfCnpjValidator.validate(control);
    return CpfCnpjValidator.validate(control);//no ? {'validateCpfCnpj' : {control}} : name;
}

@Directive({
  selector: '[validateCpfCnpj][formControl],[validateCpfCnpj][formControlName],[validateCpfCnpj][ngModel]',
  providers: [{
    provide: NG_VALIDATORS, 
    useExisting: CpfCnpjValidatorDirective, 
    multi: true 
  }]
})
export class CpfCnpjValidatorDirective implements Validator {

    validate(control: AbstractControl): {[key: string]: any} {        
        return CpfCnpjValidator.validate(control);
    }
}
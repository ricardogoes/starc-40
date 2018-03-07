import { AbstractControl } from '@angular/forms';

import { CpfValidator } from './cpf.validator';
import { CnpjValidator } from './cnpj.validator';

export class CpfCnpjValidator {

    /**
     * Método estático responsável pela validação dos dados.
     *
     * @param AbstractControl control
     * @return object ou null caso válido
     */
    static validate(control: AbstractControl): {[key: string]: boolean} {
        if (this.cpfCnpjValido(control.value)) {            
            return null;
        }

        return { 'cpfCnpj': true };        
    }

	/**
     * Valida um CPF/CNPJ.
     *
     * @param cpfCnpj valor do cpf/cnpj a ser validado.
     * @return boolean informando se o cpf/cnpj é válido ou não.
     */
    static cpfCnpjValido(cpfCnpj: any): boolean {
        if (!cpfCnpj) {
            return false;
        }
        
        var cpfCnpjValor = cpfCnpj.replace(/\D/g, '');
        
        var cpfCnpjValido = false;        
        
        if (cpfCnpjValor.length === 11) {
            cpfCnpjValido = CpfValidator.cpfValido(cpfCnpjValor);
        } else if (cpfCnpjValor.length === 14) {
            cpfCnpjValido = CnpjValidator.cnpjValido(cpfCnpjValor);
        }
        
        return cpfCnpjValido;
    }
}
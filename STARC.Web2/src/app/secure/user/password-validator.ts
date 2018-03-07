import {AbstractControl} from '@angular/forms';
export class PasswordValidator {

    static MatchPassword(AC: AbstractControl): any {
       let password = AC.get('Password').value; // to get value in input tag
       let confirmPassword = AC.get('ConfirmPassword').value; // to get value in input tag
       
        if(password != '' && password != confirmPassword) {
            AC.get('ConfirmPassword').setErrors( {MatchPassword: true} )
        } else {
            AC.get('ConfirmPassword').setErrors( {MatchPassword: false} )
        }
    }
}
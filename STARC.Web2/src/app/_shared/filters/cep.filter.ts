import {Pipe, PipeTransform} from '@angular/core';

@Pipe({
	name: 'cep'
})
export class CepFilter implements PipeTransform{
	transform(value: string): string{
		if(value.length != 8)
			return "";
		
		return value.substr(0, 5) + '-' + value.substr(5, 3);
	}
}
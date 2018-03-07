import {} from 'jasmine';

import { CpfCnpjFilter } from './cpf-cnpj.filter'

describe('Filter: CpfCnpj', () =>
{
    let filter: CpfCnpjFilter;

    beforeEach(() =>
    {
        filter = new CpfCnpjFilter;
    });

    it('providing valid input without mask returns valid CPF', () =>
    {
        expect(filter.transform("34538886858")).toBe("345.388.868-58");
    });

    it('providing valid input with mask returns valid CPF', () =>
    {
        expect(filter.transform("345.388.868-58")).toBe("345.388.868-58");
    });

    it('providing valid input without mask returns valid CNPJ', () =>
    {
        expect(filter.transform("42245151000150")).toBe("42.245.151/0001-50");
    });

    it('providing valid input with mask returns valid CNPJ', () =>
    {
        expect(filter.transform("42.245.151/0001-50")).toBe("42.245.151/0001-50");
    });

    it('providing blank input must not return any result', () =>
    {
        expect(filter.transform("")).toBe("");
    });

    it('providing input with length different from 11 and 14 must not return any result', () =>
    {
        expect(filter.transform("345388868589")).toBe("");
    });
});
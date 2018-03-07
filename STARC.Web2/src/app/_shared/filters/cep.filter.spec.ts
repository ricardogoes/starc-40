import {} from 'jasmine';

import { CepFilter } from './cep.filter'

describe('Filter: CEP', () =>
{
    let filter: CepFilter;

    beforeEach(() =>
    {
        filter = new CepFilter;
    });

    it('Should CEP be validated with valid input', () =>
    {
        expect(filter.transform("03548000")).toBe("03548-000");
    });

    it('Should CEP not be validated when input has more than 8 characters (return blank)', () =>
    {
        expect(filter.transform("035480000")).toBe("");
    });

    it('Should CEP not be validated when input has less than 8 characters (return blank)', () =>
    {
        expect(filter.transform("0354800")).toBe("");
    });

});
import {} from 'jasmine';

import { StatusFilter } from './status.filter'

describe('Filter: Status', () =>
{
    let filter: StatusFilter;

    beforeEach(() =>
    {
        filter = new StatusFilter;
    });

    it('Should return Ativo when value is true', () =>
    {
        expect(filter.transform(true)).toBe("Ativo");
    });

    it('Should return Inativo when value is false', () =>
    {
        expect(filter.transform(false)).toBe("Inativo");
    });

     it('Should return Inativo when no value is passed', () =>
    {
        expect(filter.transform(null)).toBe("Inativo");
    });
});
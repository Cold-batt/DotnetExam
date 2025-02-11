import { useQuery } from '@tanstack/react-query';

import { IQueryOptions } from '@/shared/api/model';

import { ReferencesService } from '..';
import { ICountriesRequest, ICountryType } from '../model';

export const KEY_COUNTRIES = 'references/countries';

export const useCountries = (params: ICountriesRequest, options?: IQueryOptions<ICountryType[]>) =>
  useQuery({
    queryKey: [KEY_COUNTRIES],
    queryFn: () => ReferencesService.getCountries(params),
    ...options,
  });

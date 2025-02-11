import { apiExtract } from '../..';
import { ICountriesRequest, ICountryType, ITagsRequest } from './model';

export const ReferencesService = {
  async getTags(data?: ITagsRequest) {
    return await apiExtract.get<string[]>(`/references/tags`, { params: data });
  },

  async getCountries(data?: ICountriesRequest) {
    return await apiExtract.get<ICountryType[]>(`/references/countries`, { params: data });
  },
};

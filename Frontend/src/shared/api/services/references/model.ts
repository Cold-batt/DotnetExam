export interface IBaseListRequest {
  limit: number;
  offset?: number;
}

export interface IBaseListResponse<T> {
  total: number;
  items: T[];
}

export interface ITagsRequest extends IBaseListRequest {
  type:
    | 'place'
    | 'artwork'
    | 'profile'
    | 'organization'
    | 'artwork_type'
    | 'artwork_maker'
    | 'artwork_period'
    | 'artwork_materials'
    | 'artwork_dimensions'
    | 'artwork_markings'
    | 'artwork_features'
    | 'artwork_subject'
    | 'artwork_reference';
  tag?: string;
}

export interface ICountriesRequest extends IBaseListRequest {
  country?: string;
}

export type ITagsList = {
  items: string[];
};

export interface ICountryType {
  id?: number;
  iso?: string;
  iso3?: string;
  name?: string;
  phoneCode?: number;
  states?: IStateType[];
}

export interface IStateType {
  id?: number;
  code?: string;
  name?: string;
}
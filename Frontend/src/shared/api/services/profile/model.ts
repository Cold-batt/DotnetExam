import { ICountryType } from '../references/model';

export interface IArtUser {
  id?: string;
  avatarBackground?: string;
  email?: string;
  language?: string;
  firstname?: string;
  lastname?: string;
  username?: string;
  nickname?: string;
  description?: string;
  avatar?: string;
  organization?: string;
  social?: string[];
  images?: {
    avatar?: {
      xl?: string;
      l?: string;
      m?: string;
      s?: string;
      xs?: string;
    };
  };
  phone?: string;
  showEmail?: boolean;
  showPhone?: boolean;
  openingHours?: string;
  address?: {
    zip?: string;
    country?: {
      id?: number;
      iso?: string;
      iso3?: string;
      name?: string;
      phoneCode?: number;
      states?: {
        id?: number;
        code?: string;
        name?: string;
      }[];
    };
    state?: string;
    city?: string;
    line1?: string;
    line2?: string;
  };
  createdAt?: string;
  updatedAt?: string;
  website?: string;
  verified?: string;
  tags?: string[];
}

export interface IArtUserUpdate
  extends IArtUserUpdateGeneral,
    IArtUserUpdateContact,
    IArtUserUpdateLocation {}

export interface IArtUserUpdateGeneral {
  avatar?: string;
  firstname?: string;
  lastname?: string;
  nickname?: string;
  username?: string;
  tags?: string[];
  description?: string;
  organization?: string;
  avatarBackground?: string;
}

export const IArtUserUpdateGeneralFields = [
  'avatar',
  'firstname',
  'lastname',
  'nickname',
  'username',
  'tags',
  'description',
  'organization',
  'avatarBackground',
];

export interface IArtUserUpdateContact {
  showEmail?: boolean;
  showPhone?: boolean;
  phone?: string;
  website?: string;
  social?: string[];
}

export const IArtUserUpdateContactFields = ['showEmail', 'showPhone', 'phone', 'website', 'social'];

export interface IArtUserUpdateLocation {
  address?: {
    zip?: string;
    country?: ICountryType;
    state?: string;
    city?: string;
    line1?: string;
    line2?: string;
  };
}

export const IArtUserUpdateLocationFields = ['address'];

export interface ICheckUsernameResponse {
  isValid: boolean;
}

export interface IChangePasswordRequest {
  password: string;
}

export interface IDeleteAccountRequest {
  email: string;
}

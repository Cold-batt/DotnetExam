import { TwinVisabilityStatus } from '@/shared/constants';

import { IBaseListRequest } from '../references/model';

export interface ICreateTwinData {
  title: string;
  description: string;
  tags: string[];
  maker: string;
  period: string;
  materials: string;
  reference: string;
  dimensions: string;
  markings: string;
  features: string;
  subject: string;
  properties: { trait_type: string; value: string }[];
}

// interface IPostTwinPhotos {}

export interface ITwinsRequest extends IBaseListRequest {
  status?: string;
  with_videos?: boolean;
  with_images?: boolean;
}

export interface ITwinsResponse {
  total: number;
  items: ITwin[];
}

export interface ITwin {
  certificate_domain?: string;
  client?: string;
  created_at?: string;
  domain?: string;
  domain_status?: string;
  expired?: string;
  expired_at?: string;
  gallery?: string;
  id?: string;
  images?: Record<string, unknown>;
  ip_rights_address?: null | string;
  ip_rights_links?: string[];
  ip_rights_status?: string;
  ip_rights_updated?: null | string;
  issuer?: IIssuer;
  maker?: string;
  marketplaceLinks?: null | string;
  nft?: string[];
  nft_amount?: number;
  nft_collection?: null | string;
  nft_minting?: string[];
  nft_minting_amount?: number;
  nft_status?: string;
  preview?: string;
  priority?: number;
  properties?: unknown[];
  public_pdf?: boolean;
  sale?: ISale;
  social?: string[];
  status?: TwinVisabilityStatus;
  subdomain_name?: null | string;
  tags?: string[];
  title?: string;
  updated_at?: string;
  videos?: IVideoLinks;
  _acl?: string[];
}

interface IIssuer {
  firstname: string;
  lastname: string;
  nickname: string;
  hideRealname: boolean;
}

interface ISale {
  price: number;
  currency: string;
  status: string;
}

interface IVideoLinks {
  preview: IFileSizes;
  image: IFileSizes;
}

interface IFileSizes {
  v63x48: string;
  v112x112: string;
  v118x89: string;
  v170x170: string;
  v200x200: string;
  v325x244: string;
  v970x548: string;
  v1440x700: string;
}

export interface IUploadTwinItemRequest {
  file: string;
  id: string;
}

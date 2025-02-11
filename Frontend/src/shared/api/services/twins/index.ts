import { dataURLToFile } from '@/shared/utils/functions';

import { apiExtract } from '../..';
import {
  ICreateTwinData,
  ITwin,
  ITwinsRequest,
  ITwinsResponse,
  IUploadTwinItemRequest,
} from './model';

export const TwinService = {
  async createTwin(data: ICreateTwinData) {
    return await apiExtract.post<string>('/twins/', data);
  },

  async getTwinsList(data?: ITwinsRequest) {
    return await apiExtract.get<ITwinsResponse>(`/twins/list/my`, {
      params: data,
    });
  },

  async deleteTwin(data: string): Promise<void> {
    return await apiExtract.delete(`/twins/${data}`);
  },

  async getTwin(id: string) {
    return await apiExtract.get<ITwin>(`/twins/${id}`);
  },

  async getTwinQr(id: string) {
    return await apiExtract.get<string>(`/twins/${id}/qr`);
  },

  async publishTwin(id: string): Promise<void> {
    return await apiExtract.put(`/twins/${id}/publish`);
  },

  async unpublishTwin(id: string): Promise<void> {
    return await apiExtract.delete(`/twins/${id}/unpublish`);
  },

  async uploadTwinImage(data: IUploadTwinItemRequest): Promise<void> {
    const formData = new FormData();

    formData.append('image', dataURLToFile(data.file), 'twin_img');

    return await apiExtract.post(`/twins/${data.id}/images`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
  },

  async uploadTwinVideo(data: IUploadTwinItemRequest): Promise<void> {
    const formData = new FormData();

    formData.append('video', dataURLToFile(data.file), 'twin_img');

    return await apiExtract.post(`/twins/${data.id}/video`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
  },

  async disconnectFromTwin({ twin_id, domain_id }: { twin_id: string; domain_id: string }) {
    return await apiExtract.delete<unknown>(`/twins/${twin_id}/domain/${domain_id}`);
  },

  async connectToTwin({ twin_id, domain_id }: { twin_id: string; domain_id: string }) {
    return await apiExtract.put<unknown>(`/twins/${twin_id}/domain/${domain_id}`);
  },
};

import { dataURLToFile } from '@/shared/utils/functions';

import { apiExtract } from '../..';
import {
  IArtUser,
  IArtUserUpdate,
  IChangePasswordRequest,
  ICheckUsernameResponse,
  IDeleteAccountRequest,
} from './model';

export const ProfileService = {
  async getUser() {
    return await apiExtract.get<IArtUser>('/users/me');
  },

  async patchUser(data: IArtUserUpdate) {
    return await apiExtract.patch<IArtUser>('users/me', data);
  },

  async getCheckUsername(data: string) {
    return await apiExtract.get<ICheckUsernameResponse>(`/users/check/${data}`);
  },

  async putChangePassword(data: IChangePasswordRequest): Promise<void> {
    return await apiExtract.put('users/me/password', data);
  },

  async deleteAccount(data: IDeleteAccountRequest): Promise<void> {
    return await apiExtract.put('users/me/delete', data);
  },

  async postAvatar(data: string): Promise<void> {
    const formData = new FormData();

    formData.append('image', dataURLToFile(data), 'user_avatar');

    return await apiExtract.post('users/me/avatar', formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
  },

  async getQr() {
    return await apiExtract.get<string>('users/me/qr');
  },

  async getCertificate() {
    return await apiExtract.get<string>('users/me/certificate');
  },
};

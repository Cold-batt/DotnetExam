import { useMutation } from '@tanstack/react-query';

import { IMutationOptions } from '@/shared/api/model';

import { ProfileService } from '..';
import { IChangePasswordRequest } from '../model';

export const KEY_CHANGE = 'users/me/password';

export const useChangePassword = (options?: IMutationOptions<void, IChangePasswordRequest>) =>
  useMutation({
    mutationKey: [KEY_CHANGE],
    mutationFn: (data) => ProfileService.putChangePassword(data),
    ...options,
  });

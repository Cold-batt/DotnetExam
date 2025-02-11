import { useMutation } from '@tanstack/react-query';

import { IMutationOptions } from '@/shared/api/model';

import { ProfileService } from '..';
import { IDeleteAccountRequest } from '../model';

export const KEY_DELETE = 'users/me/delete';

export const useDeleteAccount = (options?: IMutationOptions<void, IDeleteAccountRequest>) =>
  useMutation({
    mutationKey: [KEY_DELETE],
    mutationFn: (data) => ProfileService.deleteAccount(data),
    ...options,
  });

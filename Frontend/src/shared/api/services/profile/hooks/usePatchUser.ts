import { useMutation } from '@tanstack/react-query';

import { IMutationOptions } from '@/shared/api/model';

import { ProfileService } from '..';
import { IArtUser, IArtUserUpdate } from '../model';

export const KEY_EDIT_USER = 'users/patch/me';

export const usePatchUser = (options?: IMutationOptions<IArtUser, IArtUserUpdate>) =>
  useMutation({
    mutationKey: [KEY_EDIT_USER],
    mutationFn: (data) => ProfileService.patchUser(data),
    ...options,
  });

import { useMutation } from '@tanstack/react-query';

import { IMutationOptions } from '@/shared/api/model';

import { ProfileService } from '..';

export const KEY_AVATAR = 'users/me/avatar';

export const useUpdateAvatar = (options?: IMutationOptions<void, string>) =>
  useMutation({
    mutationKey: [KEY_AVATAR],
    mutationFn: (data) => ProfileService.postAvatar(data),
    ...options,
  });

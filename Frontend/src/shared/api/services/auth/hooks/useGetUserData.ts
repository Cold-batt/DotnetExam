import { useMutation } from '@tanstack/react-query';

import { IMutationOptions } from '@/shared/api/model';

import { AuthService } from '..';
import { IUserDataRequest } from '../model';

export const KEY_GET_USER = 'User/getUserData';

export const useGetUserData = (options?: IMutationOptions<IUserDataRequest>) =>
  useMutation({
    mutationKey: [KEY_GET_USER],
    mutationFn: () => AuthService.getUserData(),
    ...options,
  });

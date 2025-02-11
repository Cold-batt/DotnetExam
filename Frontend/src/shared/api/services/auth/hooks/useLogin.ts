import { useMutation } from '@tanstack/react-query';

import { IMutationOptions } from '@/shared/api/model';

import { AuthService } from '..';
import { ISignInRequest, ISignInResponse } from '../model';

export const KEY_LOGIN = 'User/signIn';

export const useLogin = (options?: IMutationOptions<ISignInResponse, ISignInRequest>) =>
  useMutation({
    mutationKey: [KEY_LOGIN],
    mutationFn: (data) => AuthService.login(data),
    ...options,
  });

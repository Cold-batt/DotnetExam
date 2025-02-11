import { useMutation } from '@tanstack/react-query';

import { IMutationOptions } from '@/shared/api/model';

import { AuthService } from '..';
import { ISignInResponse, ISignUpRequest } from '../model';

export const KEY_REGISTRATION = 'users/registration';

export const useRegistration = (options?: IMutationOptions<ISignInResponse, ISignUpRequest>) =>
  useMutation({
    mutationKey: [KEY_REGISTRATION],
    mutationFn: (data) => AuthService.registration(data),
    ...options,
  });

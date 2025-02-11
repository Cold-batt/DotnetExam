import { useMutation } from '@tanstack/react-query';

import { IMutationOptions } from '@/shared/api/model';

import { AuthService } from '..';
import { ISignUpRequest } from '../model';

export const KEY_REGISTRATION = 'users/registration';

export const useRegistration = (options?: IMutationOptions<void, ISignUpRequest>) =>
  useMutation({
    mutationKey: [KEY_REGISTRATION],
    mutationFn: (data) => AuthService.registration(data),
    ...options,
  });

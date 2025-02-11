import { useMutation } from '@tanstack/react-query';

import { IMutationOptions } from '@/shared/api/model';

import { TwinService } from '..';
import { ICreateTwinData } from '../model';

export const KEY_CREATE_TWIN = 'createTwin';

export const useCreateTwin = (options?: IMutationOptions<string, ICreateTwinData>) =>
  useMutation({
    mutationKey: [KEY_CREATE_TWIN],
    mutationFn: (data) => TwinService.createTwin(data),
    ...options,
  });

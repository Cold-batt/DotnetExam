import { useMutation } from '@tanstack/react-query';

import { IMutationOptions } from '@/shared/api/model';

import { TwinService } from '..';

export const KEY_DELETE = 'twins/delete';

export const useDeleteTwin = (options?: IMutationOptions<void, string>) =>
  useMutation({
    mutationKey: [KEY_DELETE],
    mutationFn: (data) => TwinService.deleteTwin(data),
    ...options,
  });

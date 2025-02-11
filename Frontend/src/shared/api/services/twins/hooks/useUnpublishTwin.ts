import { useMutation } from '@tanstack/react-query';

import { IMutationOptions } from '@/shared/api/model';

import { TwinService } from '..';

export const KEY_UNPUBLISH = 'twins/put/unpublish';

export const useUnpublishTwin = (options?: IMutationOptions<void, string>) =>
  useMutation({
    mutationKey: [KEY_UNPUBLISH],
    mutationFn: (id: string) => TwinService.unpublishTwin(id),
    ...options,
  });

import { useMutation } from '@tanstack/react-query';

import { IMutationOptions } from '@/shared/api/model';

import { TwinService } from '..';

export const KEY_PUBLISH = 'twins/put/publish';

export const usePublishTwin = (options?: IMutationOptions<void, string>) =>
  useMutation({
    mutationKey: [KEY_PUBLISH],
    mutationFn: (id: string) => TwinService.publishTwin(id),
    ...options,
  });

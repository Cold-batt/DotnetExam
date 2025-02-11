import { useMutation } from '@tanstack/react-query';

import { IMutationOptions } from '@/shared/api/model';

import { TwinService } from '..';

export const KEY_CONNECT_TO_TWIN = 'connectTOTWin';

export const useConnectToTwin = (
  options?: IMutationOptions<unknown, { twin_id: string; domain_id: string }>,
) =>
  useMutation({
    mutationKey: [KEY_CONNECT_TO_TWIN],
    mutationFn: (data) => TwinService.connectToTwin(data),
    ...options,
  });

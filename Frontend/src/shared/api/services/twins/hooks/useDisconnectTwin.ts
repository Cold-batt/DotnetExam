import { useMutation } from '@tanstack/react-query';

import { IMutationOptions } from '@/shared/api/model';

import { TwinService } from '..';

export const KEY_DISCONNECT_TWIN = 'disconnectTWin';

export const useDisconnectTwin = (
  options?: IMutationOptions<unknown, { twin_id: string; domain_id: string }>,
) =>
  useMutation({
    mutationKey: [KEY_DISCONNECT_TWIN],
    mutationFn: (data) => TwinService.disconnectFromTwin(data),
    ...options,
  });

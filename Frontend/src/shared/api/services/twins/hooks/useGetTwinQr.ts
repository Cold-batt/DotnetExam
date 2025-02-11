import { useQuery } from '@tanstack/react-query';

import { IQueryOptions } from '@/shared/api/model';

import { TwinService } from '..';

export const KEY_QR = 'twins/get/qr';

export const useGetTwinQr = (params: string, options?: IQueryOptions<string>) =>
  useQuery({
    queryKey: [KEY_QR],
    queryFn: () => TwinService.getTwinQr(params),
    ...options,
  });

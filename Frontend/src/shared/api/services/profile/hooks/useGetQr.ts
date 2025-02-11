import { useQuery } from '@tanstack/react-query';

import { IQueryOptions } from '@/shared/api/model';

import { ProfileService } from '..';

export const KEY_QR = 'users/me/qr';

export const useGetQr = (options?: IQueryOptions<string>) =>
  useQuery({
    queryKey: [KEY_QR],
    queryFn: () => ProfileService.getQr(),
    ...options,
  });

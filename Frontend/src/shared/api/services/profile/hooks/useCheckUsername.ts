import { useQuery } from '@tanstack/react-query';

import { IQueryOptions } from '@/shared/api/model';

import { ProfileService } from '..';
import { ICheckUsernameResponse } from '../model';

export const KEY_CHECK = 'users/check';

export const useCheckUsername = (params: string, options?: IQueryOptions<ICheckUsernameResponse>) =>
  useQuery({
    queryKey: [KEY_CHECK, params],
    queryFn: () => ProfileService.getCheckUsername(params),
    enabled: !!params,
    ...options,
  });

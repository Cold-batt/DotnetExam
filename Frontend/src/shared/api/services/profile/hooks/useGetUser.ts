import { useQuery } from '@tanstack/react-query';

import { IQueryOptions } from '@/shared/api/model';

import { ProfileService } from '..';
import { IArtUser } from '../model';

export const KEY_USER = 'users/get/me';

export const useGetUser = (options?: IQueryOptions<IArtUser>) =>
  useQuery({
    queryKey: [KEY_USER, options?.enabled],
    queryFn: () => ProfileService.getUser(),
    ...options,
  });

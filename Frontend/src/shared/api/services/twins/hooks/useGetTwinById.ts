import { useQuery } from '@tanstack/react-query';

import { IQueryOptions } from '@/shared/api/model';

import { TwinService } from '..';
import { ITwin } from '../model';

export const KEY_GET_TWIN_BY_ID = 'getTwinById';

export const useGetTwinById = (id?: string, options?: IQueryOptions<ITwin>) =>
  useQuery({
    queryKey: [KEY_GET_TWIN_BY_ID, id],
    queryFn: () => TwinService.getTwin(String(id)),
    enabled: !!id,
    ...options,
  });

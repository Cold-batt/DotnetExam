import { useInfiniteQuery } from '@tanstack/react-query';

import { IInfiniteQueryOptions } from '@/shared/api/model';

import { TwinService } from '..';
import { ITwinsRequest, ITwinsResponse } from '../model';

const KEY_TWINS = 'twins/list/my';

export const useGetTwinsList = (
  params: ITwinsRequest,
  options?: IInfiniteQueryOptions<ITwinsResponse>,
) =>
  useInfiniteQuery({
    queryKey: [KEY_TWINS, params],
    queryFn: ({ pageParam = 0 }) =>
      TwinService.getTwinsList({
        ...params,
        offset: pageParam as number,
      }),
    getNextPageParam: (lastPage, allPages) => {
      const totalDataCount = (allPages?.flatMap((el) => el.items)).length;
      if (lastPage.total > totalDataCount) return totalDataCount;
      return undefined;
    },
    initialPageParam: params?.offset,
    ...options,
  });

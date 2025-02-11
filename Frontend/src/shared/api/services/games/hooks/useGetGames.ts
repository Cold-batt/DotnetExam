import { useInfiniteQuery } from '@tanstack/react-query';

import { IInfiniteQueryOptions } from '@/shared/api/model';

import { GamesService } from '..';
import { IGetGamesResponse, IGetGamesRequest } from '../module';

export const KEY_GAMES = 'getGames';

export const useGetGames = (
  params: IGetGamesRequest,
  options?: IInfiniteQueryOptions<IGetGamesResponse>,
) =>
  useInfiniteQuery({
    queryKey: [KEY_GAMES, params],
    queryFn: ({ pageParam = 1 }) =>
      GamesService.getGames({
        ...params,
        PageNumber: pageParam as number,
        PageSize: 10,
      }),
    getNextPageParam: (lastPage, allPages, pageParam) => {
      const totalDataCount = (allPages?.flatMap((el) => el.entities)).length;
      if (totalDataCount < lastPage.totalCount) return pageParam as number + 1;
      return undefined;
    },
    initialPageParam: params?.PageSize,
    ...options,
  });

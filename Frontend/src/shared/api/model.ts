import {
  InfiniteData,
  UseInfiniteQueryOptions,
  UseMutationOptions,
  UseQueryOptions,
} from '@tanstack/react-query';
import { AxiosError } from 'axios';

import { IBaseError } from '../types';

export type IBasicResponseError = AxiosError<IBaseError>;

export type IQueryOptions<T> = Omit<
  UseQueryOptions<T, IBasicResponseError>,
  'queryKey' | 'queryFn'
>;

export type IInfiniteQueryOptions<T, E = IBasicResponseError> = Omit<
  UseInfiniteQueryOptions<T, AxiosError<E>, InfiniteData<T>>,
  'queryKey' | 'queryFn' | 'getPreviousPageParam' | 'getNextPageParam' | 'initialPageParam'
>;

export type IMutationOptions<T, D = void, E = IBasicResponseError> = Omit<
  UseMutationOptions<T, E, D>,
  'mutationKey' | 'mutationFn'
>;

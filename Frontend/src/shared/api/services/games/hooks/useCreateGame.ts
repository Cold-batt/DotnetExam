import { useMutation } from '@tanstack/react-query';

import { IMutationOptions } from '@/shared/api/model';


import { ICreateGameRequest, ICreateGameResponse } from '../module';
import { GamesService } from '..';


export const KEY_CREATE_Game = 'createGame';

export const useCreateGame = (options?: IMutationOptions<ICreateGameResponse, ICreateGameRequest>) =>
  useMutation({
    mutationKey: [KEY_CREATE_Game],
    mutationFn: (data) => GamesService.createGame(data),
    ...options,
  });

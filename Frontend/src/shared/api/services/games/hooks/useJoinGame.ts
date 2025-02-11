import { useMutation } from '@tanstack/react-query';

import { IMutationOptions } from '@/shared/api/model';


import { IJoinGameRequest, IJoinGameResponse } from '../module';
import { GamesService } from '..';


export const KEY_JOIN_Game = 'createGame';

export const useJoinGame = (options?: IMutationOptions<IJoinGameResponse, IJoinGameRequest >) =>
  useMutation({
    mutationKey: [KEY_JOIN_Game],
    mutationFn: (data) => GamesService.joinGame(data),
    ...options,
  });

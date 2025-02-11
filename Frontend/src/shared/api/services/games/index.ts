import { apiExtract } from '../..';
import { ICreateGameRequest, ICreateGameResponse, IGetGamesRequest, IGetGamesResponse, IJoinGameRequest, IJoinGameResponse } from './module';


export const GamesService = {
  async getGames(params: IGetGamesRequest) {
    return await apiExtract.get<IGetGamesResponse>('/Game/games', { params });
  },

  async createGame(data: ICreateGameRequest) {
    return await apiExtract.post<ICreateGameResponse>('/Game/create', data);
  },

  async joinGame(data: IJoinGameRequest) {
    return await apiExtract.post<IJoinGameResponse>('/Game/join', data)
  }
};

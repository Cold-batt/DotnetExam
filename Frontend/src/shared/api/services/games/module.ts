export interface IGetGamesRequest {
  PageNumber?: number;
  PageSize?: number;
}

export interface IGetGamesResponse {
  entities: IGame[]
  totalCount: number;
}

export interface IGame {
  gameId: string;
  maxRate: number;
  gameState: number;
}

export interface ICreateGameRequest {
  maxRate: number;
}

export interface ICreateGameResponse {
  gameId: string;
}

export interface IJoinGameRequest {
  gameId: string;
}

export interface IJoinGameResponse {
  gameId: string;
  ownerId: string;
  opponentId?: string;
  gameProgress?: {
    moveUserId?: string;
    gameMap?: number[];
  }
}
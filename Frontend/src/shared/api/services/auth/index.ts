import { apiExtract } from '../..';
import { ISignInRequest, ISignInResponse, ISignUpRequest, IUserDataRequest } from './model';

export const AuthService = {
  async login(data: ISignInRequest) {
    return await apiExtract.post<ISignInResponse>('/User/signIn', data);
  },

  async registration(data: ISignUpRequest) {
    return await apiExtract.post<ISignInResponse>('User/register', data);
  },

  async getUserData() {
    return await apiExtract.get<IUserDataRequest>('/User/getUserData')
  }
};

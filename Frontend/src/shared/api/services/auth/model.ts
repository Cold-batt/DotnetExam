export type ISignInRequest = {
  userName: string;
  password: string;
};

export type ISignInResponse = {
  jwtToken: string;
}

export type ISignUpRequest = {
  userName: string;
  password: string;
  confirmPassword: string;
};

export type IUserDataRequest = {
  username: string;
  userRaiting: string;
}
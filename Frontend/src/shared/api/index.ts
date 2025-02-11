import axios, { AxiosResponse } from 'axios';
import { authUtils } from '../utils';

const API_URL = import.meta.env.VITE_API_URL;

export const api = axios.create({
  baseURL: API_URL,
});

api.interceptors.request.use(
  (config) => {
    if (config.url === '/auth/login') {
      config.headers['Content-Type'] = 'application/x-www-form-urlencoded';
    } else {
      config.headers['Content-Type'] = 'application/json';
    }

    if (config.headers) {
      const token = authUtils.getToken();

      if (token) {
        config.headers['Authorization'] = `Bearer ${token}`;
      }
    }

    return config;
  },
  (error) => Promise.reject(error),
);

api.interceptors.response.use(
  (response) => response,
  async (error) => {

    if (error.response && error.response.status === 401) {
      if (window.location.pathname !== '/signIn') {
        window.location.href = '/signIn';
      }
    }

    return Promise.reject(error);
  },
);

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

export const apiExtract = {
  get: <T>(...args: Parameters<typeof api.get<T>>) => api.get<T>(...args).then(responseBody),
  post: <T>(...args: Parameters<typeof api.post<T>>) => api.post<T>(...args).then(responseBody),
  put: <T>(...args: Parameters<typeof api.put<T>>) => api.put<T>(...args).then(responseBody),
  patch: <T>(...args: Parameters<typeof api.patch<T>>) => api.patch<T>(...args).then(responseBody),
  delete: <T>(...args: Parameters<typeof api.delete<T>>) =>
    api.delete<T>(...args).then(responseBody),
};


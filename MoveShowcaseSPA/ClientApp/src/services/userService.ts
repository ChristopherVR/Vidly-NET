import http from './httpService';
import apiUrl from '../config.json';
import { RegisterUser, UpdateUser } from '../interfaces/user';

const apiEndpoint = `${apiUrl}/user`;

export const update = async (user: UpdateUser) => {
  const data = await http.post(`${apiEndpoint}/update`, {
    ...user,
  });
  return data;
};

export default update;

export const register = async (user: RegisterUser) => {
  const response = await http
    .post(`${apiEndpoint}/create`, {
      ...user,
    })
    .then((x) => x.data);

  sessionStorage.setItem('auth_token', response.token);
  return response.id;
};

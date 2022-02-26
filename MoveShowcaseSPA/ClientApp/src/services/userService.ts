import http from './httpService';
import { apiUrl } from '../config.json';
import { RegisterUser, User } from '../interfaces/user';

const apiEndpoint = `${apiUrl}/users`;

export const update = async (user: User) => {
  const data = await http.post(apiEndpoint, {
    email: user.userName,
    name: user.name,
    surname: user.surname,
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

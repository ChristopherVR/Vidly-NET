import http from './httpService';
import { RegisterUser, UpdateUser } from '../interfaces/user';

const apiEndpoint = `${process.env.REACT_APP_API_URL}users`;

export const update = async (user: UpdateUser) => {
  const data = await http.patch(`${apiEndpoint}/user`, {
    ...user,
  });
  return data;
};

export default update;

export const register = async (user: RegisterUser) => {
  const response = await http
    .post(`${apiEndpoint}/user`, {
      ...user,
    })
    .then((x) => x.data);

  sessionStorage.setItem('auth_token', response.token);
  return response.id;
};

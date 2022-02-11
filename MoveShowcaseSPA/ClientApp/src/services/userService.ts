import http from './httpService';
import { apiUrl } from '../config.json';
import { User } from '../interfaces/user';

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

export function register(user: User | undefined) {
  throw new Error('Function not implemented.');
}

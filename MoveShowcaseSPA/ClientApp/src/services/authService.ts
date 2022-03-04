import http from './httpService';
import apiUrl from '../config.json';

const apiEndpoint = `${apiUrl}/user/`;

export const login = async (email: string, password: string) => {
  await http.post(`${apiEndpoint}Login`, { username: email, password });
};

export const logout = async () => {
  await http.post(`${apiEndpoint}Logout`);
};

export const getCurrentUser = async () => {
  try {
    const user = await http.get(apiEndpoint);
    return user;
  } catch (ex) {
    return null;
  }
};

export default {
  login,
  logout,
  getCurrentUser,
};

import { toast } from 'react-toastify';
import http from './httpService';

const apiEndpoint = `${process.env.REACT_APP_API_URL}users/`;

export const login = async (email: string, password: string) => {
  try {
    const response = await http.post(`${apiEndpoint}user/login`, {
      username: email,
      password,
    });
    sessionStorage.setItem('auth_token', response.data.token);
  } catch {
    toast.error('An error ocurred trying to login');
  }
};

export const logout = async () => {
  await http.post(`${apiEndpoint}user/logout`);
  sessionStorage.removeItem('auth_token');
};

export const getCurrentUser = async () => {
  try {
    const response = await http.get(apiEndpoint);
    return response.data;
  } catch (ex) {
    return null;
  }
};

export default {
  login,
  logout,
  getCurrentUser,
};

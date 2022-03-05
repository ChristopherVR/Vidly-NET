import { toast } from 'react-toastify';
import http from './httpService';

const apiEndpoint = `${process.env.REACT_APP_API_URL}user/`;

export const login = async (email: string, password: string) => {
  try {
    const response = await http.post(`${apiEndpoint}Login`, {
      username: email,
      password,
    });
    sessionStorage.setItem('auth_token', response.data.token);
  } catch {
    toast.error('An error ocurred trying to login');
  }
};

export const logout = async () => {
  await http.post(`${apiEndpoint}Logout`);
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

import { AxiosResponse } from 'axios';
import { toast } from 'react-toastify';
import { LoginUser, User } from '../interfaces/user';
import http from './httpService';

const apiEndpoint = `${process.env.REACT_APP_API_URL}/users/`;

export const login = async (
  email: string,
  password: string,
  cb: React.Dispatch<React.SetStateAction<User | undefined>>,
) => {
  if (!process.env.REACT_APP_API_URL) {
    toast.error(
      'System is not properly configured. Contact a System Administrator.',
    );
    return;
  }
  await http
    .post(`${apiEndpoint}user/login`, {
      username: email,
      password,
    })
    .then((res: AxiosResponse<LoginUser, LoginUser>) => {
      sessionStorage.setItem('auth_token', res.data.token);
      cb({
        id: res.data.id,
        name: res.data.name,
        surname: res.data.surname,
        userName: res.data.userName,
      });
    })
    .catch((err) => toast.error(err));
};

export const logout = async () => {
  sessionStorage.removeItem('auth_token');
};

export const getCurrentUser = async () => {
  try {
    const token = sessionStorage.getItem('auth_token');
    if (token && token !== 'undefined') {
      const response = await http.get(`${apiEndpoint}user`);
      return response.data;
    }
    return null;
  } catch {
    toast.error('An error occurred trying to retrieve the user details');
  }
  return null;
};

export const isLoggedIn = () => !!sessionStorage.getItem('auth_token');

export default {
  login,
  logout,
  getCurrentUser,
  isLoggedIn,
};

import axios from 'axios';
import { toast } from 'react-toastify';

axios.interceptors.request.use((request) => {
  const token = sessionStorage.getItem('auth_token');
  if (token && request.headers) {
    request.headers.Authorization = `Bearer ${sessionStorage.getItem(
      'auth_token',
    )}`;
  }
  return request;
});

axios.interceptors.response.use(
  (res) => res,
  (error) => {
    const expectedError =
      error.response &&
      error.response.status >= 400 &&
      error.response.status < 500;
    if (!expectedError) {
      // eslint-disable-next-line no-console
      console.debug(error);
      toast.error('An unexpected error occurred.');
    }

    return Promise.reject(error);
  },
);

export default {
  get: axios.get,
  post: axios.post,
  put: axios.put,
  patch: axios.patch,
  delete: axios.delete,
};

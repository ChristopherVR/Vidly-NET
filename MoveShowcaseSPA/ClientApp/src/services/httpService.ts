import axios from 'axios';
import { toast } from 'react-toastify';

axios.interceptors.response.use(undefined, (error) => {
  const expectedError =
    error.response &&
    error.response.status >= 400 &&
    error.response.status < 500;

  if (!expectedError) {
    // eslint-disable-next-line no-console
    console.log(error);
    toast.error('An unexpected error occurred.');
  }

  return Promise.reject(error);
});

export default {
  get: axios.get,
  post: axios.post,
  put: axios.put,
  delete: axios.delete,
};

import { useEffect } from 'react';
import auth from '../services/authService';

function Logout() {
  useEffect(() => {
    auth.logout();
    window.location.href = '/';
  });

  return null;
}

export default Logout;

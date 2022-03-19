import { useContext, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import UserContext from '../context/userContext';
import auth from '../services/authService';

function Logout() {
  const navigate = useNavigate();
  const { setUser } = useContext(UserContext);
  useEffect(() => {
    auth.logout();
    navigate('/');
    if (setUser) setUser(undefined);
  });

  return null;
}

export default Logout;

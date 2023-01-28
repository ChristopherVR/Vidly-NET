import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import authService from '../services/authService';

function PrivateRoute() {
  const isLoggedIn = authService.isLoggedIn();

  if (!isLoggedIn) {
    return <Navigate to="/login" />;
  }
  // eslint-disable-next-line react/jsx-props-no-spreading
  return <Outlet />;
}

export default PrivateRoute;
